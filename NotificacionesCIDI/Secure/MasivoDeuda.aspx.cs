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
        Notificaciones_generales_cidi objNoti = new Notificaciones_generales_cidi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillBarrios();
                fillCateDeuda();
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

        // Dropdown de categorias deuda
        private void fillCateDeuda()
        {
            lstCatDeuda.DataSource = BLL.CATE_DEUDA.readInmueble();
            lstCatDeuda.DataTextField = "des_categoria";
            lstCatDeuda.DataValueField = "cod_categoria";
            lstCatDeuda.DataBind();
        }
        protected void gvDeuda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // gvDeuda.PageIndex = e.NewPageIndex;
            // List<DAL.MasivoDeuda> lst = BLL.MasivoDeudaBLL.read(
            //     Convert.ToDateTime("01/01/1900"), DateTime.Now,
            //     new List<string>());
            // Session["LST"] = lst;
            // fillGrillaDeuda(lst);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the run time error "  
            //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
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

            List<DAL.MasivoDeuda> lst = new List<DAL.MasivoDeuda>();
            var lstFiltroBarrios = lst;
            var lstFiltroCategorias = lst;

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
                // lst = filtroBarrios(lst, seleccionados);
                lstFiltroBarrios = filtroBarrios(lst, seleccionados);

            }

            if (categoriasDeudaSeleccionadas.Count > 0)
            {
                // lst = filtroCategoriaDeuda(lst, categoriasDeudaSeleccionadas[0]);
                lstFiltroCategorias = filtroCategoriaDeuda(lst, categoriasDeudaSeleccionadas[0]);

            }

            lst = lstFiltroBarrios
                    .Union(lstFiltroCategorias)
                    .ToList();

            seleccionados.Clear();
            categoriasDeudaSeleccionadas.Clear();



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
            //VCtasctes Lista;
            //Listadeuda = new List<VCtasctes>();
            //Listadeuda.Clear();
            //for (int i = 0; i < grdList.Rows.Count; i++)
            //{
            //    GridViewRow row = grdList.Rows[i];
            //    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
            //    if (isChecked)
            //    {
            //        if (Convert.ToInt32(grdList.DataKeys[i].Values["nro_transaccion"]) != 1000)
            //        {
            //            Lista = new VCtasctes();
            //            Lista.importe = Convert.ToDecimal(grdList.Rows[i].Cells[2].Text);
            //            Lista.nro_transaccion = Convert.ToInt32(grdList.DataKeys[i].Values["nroTransaccion"]);
            //            Lista.periodo = Convert.ToString(grdList.Rows[i].Cells[1].Text);
            //            Lista.fecha_vencimiento = Convert.ToString(grdList.Rows[i].Cells[3].Text);
            //            //DateTime.Today.ToString("dd/MM/yyyy");
            //            Lista.categoria_deuda = Convert.ToInt32(grdList.DataKeys[i].Values["categoriaDeuda"]);
            //            Listadeuda.Add(Lista);
            //            Lista = null;
            //        }

            //    }
            //}
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
                lst = BLL.MasivoDeudaBLL.getByBarrios(seleccionados);
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
            //         try
            //         {
            //             lstFiltrada = (List<MasivoDeudaInm>)Session["registros_notificar"]; 

            //             Notificaciones_generales_cidi obj =
            // new Notificaciones_generales_cidi();
            //             List<DAL.Detalle_notificaciones_generaes_cidi> lst = 
            //                 new List<Detalle_notificaciones_generaes_cidi>();

            //             foreach (var item in lstFiltrada)
            //             {
            //                 if(item.cuil != null && item.cuil.Length == 11)
            //                 {
            //                     Detalle_notificaciones_generaes_cidi obj2 = 
            //                         new Detalle_notificaciones_generaes_cidi();
            //                     obj2.cuit = item.cuil;
            //                     obj2.detalle_estado_cidi = string.Empty;
            //                     obj2.estado = "Sin Enviar";
            //                     obj2.fecha_primer_envio = null;
            //                     lst.Add(obj2);
            //                 }
            //             }
            //             BLL.Notificaciones_generales_cidi.insert(obj, lst);
            //         }
            //         catch (Exception ex)
            //         {
            //             throw ex;
            //         }
        }
    }
}