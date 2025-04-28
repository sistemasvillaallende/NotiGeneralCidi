using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DocumentFormat.OpenXml.ExtendedProperties;
using Newtonsoft.Json;
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
                Session["subsistema"] = subsistema;
                Session["id_plantilla"] = null;
                Session.Remove("id_plantilla");
                fillNotas();
            }
        }

        private void fillGrilla(int anioDesde,int anioHasta, bool exento)
        {

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
               lstFiltrada = JsonConvert.DeserializeObject<List<DAL.MasivoDeudaAuto>>(response.Content);
            }

            gvDeuda.DataSource = lstFiltrada;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
            Session.Add("registros_notificar", lstFiltrada);
        }

        private void fillNotas()
        {

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
                 lst = JsonConvert.DeserializeObject<List<DAL.NotasPlantillas>>(response.Content);    
            }

            var listaNotas = lst
                   .Select(n => new { n.id, n.nom_plantilla, n.contenido })
                   .ToList();

            gvPlantilla.DataSource = listaNotas;
            gvPlantilla.DataBind();

        }


        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {
            int anioDesde = Convert.ToInt32(txtAnio.Text);
            int anioHasta = Convert.ToInt32(TextBox1.Text);
            bool exento = chkExento.Checked;

            fillGrilla(anioDesde,anioHasta, exento);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }

        //protected void btnClearFiltros_ServerClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtAnio.Text = "";
        //        lstFiltrada = new List<DAL.MasivoDeudaAuto>();
        //        Session.Remove("registros_notificar");

        //        gvDeuda.DataSource = null;
        //        gvDeuda.DataBind();
        //        Response.Redirect(Request.RawUrl, false);
        //        Context.ApplicationInstance.CompleteRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = "Error al limpiar los filtros: " + ex.Message;
        //    }
        //}

        protected void gvDeuda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {
                List<DAL.MasivoDeudaAuto> todosLosRegistros = (List<DAL.MasivoDeudaAuto>)Session["registros_notificar"];

                List<DAL.MasivoDeudaAuto> lstFiltrada = new List<DAL.MasivoDeudaAuto>();

                foreach (GridViewRow row in gvDeuda.Rows)
                {
                    CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSelect");

                    if (chkSeleccionar != null && chkSeleccionar.Checked)
                    {
                        int index = row.RowIndex;
                        lstFiltrada.Add(todosLosRegistros[index]);
                    }
                }

                if (lstFiltrada.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal",
                    "$('#modalErrorTexto').text('Debe seleccionar algun registro.');" +
                    "$('#modalError').modal('show');", true);
                    return;
                }

                int nroNotificacion = 1;
                NotificacionGeneral obj = new NotificacionGeneral();
                List<DAL.DetNotificacionGeneral> lst = new List<DetNotificacionGeneral>();
                int idPlantilla = Convert.ToInt32(Session["id_plantilla"]);
                if (Session["id_plantilla"] == null )
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal",
                    "$('#modalErrorTexto').text('Debe seleccionar una plantilla.');" +
                    "$('#modalError').modal('show');", true);
                    return;
                }
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
                        obj2.Dominio = item.dominio;
                        obj2.Cuit = item.cuit;
                        obj2.Nombre = $"{item.nombre} {item.apellido}";
                        obj2.Cod_estado_cidi = 0;
                        lst.Add(obj2);
                    }
                }
                InsertDetalleNotificacion(lst);
                InsertNotificacionGeneral(obj);
                Response.Redirect($"/Secure/DetNotificacionesGeneral.aspx?nro_emision={obj.Nro_Emision}&subsistema={subsistema}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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


        protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                lstFiltrada = (List<DAL.MasivoDeudaAuto>)Session["registros_notificar"];

                if (lstFiltrada == null || lstFiltrada.Count == 0)
                {
                    lblError.Text = "No hay datos para exportar.";
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=NotificacionAuto_Export_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";

                Page.EnableViewState = false;

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    gvDeuda.AllowPaging = false;
                    gvDeuda.DataSource = lstFiltrada;
                    gvDeuda.DataBind();
                    gvDeuda.Columns[0].Visible = false;
                    gvDeuda.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                    gvDeuda.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
                    gvDeuda.RowStyle.BackColor = System.Drawing.Color.White;

                    gvDeuda.RenderControl(hw);
                    gvDeuda.Columns[0].Visible = true;
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                Response.End();
            }
            catch (Exception ex)
            {
                string errorMessage = $"Export Error: {ex.Message}\nStack Trace: {ex.StackTrace}";
                lblError.Text = "Error al exportar: " + ex.Message;
                System.Diagnostics.Debug.WriteLine(errorMessage);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


    }
}