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
                Session["subsistema"] = subsistema;
                fillZonas();
                fillCalles();
                fillNotas();
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
        private void fillGrilla(int desde, int hasta, int cod_calle ,bool dado_baja, string cod_zona)
        {
            List<DAL.MasivoDeudaIyC> filtroIndyCom = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"NotificadorIndyCom/getNotificacionIndycomFiltered?calleDesde={desde}&calleHasta={hasta}&cod_calle={cod_calle}&dado_baja={dado_baja}&cod_zona={cod_zona}", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                filtroIndyCom = JsonConvert.DeserializeObject<List<DAL.MasivoDeudaIyC>>(response.Content);
            }

            gvDeuda.DataSource = filtroIndyCom;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
            Session.Add("registros_notificar", filtroIndyCom);
        }

        private void fillNotas()
        {
            List<NotasPlantillas> plantillas = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("Plantillas/getPlantillas", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                plantillas = JsonConvert.DeserializeObject<List<NotasPlantillas>>(response.Content);
            }

            var listaNotas = plantillas
                   .Select(n => new { n.id, n.nom_plantilla, n.contenido })
                   .ToList();

            gvPlantilla.DataSource = listaNotas;
            gvPlantilla.DataBind();

        }


        protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {

                lstFiltrada = (List<DAL.MasivoDeudaIyC>)Session["registros_notificar"];

                // Verify data exists
                if (lstFiltrada == null || lstFiltrada.Count == 0)
                {
                    lblError.Text = "No hay datos para exportar.";
                    return;
                }

                // Ensure proper response configuration
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=DeudaIyC_Export_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";

                // Disable view state
                Page.EnableViewState = false;

                // Create a string writer
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    // Prepare GridView
                    gvDeuda.AllowPaging = false;
                    gvDeuda.DataSource = lstFiltrada;
                    gvDeuda.DataBind();

                    // Add custom styling for export
                    gvDeuda.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                    gvDeuda.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
                    gvDeuda.RowStyle.BackColor = System.Drawing.Color.White;

                    // Render the GridView
                    gvDeuda.RenderControl(hw);

                    // Write to response
                    Response.Write(sw.ToString());

                    // End the response
                    Response.End();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                // This is expected and can be ignored
                Response.End();
            }
            catch (Exception ex)
            {
                // Log the full error
                string errorMessage = $"Export Error: {ex.Message}\nStack Trace: {ex.StackTrace}";

                // Display error to user
                lblError.Text = "Error al exportar: " + ex.Message;

                // Optional: Log to server or file
                System.Diagnostics.Debug.WriteLine(errorMessage);
                // Or use your preferred logging mechanism
            }
        }

        // Override to prevent form validation error
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Do nothing
        }


        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {

            int desde = !string.IsNullOrWhiteSpace(txtDesde.Text)
                ? Convert.ToInt32(txtDesde.Text)
                : 0;

            int hasta = !string.IsNullOrWhiteSpace(txtHasta.Text)
                ? Convert.ToInt32(txtHasta.Text)
                : 0;
            bool activo = chkActivo.Checked;
            int cod_calle = (lstCalles.SelectedItem != null && !string.IsNullOrWhiteSpace(lstCalles.SelectedItem.Value))
                ? Convert.ToInt32(lstCalles.SelectedItem.Value)
                : 0;

            string cod_zona = (lstZonas.SelectedItem != null && !string.IsNullOrWhiteSpace(lstZonas.SelectedItem.Text))
                ? lstZonas.SelectedItem.Text
                : null;

            fillGrilla(desde, hasta,cod_calle, activo, cod_zona);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }

         protected void btnClearFiltros_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtDesde.Text = "";
                txtHasta.Text = "";
                chkActivo.Checked = false;
                lstCalles.ClearSelection();
               
                lstFiltrada = new List<DAL.MasivoDeudaIyC>();
                Session.Remove("registros_notificar");

                gvDeuda.DataSource = null;
                gvDeuda.DataBind();
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al limpiar los filtros: " + ex.Message;
            }
        }

        protected void gvDeuda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {

                lstFiltrada = (List<DAL.MasivoDeudaIyC>)Session["registros_notificar"];
                int nroNotificacion = 1;

                NotificacionGeneral obj = new NotificacionGeneral();
                List<DAL.DetNotificacionGeneral> lst = new List<DetNotificacionGeneral>();
                int idPlantilla = Convert.ToInt32(Session["id_plantilla"]);

                var plantilla = GetPlantillaByPk(idPlantilla);
                string contenidoPlantilla = plantilla.contenido;

                int subsistema = Convert.ToInt32(Session["subsistema"]);

                obj.Nro_Emision = GetMaxNroEmision() + 1;
                obj.subsistema = subsistema;
                obj.Cantidad_Reg = lstFiltrada.Count();
                obj.id_plantilla = idPlantilla;

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

                        string contenidoPersonalizado = ReemplazarVariables(contenidoPlantilla, item.nombre, item.apellido, item.cuit);
                        EnviarNotificacion(contenidoPersonalizado, item.cuit); // aca ya tengo la plantilla con las variables
                        lst.Add(obj2);
                    }
                }
                InsertDetalleNotificacion(lst);
                InsertNotificacionGeneral(obj);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", @"
                        var modalElement = document.getElementById('modalNotif');
                        var myModal = new bootstrap.Modal(modalElement);
                        
                        // Asegura que los botones de cierre funcionen
                        document.querySelector('#modalNotif .btn-close').addEventListener('click', function() {
                            myModal.hide();
                        });
                        
                        document.querySelector('#modalNotif .btn-secondary').addEventListener('click', function() {
                            myModal.hide();
                        });
                        
                        myModal.show();
                    ", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ReemplazarVariables(string plantilla, string nombre, string apellido, string cuit)
        {
            string resultado = plantilla;
            resultado = resultado.Replace("{nombre}", nombre);
            resultado = resultado.Replace("{apellido}", apellido);
            resultado = resultado.Replace("{cuit}", cuit);
            return resultado;
        }

        private void EnviarNotificacion(string contenidoPersonalizado, string cuit)
        {


        }
        private void InsertDetalleNotificacion(List<DAL.DetNotificacionGeneral> lst)
        {
            try
            {
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new RestClient(options);
                var requestInsert = new RestRequest("DetalleNotificador/insertMasivo", Method.Post);
                requestInsert.AddJsonBody(lst);

                RestResponse responseInsert = client.Execute(requestInsert);

            }
            catch (Exception)
            {
                throw;
            }

        }

        private void InsertNotificacionGeneral(NotificacionGeneral obj)
        {
            try
            {
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new RestClient(options);
                var requestInsert = new RestRequest("NotificadorGeneral/insertNuevaNotificacion", Method.Post);
                requestInsert.AddJsonBody(obj);

                RestResponse responseInsert = client.Execute(requestInsert);

            }
            catch (Exception)
            {
                throw;
            }

        }
        private NotasPlantillas GetPlantillaByPk(int idPlantilla)
        {
            try
            {
                NotasPlantillas plantilla = null;
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new RestClient(options);
                var request = new RestRequest($"Plantillas/getPlantillaByPk?id={idPlantilla}", Method.Get);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    plantilla = JsonConvert.DeserializeObject<NotasPlantillas>(response.Content);
                }
                return plantilla;

            }
            catch (Exception)
            {
                throw;
            }

        }


        private int GetMaxNroEmision()
        {
            try
            {
                int MaxValue = 0;
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new RestClient(options);
                var request = new RestRequest("NotificadorGeneral/getMaxNroEmision", Method.Get);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    MaxValue = JsonConvert.DeserializeObject<Int32>(response.Content);
                }
                return MaxValue;

            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void gvPlantilla_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSeleccionar");

                if (chk != null)
                {
                    e.Row.Attributes["onclick"] = $"javascript:SeleccionarFila(this, '{chk.ClientID}');";
                }
                e.Row.Attributes["style"] = "cursor:pointer;";
            }
        }

        protected void gvPlantilla_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnObtenerSeleccionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvPlantilla.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSeleccionar");
                if (chkSeleccionar.Checked)
                {
                    int id_plantilla = Convert.ToInt32(gvPlantilla.DataKeys[row.RowIndex]["id"]);

                    Session["id_plantilla"] = id_plantilla;


                }
            }
        }

    }
}