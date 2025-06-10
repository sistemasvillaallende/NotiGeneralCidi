using DAL;
using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using NotificacionesCIDI.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeuda : System.Web.UI.Page
    {
        List<DAL.MasivoDeuda> lstFiltrada = null;
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
                fillBarrios();
                fillCateDeuda();
                fillZonas();
                fillCalles();

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
        public static string ProcesarSeleccionados(List<DAL.MasivoDeuda> inmueblesSeleccionados)
        {
            try
            {
                HttpContext.Current.Session["InmueblesSeleccionados"] = inmueblesSeleccionados;

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: a" + ex.Message;
            }
        }

        private void fillBarrios()
        {

            List<BARRIOS> barrios = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("Barrio/getBarrios", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                barrios = JsonConvert.DeserializeObject<List<BARRIOS>>(response.Content);
            }

            lstBarrios.DataSource = barrios;
            lstBarrios.DataTextField = "nom_barrio";
            lstBarrios.DataValueField = "cod_barrio";
            lstBarrios.DataBind();
        }


        [System.Web.Services.WebMethod]
        public static string fillGrillaJSON(int cod_categoria,int cod_barrio, int cod_calle, string cod_zona, int desde, int hasta)
        {
            string urlBase = ConfigurationManager.AppSettings["urlBase"];
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"NotificacionInmueble/getNotificacionInmueble?cod_categoria={cod_categoria}&cod_barrio={cod_barrio}&cod_calle={cod_calle}&cod_zona={cod_zona}&desde={desde}&hasta={hasta}", Method.Get);
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

        private void fillZonas()
        {
            List<ZONAS> zonas = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("Zona/getZonas", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                zonas = JsonConvert.DeserializeObject<List<ZONAS>>(response.Content);
            }
            lstZonas.DataSource = zonas;
            lstZonas.DataTextField = "categoria";
            lstZonas.DataValueField = "categoria";
            lstZonas.DataBind();
        }
        private void fillCateDeuda()
        {

            List<DAL.CATE_DEUDA> cateDeuda = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("CategoriaDeuda/readIndyCom", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                cateDeuda = JsonConvert.DeserializeObject<List<DAL.CATE_DEUDA>>(response.Content);
            }
            lstCatDeuda.DataSource = cateDeuda;
            lstCatDeuda.DataTextField = "des_categoria";
            lstCatDeuda.DataValueField = "cod_categoria";
            lstCatDeuda.DataBind();
        }

        private void fillCalles()
        {
            List<CALLES> calles = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("Calle/getAllCalles", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                calles = JsonConvert.DeserializeObject<List<CALLES>>(response.Content);
            }

            lstCalles.DataSource = calles;
            lstCalles.DataTextField = "NOM_CALLE";
            lstCalles.DataValueField = "COD_CALLE";
            lstCalles.DataBind();
        }



      
        private void ExportToExcel(string nameReport, GridView wControl)
        {

            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment;filename=Deuda_Tasa.xls");
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.xls";

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            //gvDeuda.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();

        }


        protected void btnExport_ServerClick(object sender, EventArgs e)
        {
          //  ExportToExcel("Export", gvDeuda);
        }

        protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        {
           // ExportToExcel("Export", gvDeuda);
        }


        [WebMethod]
        public static string ContinuarGenerarNotificaciones()
        {
            try
            {
                List<DAL.MasivoDeuda> lstFiltrada = (List<DAL.MasivoDeuda>)HttpContext.Current.Session["InmueblesSeleccionados"];

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
                        obj2.Circunscripcion = item.cir;
                        obj2.Seccion = item.sec;
                        obj2.Manzana = item.man;
                        obj2.Parcela = item.par;
                        obj2.P_h = item.p_h;
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

    }
}