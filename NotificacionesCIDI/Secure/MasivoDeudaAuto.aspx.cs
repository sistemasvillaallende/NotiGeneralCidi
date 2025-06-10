using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.ExtendedProperties;
using Newtonsoft.Json;
using NotificacionesCIDI.Helpers;
using RestSharp;

namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeudaAuto : System.Web.UI.Page
    {
        List<DAL.MasivoDeudaAuto> lstFiltrada = null;
        int subsistema;
        string urlBase = ConfigurationManager.AppSettings["urlBase"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                HttpContext.Current.Session["subsistema"] = subsistema;
                if (Request.QueryString["action"] == "downloadExcel")
                {
                    DescargarExcel();
                    return;
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string fillGrillaJSON(int anioDesde, int anioHasta, bool exento)
        {
            string urlBase = ConfigurationManager.AppSettings["urlBase"];
            var options = new RestClientOptions(urlBase)
            {
                MaxTimeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"/NotificacionAuto/getNotificacionAuto?desde={anioDesde}&hasta={anioHasta}&exento={exento}", Method.Get);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                return response.Content;
            }
            else
            {
                return "[]";
            }
        }

        [System.Web.Services.WebMethod]
        public static string ObtenerPlantillas()
        {
            string urlBase = ConfigurationManager.AppSettings["urlBase"];
            List<DAL.NotasPlantillas> lst = null;
            var options = new RestClientOptions(urlBase)
            {
                MaxTimeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("Plantillas/getPlantillas", Method.Get);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                return response.Content;
            }
            else
            {
                return "[]";
            }

        }

        [System.Web.Services.WebMethod]
        public static string GuardarPlantillaEnSesion(int idPlantilla)
        {
            try
            {
                HttpContext.Current.Session["PlantillaSeleccionada"] = idPlantilla;
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static string ProcesarSeleccionados(List<DAL.MasivoDeudaAuto> autosSeleccionados)
        {
            try
            {
                HttpContext.Current.Session["AutosSeleccionados"] = autosSeleccionados;

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: a" + ex.Message;
            }
        }

        [WebMethod]
        public static string ContinuarGenerarNotificaciones()
        {
            try
            {
                List<DAL.MasivoDeudaAuto> lstFiltrada = (List<DAL.MasivoDeudaAuto>)HttpContext.Current.Session["AutosSeleccionados"];

                if (lstFiltrada == null || lstFiltrada.Count == 0)
                {
                    return "Error: No se encontraron registros seleccionados.";
                }

                int idPlantilla = Convert.ToInt32(HttpContext.Current.Session["PlantillaSeleccionada"]);
                int subsistema = Convert.ToInt32(HttpContext.Current.Session["subsistema"]);


                NotificacionGeneral obj = new NotificacionGeneral();
                var plantilla = Helper.GetPlantillaByPk(idPlantilla);
                string contenidoPlantilla = plantilla.contenido;               
                obj.Nro_Emision = Helper.GetMaxNroEmision() + 1;
                obj.subsistema = subsistema;
                obj.Cantidad_Reg = lstFiltrada.Count();
                obj.id_plantilla = idPlantilla;

                List<DetNotificacionGeneral> lst = new List<DetNotificacionGeneral>();
                int nroNotificacion = 1;

                foreach (var item in lstFiltrada)
                {
                    if (item.cuit != null && item.cuit.Length == 11)
                    {
                        DetNotificacionGeneral obj2 = new DetNotificacionGeneral();
                        obj2.Nro_Emision = obj.Nro_Emision;
                        obj2.Nro_Notificacion = nroNotificacion;
                        nroNotificacion++;
                        obj2.Dominio = item.dominio;
                        obj2.Cuit = item.cuit;
                        obj2.Nombre = $"{item.nombre} {item.apellido}";
                        obj2.Cod_estado_cidi = 0;
                        lst.Add(obj2);
                    }
                }

                Helper.InsertDetalleNotificacion(lst);
                Helper.InsertNotificacionGeneral(obj);

                return $"OK:?nro_emision={obj.Nro_Emision}&subsistema={subsistema}";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static string GuardarSeleccionadosParaExport(List<DAL.MasivoDeudaAuto> autosSeleccionados)
        {
            try
            {
                if (autosSeleccionados == null || autosSeleccionados.Count == 0)
                {
                    return "Error: No hay registros seleccionados.";
                }

                HttpContext.Current.Session["AutosParaExportar"] = autosSeleccionados;
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static string GenerarExcel()
        {
            try
            {
                var autosSeleccionados = (List<DAL.MasivoDeudaAuto>)HttpContext.Current.Session["AutosParaExportar"];

                if (autosSeleccionados == null || autosSeleccionados.Count == 0)
                {
                    return "Error: No hay datos en sesión para exportar.";
                }

                string fileName = "NotificacionAuto_Seleccionados_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                HttpContext.Current.Session["ExcelFileName"] = fileName;

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        private void DescargarExcel()
        {
            try
            {
                var autosSeleccionados = (List<DAL.MasivoDeudaAuto>)Session["AutosParaExportar"];

                if (autosSeleccionados == null || autosSeleccionados.Count == 0)
                {
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=NotificacionAuto_Seleccionados_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
                Response.Charset = "";

                using (StringWriter sw = new StringWriter())
                {
                    sw.WriteLine("<table border='1'>");

                    sw.WriteLine("<tr style='background-color: #D3D3D3; color: black; font-weight: bold;'>");
                    sw.WriteLine("<td>Dominio</td>");
                    sw.WriteLine("<td>CUIT</td>");
                    sw.WriteLine("<td>Nombre</td>");
                    sw.WriteLine("<td>Apellido</td>");
                    sw.WriteLine("<td>Año</td>");
                    sw.WriteLine("<td>Exento</td>");
                    sw.WriteLine("</tr>");

                    // Escribir datos
                    foreach (var auto in autosSeleccionados)
                    {
                        sw.WriteLine("<tr>");
                        sw.WriteLine($"<td>{auto.dominio ?? ""}</td>");
                        sw.WriteLine($"<td>{auto.cuit ?? ""}</td>");
                        sw.WriteLine($"<td>{auto.nombre ?? ""}</td>");
                        sw.WriteLine($"<td>{auto.apellido ?? ""}</td>");
                        sw.WriteLine($"<td>{auto.anio}</td>");
                        sw.WriteLine($"<td>{(auto.exento ? "SI" : "NO")}</td>");
                        sw.WriteLine("</tr>");
                    }

                    sw.WriteLine("</table>");

                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {

            }
        }


    }
}