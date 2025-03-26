using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeudaAuto : System.Web.UI.Page
    {
        List<DAL.MasivoDeudaAuto> lstFiltrada = null;
        List<DAL.Detalle_notificaciones_generaes_cidi> lstDetalle = null;
        Notificaciones_generales_cidi objNoti = new Notificaciones_generales_cidi();
        int subsistema;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                Session["subsistema"] = subsistema;
                fillNotas();
            }
        }

        private void fillGrilla(int anio, bool exento)
        {
            lstFiltrada = BLL.MasivoDeudaAutoBLL.read(anio,exento);
            gvDeuda.DataSource = lstFiltrada;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
            Session.Add("registros_notificar", lstFiltrada);
        }

        private void fillNotas()
        {
            var listaNotas = BLL.NotasPlantillasBLL.read()
                   .Select(n => new { n.id, n.nom_plantilla, n.contenido })
                   .ToList();

            gvPlantilla.DataSource = listaNotas;
            gvPlantilla.DataBind();

        }

        private void ExportToExcel(string nameReport, GridView wControl)
        {

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Deuda_iyc.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            gvDeuda.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel("Deuda General Comercio", gvDeuda);
        }

        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {
            List<DAL.MasivoDeudaIyC> lst = new List<DAL.MasivoDeudaIyC>();
            int anio = Convert.ToInt32(txtAnio.Text);            
            bool exento = chkExento.Checked;       

            fillGrilla(anio,exento);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }

        protected void btnClearFiltros_ServerClick(object sender, EventArgs e)
        {

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
        }


        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {

                lstFiltrada = (List<DAL.MasivoDeudaAuto>)Session["registros_notificar"];
                int nroNotificacion = 1;

                NotificacionGeneral obj = new NotificacionGeneral();
                List<DAL.DetNotificacionGeneral> lst = new List<DetNotificacionGeneral>();
                int idPlantilla = Convert.ToInt32(Session["id_plantilla"]);

                var plantilla = BLL.NotasPlantillasBLL.getByPk(idPlantilla);
                string contenidoPlantilla = plantilla.contenido;

                int subsistema = Convert.ToInt32(Session["subsistema"]);

                obj.Nro_Emision = BLL.NotificacionGeneralBLL.getMaxNroEmision() + 1;
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

                        string contenidoPersonalizado = ReemplazarVariables(contenidoPlantilla, item.nombre, item.apellido, item.cuit);
                        EnviarNotificacion(contenidoPersonalizado, item.cuit); // aca ya tengo la plantilla con las variables
                        lst.Add(obj2);
                    }
                }
                BLL.DetNotificacionGenetalBLL.insertMasivo(lst);
                BLL.NotificacionGeneralBLL.insert(obj);

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