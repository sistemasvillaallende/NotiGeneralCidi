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

        List<DAL.MasivoDeudaInm> lstFiltrada = null;
        List<DAL.Detalle_notificaciones_generaes_cidi> lstDetalle = null;
        Notificaciones_generales_cidi objNoti = new Notificaciones_generales_cidi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //List<DAL.MasivoDeudaInm> lst = BLL.MasivoDeudaInmBLL.read(Convert.ToDateTime("01/01/1900"), DateTime.Now,
                //    new List<string>());
                //Session["LST"] = lst;
                //fillGrillaDeuda(lst);
                fillBarrios();
                fillZonas();
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
        private void fillZonas()
        {
            lstZonas.DataSource = BLL.ZonasBLL.read();
            lstZonas.DataTextField = "nom_zona";
            lstZonas.DataValueField = "cod_zona";
            lstZonas.DataBind();
        }
        private void fillGrillaDeuda(List<DAL.MasivoDeudaInm> lst)
        {
            lstFiltrada = lst;
            gvDeuda.DataSource = lst;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
            //gvDeuda.HeaderRow.TableSection = TableRowSection.TableHeader;
            decimal juducial = lst.Sum(jud => jud.deudaJudicial);
            txtTotJudicial.Text = string.Format("{0:c}", juducial);

            decimal preJudicial = lst.Sum(preJud => preJud.deudaPreJudicial);
            txtPreJudicial.Text = string.Format("{0:c}", preJudicial);

            decimal administrativa = lst.Sum(admin => admin.deudaAdministrativa);
            txtAdministrativa.Text = string.Format("{0:c}", administrativa);

            decimal normal = lst.Sum(norm => norm.deudaNormal);
            txtNormal.Text = string.Format("{0:c}", normal);

            txtTotal.Text = string.Format("{0:c}", juducial + preJudicial + administrativa + +normal);

            txtRegistros.Text = lst.Count().ToString();
        }
        private void fillCateDeuda()
        {
            lstCatDeuda.DataSource = BLL.CATE_DEUDA.readAuto();
            lstCatDeuda.DataTextField = "des_categoria";
            lstCatDeuda.DataValueField = "cod_categoria";
            lstCatDeuda.DataBind();
        }
        protected void gvDeuda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDeuda.PageIndex = e.NewPageIndex;
            List<DAL.MasivoDeudaInm> lst = BLL.MasivoDeudaInmBLL.read(
                Convert.ToDateTime("01/01/1900"), DateTime.Now,
                new List<string>());
            Session["LST"] = lst;
            fillGrillaDeuda(lst);
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
            List<DAL.MasivoDeudaInm> lst = new List<DAL.MasivoDeudaInm>();
            //CATEGORIA DEUDA
            foreach (ListItem item in lstCatDeuda.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(item.Value);
                }
            }

            //FILTRO FECHA
            lst = filtroFecha(seleccionados);
            if (lst == null)
                return;
            seleccionados.Clear();

            //FILTRO FECHA
            foreach (ListItem item in lstBarrios.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(item.Text);
                }
            }
            if (seleccionados.Count != 0)
                lst = filtroBarrios(lst, seleccionados);
            seleccionados.Clear();

            //FILTRO ZONAS
            foreach (ListItem item in lstZonas.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(item.Text);
                }
            }
            if (seleccionados.Count != 0)
                lst = filtroZonas(lst, seleccionados);
            seleccionados.Clear();

            //FILTRO TIPO DEUDA
            foreach (ListItem item in lstTipoDeuda.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(item.Text);
                }
            }
            if (seleccionados.Count != 0)
                lst = filtroTipoDeuda(lst, seleccionados);
            seleccionados.Clear();

            if (ddlFiltroDeuda.SelectedIndex > 0)
                lst = filtroMontos(lst);
            if (lst == null)
                return;

            fillGrillaDeuda(lst);
            lstFiltrada = lst;
            Session.Add("registros_notificar", lstFiltrada);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }

        protected void btnClearFiltros_ServerClick(object sender, EventArgs e)
        {
            ddlFecha.SelectedIndex = 0;
            ddlFiltroDeuda.SelectedIndex = 0;
            lstTipoDeuda.ClearSelection();
            lstBarrios.ClearSelection();
            lstZonas.ClearSelection();
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            txtMontoDesde.Text = string.Empty;
            txtMontoHasta.Text = string.Empty;
            List<DAL.MasivoDeudaInm> lst = BLL.MasivoDeudaInmBLL.read(Convert.ToDateTime("01/01/1900"), DateTime.Now,
                new List<string>());
            Session["LST"] = lst;
            fillGrillaDeuda(lst);

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
                Label lblNroCta = (Label)e.Row.FindControl("lblNroCta");
                DAL.MasivoDeudaInm obj = (DAL.MasivoDeudaInm)e.Row.DataItem;
                lblNroCta.Text = string.Format("{0}-{1}-{2}-{3}-{4}", obj.cir, obj.sec, obj.man, obj.par, obj.p_h);
            }
        }

        //Filtros
        private List<DAL.MasivoDeudaInm> filtroFecha(List<string> seleccionados)
        {
            try
            {
                int indice = ddlFecha.SelectedIndex;
                List<DAL.MasivoDeudaInm> lst = new List<DAL.MasivoDeudaInm>();

                switch (indice)
                {
                    case 0:
                        lst = BLL.MasivoDeudaInmBLL.read(Convert.ToDateTime("01/01/1900"), DateTime.Now, seleccionados);
                        Session["LST"] = lst;
                        break;
                    case 1:
                        if (txtDesde.Text == string.Empty)
                        {
                            lblError.Text = "Debe especificar la fecha a partir de la que necesita ver la deuda";
                            return null;
                        }
                        else
                        {
                            lblError.Text = string.Empty;
                            DateTime desde = Convert.ToDateTime(txtDesde.Text);
                            lst = BLL.MasivoDeudaInmBLL.read(desde, DateTime.Now, seleccionados);
                            Session["LST"] = lst;
                        }
                        break;
                    case 2:
                        if (txtHasta.Text == string.Empty)
                        {
                            lblError.Text = "Debe especificar la fecha hasta la cual necesita ver la deuda";
                            return null;
                        }
                        else
                        {
                            lblError.Text = string.Empty;
                            DateTime hasta = Convert.ToDateTime(txtHasta.Text);
                            lst = BLL.MasivoDeudaInmBLL.read(Convert.ToDateTime("01/01/1900"), hasta, seleccionados);
                            Session["LST"] = lst;
                        }
                        break;
                    case 3:
                        if (txtDesde.Text == string.Empty)
                        {
                            lblError.Text = "Debe especificar la fecha a partir de la que necesita ver la deuda";
                            return null;
                        }
                        if (txtHasta.Text == string.Empty)
                        {
                            lblError.Text = "Debe especificar la fecha hasta la cual necesita ver la deuda";
                            return null;
                        }

                        lblError.Text = string.Empty;
                        DateTime desde2 = Convert.ToDateTime(txtDesde.Text);
                        DateTime hasta2 = Convert.ToDateTime(txtHasta.Text);
                        lst = BLL.MasivoDeudaInmBLL.read(desde2, hasta2, seleccionados);
                        Session["LST"] = lst;
                        break;
                }
                return filtroMontoMayoraCero(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeudaInm> filtroBarrios(List<DAL.MasivoDeudaInm> lst, List<string> seleccionados)
        {
            try
            {
                lst = (from lista in lst
                       where seleccionados.Contains(lista.barrio)
                       select lista).ToList();
                return filtroMontoMayoraCero(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeudaInm> filtroZonas(List<DAL.MasivoDeudaInm> lst, List<string> seleccionados)
        {
            try
            {
                lst = (from lista in lst
                       where seleccionados.Contains(lista.zona)
                       select lista).ToList();
                return filtroMontoMayoraCero(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeudaInm> filtroMontos(List<DAL.MasivoDeudaInm> lst)
        {
            try
            {
                List<DAL.MasivoDeudaInm> lst2 = new List<DAL.MasivoDeudaInm>();

                decimal montos = 0;
                foreach (var item in lst)
                {
                    montos = 0;
                    if (lstTipoDeuda.Items[0].Selected)
                        montos += item.deudaJudicial;
                    if (lstTipoDeuda.Items[1].Selected)
                        montos += item.deudaPreJudicial;
                    if (lstTipoDeuda.Items[2].Selected)
                        montos += item.deudaAdministrativa;
                    if (lstTipoDeuda.Items[3].Selected)
                        montos += item.deudaNormal;
                    switch (ddlFiltroDeuda.SelectedIndex)
                    {
                        case 0:
                            lst2.Add(item);
                            break;
                        case 1:
                            if (txtMontoDesde.Text != string.Empty)
                            {
                                if (montos > Convert.ToDecimal(txtMontoDesde.Text))
                                    lst2.Add(item);
                            }
                            else
                            {
                                lblError.Text = "Debe indicar el monto para filtar";
                                return null;
                            }
                            break;
                        case 2:
                            if (txtMontoHasta.Text != string.Empty)
                            {
                                if (montos < Convert.ToDecimal(txtMontoHasta.Text))
                                    lst2.Add(item);
                            }
                            else
                            {
                                lblError.Text = "Debe indicar el monto para filtar";
                                return null;
                            }
                            break;
                        case 3:
                            if (txtMontoDesde.Text == string.Empty)
                            {
                                lblError.Text = "Debe indicar el monto para filtar";
                                return null;
                            }
                            if (txtMontoHasta.Text == string.Empty)
                            {
                                lblError.Text = "Debe indicar el monto para filtar";
                                return null;
                            }
                            lblError.Text = "Debe indicar el monto para filtar";
                            if (montos > Convert.ToDecimal(txtMontoDesde.Text) &&
                                montos < Convert.ToDecimal(txtMontoHasta.Text))
                                lst2.Add(item);
                            break;
                        default:
                            break;
                    }
                }

                return lst2;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeudaInm> filtroTipoDeuda(List<DAL.MasivoDeudaInm> lst, List<string> seleccionados)
        {
            try
            {
                List<DAL.MasivoDeudaInm> lst2 = new List<DAL.MasivoDeudaInm>();
                if (seleccionados.Contains("Deuda Judicial"))
                    lst2.AddRange((from deuda in lst
                                   where deuda.deudaJudicial > 0
                                   select deuda).ToList());

                if (seleccionados.Contains("Deuda Pre-Judicial"))
                    lst2.AddRange((from deuda in lst
                                   where deuda.deudaPreJudicial > 0
                                   select deuda).ToList());

                if (seleccionados.Contains("Deuda Administrativa"))
                    lst2.AddRange((from deuda in lst
                                   where deuda.deudaAdministrativa > 0
                                   select deuda).ToList());

                if (seleccionados.Contains("Deuda Normal"))
                    lst2.AddRange((from deuda in lst
                                   where deuda.deudaNormal > 0
                                   select deuda).ToList());

                return filtroMontoMayoraCero(lst2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DAL.MasivoDeudaInm> filtroMontoMayoraCero(List<DAL.MasivoDeudaInm> lst)
        {
            try
            {
                bool bandera = false;
                List<DAL.MasivoDeudaInm> lst2 = new List<DAL.MasivoDeudaInm>();

                foreach (var item in lst)
                {
                    if (item.deudaAdministrativa > 0)
                        bandera = true;
                    if (item.deudaJudicial > 0)
                        bandera = true;
                    if (item.deudaNormal > 0)
                        bandera = true;
                    if (item.deudaPreJudicial > 0)
                        bandera = true;

                    if (bandera)
                        lst2.Add(item);
                    bandera = false;
                }
                return lst2;
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
                lstFiltrada = (List<MasivoDeudaInm>)Session["registros_notificar"];

                Notificaciones_generales_cidi obj = new Notificaciones_generales_cidi();
                List<DAL.Detalle_notificaciones_generaes_cidi> lst =
                    new List<Detalle_notificaciones_generaes_cidi>();

                foreach (var item in lstFiltrada)
                {
                    if (item.cuil != null && item.cuil.Length == 11)
                    {
                        Detalle_notificaciones_generaes_cidi obj2 =
                            new Detalle_notificaciones_generaes_cidi();
                        obj2.cuit = item.cuil;
                        obj2.detalle_estado_cidi = string.Empty;
                        obj2.estado = "Sin Enviar";
                        obj2.fecha_primer_envio = null;
                        lst.Add(obj2);
                    }
                }
                BLL.Notificaciones_generales_cidi.insert(obj, lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
