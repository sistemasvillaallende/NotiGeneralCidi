using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Web.Services;
using NotificacionesCIDI.Helpers;



namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeudaIyC : System.Web.UI.Page
    {
        List<DAL.MasivoDeudaIyC> lstFiltrada = null;
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
                fillZonas();
                fillCalles();
                //fillNotas();
            }
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
        private void fillZonas()
        {
            List<ZONAS> zonas = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("Zona/getZonasIyC", Method.Get);
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

        [System.Web.Services.WebMethod]
        public static string fillGrillaJSON(int desde, int hasta, int cod_calle, bool dado_baja, string cod_zona)
        {
            string urlBase = ConfigurationManager.AppSettings["urlBase"];
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"NotificadorIndyCom/getNotificacionIndycomFiltered?calleDesde={desde}&calleHasta={hasta}&cod_calle={cod_calle}&dado_baja={dado_baja}&cod_zona={cod_zona}", Method.Get);
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
        public static string ProcesarSeleccionados(List<DAL.MasivoDeudaIyC> comerciosSeleccionados)
        {
            try
            {
                HttpContext.Current.Session["ComercioSeleccionados"] = comerciosSeleccionados;

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: a" + ex.Message;
            }
        }

        //    protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        lstFiltrada = (List<DAL.MasivoDeudaIyC>)Session["registros_notificar"];

        //        // Verify data exists
        //        if (lstFiltrada == null || lstFiltrada.Count == 0)
        //        {
        //            lblError.Text = "No hay datos para exportar.";
        //            return;
        //        }

        //        // Ensure proper response configuration
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("content-disposition", "attachment;filename=DeudaIyC_Export_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        //        Response.Charset = "";

        //        // Disable view state
        //        Page.EnableViewState = false;

        //        // Create a string writer
        //        using (StringWriter sw = new StringWriter())
        //        {
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);

        //            // Prepare GridView
        //            gvDeuda.AllowPaging = false;
        //            gvDeuda.DataSource = lstFiltrada;
        //            gvDeuda.DataBind();
        //            gvDeuda.Columns[0].Visible = false;
        //            // Add custom styling for export
        //            gvDeuda.HeaderStyle.ForeColor = System.Drawing.Color.Black;
        //            gvDeuda.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
        //            gvDeuda.RowStyle.BackColor = System.Drawing.Color.White;

        //            // Render the GridView
        //            gvDeuda.RenderControl(hw);
        //            gvDeuda.Columns[0].Visible = true;
        //            // Write to response
        //            Response.Write(sw.ToString());

        //            // End the response
        //            Response.End();
        //        }
        //    }
        //    catch (System.Threading.ThreadAbortException)
        //    {
        //        // This is expected and can be ignored
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the full error
        //        string errorMessage = $"Export Error: {ex.Message}\nStack Trace: {ex.StackTrace}";

        //        // Display error to user
        //        lblError.Text = "Error al exportar: " + ex.Message;

        //        // Optional: Log to server or file
        //        System.Diagnostics.Debug.WriteLine(errorMessage);
        //        // Or use your preferred logging mechanism
        //    }
        //}

        // Override to prevent form validation error
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Do nothing
        }


        //protected void btnFiltros_ServerClick(object sender, EventArgs e)
        //{

        //    int desde = !string.IsNullOrWhiteSpace(txtDesde.Text)
        //        ? Convert.ToInt32(txtDesde.Text)
        //        : 0;

        //    int hasta = !string.IsNullOrWhiteSpace(txtHasta.Text)
        //        ? Convert.ToInt32(txtHasta.Text)
        //        : 0;
        //    bool activo = false;//chkExento.Checked;

        //    switch (DDLExento.SelectedItem.Value)
        //    {
        //        case "0":
        //            activo = false;
        //            break;
        //        case "1":
        //            activo = true;
        //            break;
        //        default:
        //            break;
        //    }
        //    int cod_calle = (lstCalles.SelectedItem != null && !string.IsNullOrWhiteSpace(lstCalles.SelectedItem.Value))
        //        ? Convert.ToInt32(lstCalles.SelectedItem.Value)
        //        : 0;

        //    string cod_zona = (lstZonas.SelectedItem != null && !string.IsNullOrWhiteSpace(lstZonas.SelectedItem.Text))
        //        ? lstZonas.SelectedItem.Text
        //        : null;

        //    //fillGrilla(desde, hasta,cod_calle, activo, cod_zona);
        //    divFiltros.Visible = false;
        //    divResultados.Visible = true;
        //}

       
        protected void gvDeuda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        [WebMethod]
        public static string ContinuarGenerarNotificaciones()
        {
            try
            {
                List<DAL.MasivoDeudaIyC> lstFiltrada = (List<DAL.MasivoDeudaIyC>)HttpContext.Current.Session["ComercioSeleccionados"];

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
                        obj2.Legajo = item.legajo;
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