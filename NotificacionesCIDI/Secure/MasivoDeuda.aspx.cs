using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeuda : System.Web.UI.Page
    {
        List<DAL.MasivoDeuda> lstFiltrada = null;
        List<DAL.Detalle_notificaciones_generaes_cidi> lstDetalle = null;
        int subsistema;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                Session["subsistema"] = subsistema;
                fillBarrios();
                fillCateDeuda();
                fillNotas();
                fillZonas();


            }
        }
        private void fillBarrios()
        {
            lstBarrios.DataSource = BLL.BarriosBLL.read();
            lstBarrios.DataTextField = "nom_barrio";
            lstBarrios.DataValueField = "cod_barrio";
            lstBarrios.DataBind();
        }
        private void fillGrilla(List<DAL.MasivoDeuda> lst)
        {
            lstFiltrada = lst;
            gvDeuda.DataSource = lst;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
        }

        private void fillCalles(int cod_barrio)
        {
            lstCalles.DataSource = BLL.CallesBLL.read(cod_barrio);
            lstCalles.DataTextField = "nom_calle";
            lstCalles.DataValueField = "cod_calle";
            lstCalles.DataBind();
        }
        private void fillZonas()
        {
            lstZonas.DataSource = BLL.ZonasBLL.read();
            lstZonas.DataTextField = "categoria";
            lstZonas.DataValueField = "categoria";
            lstZonas.DataBind();
        }
        // Dropdown de categorias deuda
        private void fillCateDeuda()
        {
            lstCatDeuda.DataSource = BLL.CATE_DEUDA.readInmueble();
            lstCatDeuda.DataTextField = "des_categoria";
            lstCatDeuda.DataValueField = "cod_categoria";
            lstCatDeuda.DataBind();
        }

        protected void lstBarrios_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> barriosSeleccionados = new List<string>();

            foreach (ListItem item in lstBarrios.Items)
            {
                if (item.Selected)
                {
                    // Estabas tratando de convertir a int y luego añadir el int a una lista de strings
                    // Ahora simplemente añadimos el Value como string
                    barriosSeleccionados.Add(item.Text);
                }
            }

            // Si hay barrios seleccionados, cargar las calles correspondientes
            if (barriosSeleccionados.Count > 0)
            {
                fillCallesPorBarrios(barriosSeleccionados);
            }
        }

        private void fillCallesPorBarrios(List<string> barrios)
        {


            lstCalles.DataSource = BLL.CallesBLL.getByCallesByBarrio(barrios);
            lstCalles.DataTextField = "NOM_CALLE";  // El nombre de la calle
            lstCalles.DataValueField = "COD_CALLE"; // El código o ID de la calle
            lstCalles.DataBind();
        }

        private List<DAL.MasivoDeuda> filtroCalle(List<DAL.MasivoDeuda> lst)
        {
            try
            {
                List<DAL.MasivoDeuda> lstFiltrada = new List<DAL.MasivoDeuda>();

                if (ddlCalles.SelectedValue == "3")
                {
                    return lst;
                }

                string desde = txtDesde.Text.Trim().ToUpper();
                string hasta = txtHasta.Text.Trim().ToUpper();


                foreach (var item in lst)
                {
                    string calle = item.nom_calle.Trim().ToUpper();

                    if (string.Compare(calle, desde) >= 0 && string.Compare(calle, hasta) <= 0)
                    {
                        lstFiltrada.Add(item);
                    }
                }

                return lstFiltrada;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                return null;
            }
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel("Deuda General Inmueble", gvDeuda);
        }

        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {
            List<string> seleccionados = new List<string>();
            List<int> categoriasDeudaSeleccionadas = new List<int>();
            List<string> callesSeleccionadas = new List<string>();
            List<string> zonasSeleccionadas = new List<string>();

            List<DAL.MasivoDeuda> lst = BLL.MasivoDeudaBLL.getByAll();


            int desde = 0;
            int hasta = 0;

            if (!string.IsNullOrEmpty(txtDesde.Text) && txtDesde.Text != "0")
            {
                desde = Convert.ToInt32(txtDesde.Text);
            }

            if (!string.IsNullOrEmpty(txtHasta.Text) && txtHasta.Text != "0")
            {
                hasta = Convert.ToInt32(txtHasta.Text);
            }

            foreach (ListItem item in lstCalles.Items)
            {
                if (item.Selected)
                {
                    callesSeleccionadas.Add(item.Text);
                }
            }

            foreach (ListItem item in lstZonas.Items)
            {
                if (item.Selected)
                {
                    zonasSeleccionadas.Add(item.Text);
                }
            }

            foreach (ListItem item in lstCatDeuda.Items)
            {
                if (item.Selected)
                {
                    categoriasDeudaSeleccionadas.Add(Convert.ToInt32(item.Value));
                }
            }

            foreach (ListItem item in lstBarrios.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(item.Text);
                }
            }
            if (seleccionados.Count != 0)
            {
                lst = filtroBarrios(lst, seleccionados);
            }

            if (categoriasDeudaSeleccionadas.Count > 0)
            {
                lst = filtroCategoriaDeuda(lst, categoriasDeudaSeleccionadas[0]);
            }

            if (zonasSeleccionadas.Count > 0)
            {
                lst = filtroZonas(lst, zonasSeleccionadas);
            }

            if (callesSeleccionadas.Count > 0)
            {
                lst = filtroCalles(lst, callesSeleccionadas, desde, hasta);
            }

            seleccionados.Clear();
            categoriasDeudaSeleccionadas.Clear();
            callesSeleccionadas.Clear();
            zonasSeleccionadas.Clear();

            fillGrilla(lst);
            lstFiltrada = lst;
            Session.Add("registros_notificar", lstFiltrada);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }

        protected void btnClearFiltros_ServerClick(object sender, EventArgs e)
        {
            lstBarrios.ClearSelection();
            List<DAL.MasivoDeuda> lst = new List<DAL.MasivoDeuda>();
            Session["LST"] = lst;
            fillGrilla(lst);
            Session["registros_notificar"] = lst;
            Session["Detalle"] = null;
            gvDeuda.DataSource = null;
            gvDeuda.DataBind();
            divFiltros.Visible = true;
            divResultados.Visible = false;

        }

        protected void btnExport_ServerClick(object sender, EventArgs e)
        {
            ExportToExcel("Export", gvDeuda);
        }

        protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        {
            ExportToExcel("Export", gvDeuda);
        }

        protected void ConfirmoPeriodos()
        {

        }

        protected void gvDeuda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var deuda = (DAL.MasivoDeuda)e.Row.DataItem;

                string denominacion = BLL.MasivoDeudaBLL.armoDenominacion(deuda.cir, deuda.sec, deuda.man, deuda.par, deuda.p_h);

                Label lblNroCta = (Label)e.Row.FindControl("lblNroCta");

                if (lblNroCta != null)
                {
                    lblNroCta.Text = denominacion;
                }
            }
        }

        //Filtros


        private List<DAL.MasivoDeuda> filtroBarrios(List<DAL.MasivoDeuda> lst, List<string> seleccionados)
        {
            try
            {
                lst = (from lista in lst
                       where seleccionados.Contains(lista.nom_barrio)
                       select lista).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeuda> filtroCalles(List<DAL.MasivoDeuda> lst, List<string> seleccionados, int? desde, int? hasta)
        {
            try
            {
                if (seleccionados == null || seleccionados.Count == 0)
                {
                    return lst;
                }

                bool hayCallesEnLista = lst.Any(x => !string.IsNullOrEmpty(x.nom_calle));

                if ((!desde.HasValue || desde.Value <= 0) && (!hasta.HasValue || hasta.Value <= 0))
                {
                    if (hayCallesEnLista)
                    {
                        return lst.Where(x => !string.IsNullOrEmpty(x.nom_calle) &&
                                           seleccionados.Contains(x.nom_calle.Trim())).ToList();
                    }
                }

                var inmueblesFiltrados = lst.Select(i => new { i.cir, i.sec, i.man, i.par, i.p_h }).ToList();

                List<DAL.MasivoDeuda> lstCallesAlturas = BLL.MasivoDeudaBLL.getByCalles(seleccionados, desde, hasta);

                return lstCallesAlturas.Where(nuevo =>
                    inmueblesFiltrados.Any(f =>
                        f.cir == nuevo.cir &&
                        f.sec == nuevo.sec &&
                        f.man == nuevo.man &&
                        f.par == nuevo.par &&
                        f.p_h == nuevo.p_h
                    )
                ).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeuda> filtroZonas(List<DAL.MasivoDeuda> lst, List<string> seleccionados)
        {
            try
            {
                List<DAL.MasivoDeuda> listaPorZonas = BLL.MasivoDeudaBLL.getByZonas(seleccionados);

                // Realizar la intersección entre ambas listas basada en la clave del inmueble
                lst = (from item in lst
                       join itemZona in listaPorZonas
                       on new { item.cir, item.sec, item.man, item.par, item.p_h }
                       equals new { itemZona.cir, itemZona.sec, itemZona.man, itemZona.par, itemZona.p_h }
                       select item).ToList();

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeuda> filtroCategoriaDeuda(List<DAL.MasivoDeuda> lst, int cod_categoria)
        {
            try
            {
                lst = BLL.MasivoDeudaBLL.getByCategoriaDeuda(cod_categoria);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {

        }


        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (Session["id_plantilla"] == null)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaPlantilla", @"
                            var modalElement = document.getElementById('modalSeleccionarPlantilla');
                            var myModal = new bootstrap.Modal(modalElement);
                            myModal.show();
                         ", true);

                    return;
                }


                lstFiltrada = (List<DAL.MasivoDeuda>)Session["registros_notificar"];
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
                        obj2.Circunscripcion = item.cir;
                        obj2.Seccion = item.sec;
                        obj2.Manzana = item.man;
                        obj2.Parcela = item.par;
                        obj2.P_h = item.p_h;
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

        private void fillNotas()
        {
            var listaNotas = BLL.NotasPlantillasBLL.read()
                   .Select(n => new { n.id, n.nom_plantilla, n.contenido })
                   .ToList();

            gvPlantilla.DataSource = listaNotas;
            gvPlantilla.DataBind();

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