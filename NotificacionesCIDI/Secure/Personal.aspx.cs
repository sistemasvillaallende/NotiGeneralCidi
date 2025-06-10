using ClosedXML.Excel;
using DAL;
using Newtonsoft.Json;
using NotificacionesCIDI.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotificacionesCIDI.Secure
{
    public partial class Personal : System.Web.UI.Page
    {
        List<DAL.MasivoDeudaGeneral> lstFiltrada = null;
        int subsistema;
        string urlBase = ConfigurationManager.AppSettings["urlBase"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                HttpContext.Current.Session["subsistema"] = subsistema;
                Session["id_plantilla"] = null;
                Session.Remove("id_plantilla");
                fillClasificacionPersonal();
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
        public static string ProcesarSeleccionados(List<DAL.Personal> personalSeleccionados)
        {
            try
            {
                HttpContext.Current.Session["PersonalSeleccionados"] = personalSeleccionados;

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: a" + ex.Message;
            }
        }


        private void fillClasificacionPersonal()
        {

            List<ClasificacionPersonal> clasifPersonal = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("ClasificacionPersonal/getClasificacionPersonal", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                clasifPersonal = JsonConvert.DeserializeObject<List<ClasificacionPersonal>>(response.Content);
            }

            lstClasificacionPersonal.DataSource = clasifPersonal;
            lstClasificacionPersonal.DataTextField = "des_clasif_per";
            lstClasificacionPersonal.DataValueField = "cod_clasif_per";
            lstClasificacionPersonal.DataBind();
        }


        [System.Web.Services.WebMethod]
        public static string fillGrillaJSON(int codClasif)
        {
            string urlBase = ConfigurationManager.AppSettings["urlBase"];
            var options = new RestClientOptions(urlBase)
            {
                MaxTimeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"Personal/getPersonalByCodClasif?codClasif={codClasif}", Method.Get);

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

        [WebMethod]
        public static string ContinuarGenerarNotificaciones()
        {
            try
            {
                List<DAL.Personal> lstFiltrada = (List<DAL.Personal>)HttpContext.Current.Session["PersonalSeleccionados"];

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
                    if (item.cuil != null)
                    {
                        DetNotificacionGeneral obj2 = new DetNotificacionGeneral();
                        obj2.Nro_Emision = obj.Nro_Emision;
                        obj2.Nro_Notificacion = nroNotificacion;
                        nroNotificacion++;
                        obj2.Cuit = item.cuil;
                        obj2.Nombre = $"{item.nombre}";
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

        /////////////////////////////// EXPORT EXCEL /////////////////////////////////////////


        //protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lstFiltrada = (List<DAL.MasivoDeudaGeneral>)Session["registros_notificar"];

        //        if (lstFiltrada == null || lstFiltrada.Count == 0)
        //        {
        //            lblError.Text = "No hay datos para exportar.";
        //            return;
        //        }

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("content-disposition", "attachment;filename=DeudaGeneral_Export_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        //        Response.Charset = "";

        //        Page.EnableViewState = false;

        //        using (StringWriter sw = new StringWriter())
        //        {
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);
        //            gvDeuda.AllowPaging = false;
        //            gvDeuda.DataSource = lstFiltrada;
        //            gvDeuda.DataBind();
        //            gvDeuda.Columns[0].Visible = false;
        //            gvDeuda.HeaderStyle.ForeColor = System.Drawing.Color.Black;
        //            gvDeuda.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
        //            gvDeuda.RowStyle.BackColor = System.Drawing.Color.White;
        //            gvDeuda.RenderControl(hw);
        //            gvDeuda.Columns[0].Visible = true;
        //            Response.Write(sw.ToString());
        //            Response.End();
        //        }
        //    }
        //    catch (System.Threading.ThreadAbortException)
        //    {
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = $"Export Error: {ex.Message}\nStack Trace: {ex.StackTrace}";
        //        lblError.Text = "Error al exportar: " + ex.Message;
        //        System.Diagnostics.Debug.WriteLine(errorMessage);
        //    }
        //}

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}


    }
}
