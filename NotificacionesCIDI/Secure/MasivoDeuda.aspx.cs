using DAL;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
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
                Session["subsistema"] = subsistema;
                Session["id_plantilla"] = null;
                Session.Remove("id_plantilla");
                fillBarrios();
                fillCateDeuda();
                fillNotas();
                fillZonas();
                fillCalles();

            }
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

        private void fillGrilla(int cod_categoria,int cod_barrio, int cod_calle, string cod_zona, int desde, int hasta)
        {

            List<DAL.MasivoDeuda> filtroInm = null;
            var options = new RestClientOptions(urlBase)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"NotificacionInmueble/getNotificacionInmueble?cod_categoria={cod_categoria}&cod_barrio={cod_barrio}&cod_calle={cod_calle}&cod_zona={cod_zona}&desde={desde}&hasta={hasta}", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                filtroInm = JsonConvert.DeserializeObject<List<DAL.MasivoDeuda>>(response.Content);
            }
            gvDeuda.DataSource = filtroInm;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
            Session.Add("registros_notificar", filtroInm);
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


        protected void gvDeuda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        private void ExportToExcel(string nameReport, GridView wControl)
        {

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Deuda_Tasa.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            gvDeuda.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();

        }

        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {

            int cod_categoria = (lstCatDeuda.SelectedItem != null && !string.IsNullOrWhiteSpace(lstCatDeuda.SelectedItem.Value))
                ? Convert.ToInt32(lstCatDeuda.SelectedItem.Value)
                : 0;

            int cod_barrio = (lstBarrios.SelectedItem != null && !string.IsNullOrWhiteSpace(lstBarrios.SelectedItem.Value))
                ? Convert.ToInt32(lstBarrios.SelectedItem.Value)
                : 0;

            int cod_calle = (lstCalles.SelectedItem != null && !string.IsNullOrWhiteSpace(lstCalles.SelectedItem.Value))
                ? Convert.ToInt32(lstCalles.SelectedItem.Value)
                : 0;

            string cod_zona = (lstZonas.SelectedItem != null && !string.IsNullOrWhiteSpace(lstZonas.SelectedItem.Text))
                ? lstZonas.SelectedItem.Text
                : null;

            int desde = !string.IsNullOrWhiteSpace(txtDesde.Text)
                ? Convert.ToInt32(txtDesde.Text)
                : 0;

            int hasta = !string.IsNullOrWhiteSpace(txtHasta.Text)
                ? Convert.ToInt32(txtHasta.Text)
                : 0;



            fillGrilla(cod_categoria,cod_barrio, cod_calle, cod_zona, desde, hasta);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }

        protected void btnExport_ServerClick(object sender, EventArgs e)
        {
            ExportToExcel("Export", gvDeuda);
        }

        protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        {
            ExportToExcel("Export", gvDeuda);
        }


        protected void gvDeuda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var deuda = (DAL.MasivoDeuda)e.Row.DataItem;

                string denominacion = ArmoDenominacion(deuda.cir, deuda.sec, deuda.man, deuda.par, deuda.p_h);

                Label lblNroCta = (Label)e.Row.FindControl("lblNroCta");

                if (lblNroCta != null)
                {
                    lblNroCta.Text = denominacion;
                }
            }
        }


        protected string ArmoDenominacion(int cir, int sec, int man, int par, int p_h)
        {
            try
            {
                StringBuilder denominacion = new StringBuilder();

                if (cir < 10)
                    denominacion.AppendFormat("CIR: 0{0} - ", cir);
                if (cir > 9 && cir < 100)
                    denominacion.AppendFormat("CIR: {0} - ", cir);

                if (sec < 10)
                    denominacion.AppendFormat("SEC: 0{0} - ", sec);
                if (sec > 9 && sec < 100)
                    denominacion.AppendFormat("SEC: {0} - ", sec);

                if (man < 10)
                    denominacion.AppendFormat("MAN: 00{0} - ", man);
                if (man > 9 && man < 100)
                    denominacion.AppendFormat("MAN: 0{0} - ", man);
                if (man > 99)
                    denominacion.AppendFormat("MAN: {0} - ", man);

                if (par < 10)
                    denominacion.AppendFormat("PAR: 00{0} - ", par);
                if (par > 9 && par < 100)
                    denominacion.AppendFormat("PAR: 0{0} - ", par);
                if (par > 99)
                    denominacion.AppendFormat("PAR: {0} - ", par);

                if (p_h < 10)
                    denominacion.AppendFormat("P_H: 00{0}", p_h);
                if (p_h > 9 && p_h < 100)
                    denominacion.AppendFormat("P_H: 0{0}", p_h);
                if (p_h > 99)
                    denominacion.AppendFormat("P_H: {0}", p_h);

                return denominacion.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {
                List<DAL.MasivoDeuda> todosLosRegistros = (List<DAL.MasivoDeuda>)Session["registros_notificar"];

                List<DAL.MasivoDeuda> lstFiltrada = new List<DAL.MasivoDeuda>();

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
                if (Session["id_plantilla"] == null)
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
                InsertDetalleNotificacion(lst);
                InsertNotificacionGeneral(obj);
                Response.Redirect($"./DetNotificacionesGeneral.aspx?nro_emision={obj.Nro_Emision}&subsistema={subsistema}");
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
        {}

        private string ReemplazarVariables(string plantilla, string nombre, string apellido, string cuit)
        {
            string resultado = plantilla;
            resultado = resultado.Replace("{nombre}", nombre);
            resultado = resultado.Replace("{apellido}", apellido);
            resultado = resultado.Replace("{cuit}", cuit);
            return resultado;
        }

        private void EnviarNotificacion(string contenidoPersonalizado, string cuit, int Nro_Emision, int Nro_notificacion)
        {
            try
            {
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new RestClient(options);
                var request = new RestRequest($"/EnvioNotificacionCIDI/enviarNotificacion?cuerpoNotif={contenidoPersonalizado}&cuit_filter={cuit}&Nro_Emision={Nro_Emision}&Nro_notificacion={Nro_notificacion}", Method.Post);
                string cookieHeaderValue = HttpContext.Current.Request.Headers["Cookie"];
                request.AddHeader("Cookie", cookieHeaderValue);

                RestResponse response = client.Execute(request);

            }
            catch (Exception)
            {
                throw;
            }


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