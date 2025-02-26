using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using ClosedXML.Excel;

namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeudaGeneral : System.Web.UI.Page
    {
        List<DAL.MasivoDeudaGeneral> lstFiltrada = null;
        List<DAL.Detalle_notificaciones_generaes_cidi> lstDetalle = null;
        Notificaciones_generales_cidi objNoti = new Notificaciones_generales_cidi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillBarrios();
            }
        }
        private void fillBarrios()
        {
            lstBarrios.DataSource = BLL.BarriosBLL.read();
            lstBarrios.DataTextField = "nom_barrio";
            lstBarrios.DataValueField = "cod_barrio";
            lstBarrios.DataBind();
        }


        protected void gvDeuda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDeuda.PageIndex = e.NewPageIndex;
            List<DAL.MasivoDeudaGeneral> lst = BLL.MasivoDeudaGeneralBLL.read(10);
            Session["LST"] = lst;
            fillGrilla(lst);
        }

        private void fillGrilla(List<DAL.MasivoDeudaGeneral> lst)
        {
            lstFiltrada = lst;
            gvDeuda.DataSource = lst;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
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

        // boton para importar excel

        private void ImportExcelToGridView(string fileP, string extension, string ishdr)
        {

        }
        protected void btnImportExcel_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {
            List<string> seleccionados = new List<string>();
            List<DAL.MasivoDeudaGeneral> lst = new List<DAL.MasivoDeudaGeneral>();

            //FILTRO Barrios
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


            fillGrilla(lst);
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
            List<DAL.MasivoDeudaGeneral> lst = BLL.MasivoDeudaGeneralBLL.read(10);
            Session["LST"] = lst;

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
            // if (e.Row.RowType == DataControlRowType.DataRow)
            // {
            //     Label lblNroCta = (Label)e.Row.FindControl("lblNroCta");
            //     DAL.MasivoDeudaInm obj = (DAL.MasivoDeudaInm)e.Row.DataItem;
            //     lblNroCta.Text = string.Format("{0}-{1}-{2}-{3}-{4}", obj.cir, obj.sec, obj.man, obj.par, obj.p_h);
            // }
        }



        private List<DAL.MasivoDeudaGeneral> filtroBarrios(List<DAL.MasivoDeudaGeneral> lst, List<string> seleccionados)
        {
            try
            {
                // lst = (from lista in lst
                //        where seleccionados.Contains(lista.barrio)
                //        select lista).ToList();
                // return lst;
                //   lst = BLL.MasivoDeudaGeneralBLL(seleccionados);
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
                lstFiltrada = (List<DAL.MasivoDeudaGeneral>)Session["registros_notificar"];

                Notificaciones_generales_cidi obj = new Notificaciones_generales_cidi();
                List<DAL.Detalle_notificaciones_generaes_cidi> lst =
                    new List<Detalle_notificaciones_generaes_cidi>();

                foreach (var item in lstFiltrada)
                {
                    if (item.cuit != null && item.cuit.Length == 11)
                    {
                        Detalle_notificaciones_generaes_cidi obj2 =
                            new Detalle_notificaciones_generaes_cidi();
                        obj2.cuit = item.cuit;
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

        protected string uploadFile(FileUpload fU, string entidad)
        {
            string ret = "nodisponible.png";
            try
            {
                string path = Server.MapPath(entidad + "/");
                if (fU.HasFile)
                {
                    try
                    {
                        string nombreImagen = fU.FileName;
                        fU.PostedFile.SaveAs(path + nombreImagen);
                        ret = nombreImagen;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnConceptos_x_legajos_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ImportExcel()
        {
           
            string nombreArchivo = uploadFile(fUploadConceptos, "archivos");
            string path = Server.MapPath("archivos/" + nombreArchivo);
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        //Agrego a esta columna que es para agregarle botones para acciones
                        //dt.Columns.Add("Accion");
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            if (cell.Value.ToString().Length > 0)
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                            //else
                            //break;
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    List<DAL.MasivoDeudaGeneral> lst = new List<DAL.MasivoDeudaGeneral>();
                    lst = pasarALstConcepto2(dt);
                    Session["Detalle"] = lst;
                    gvConceptos.DataSource = lst;
                    gvConceptos.DataBind();
                    if (gvConceptos.Rows.Count > 0)
                    {
                        gvConceptos.UseAccessibleHeader = true;
                        gvConceptos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }


        private List<DAL.MasivoDeudaGeneral> pasarALstConcepto2(DataTable dt)
        {
            List<DAL.MasivoDeudaGeneral> lstConcepto = new List<DAL.MasivoDeudaGeneral>();
              
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DAL.MasivoDeudaGeneral lst = new DAL.MasivoDeudaGeneral();
                if (dt.Rows[i]["cuit"] != DBNull.Value)
                {
                    //lst.nombre = Convert.ToString(dt.Rows[i]["nombre"]);
                    lst.cuit = Convert.ToString(dt.Rows[i]["cuit"]);
                    //lst.nom_calle = Convert.ToString(dt.Rows[i]["nom_calle"]);
             
                    //lst.barrio = Convert.ToString(dt.Rows[i]["barrio"]);
                 
                    lstConcepto.Add(lst);
                }
            }
            return lstConcepto;
        }



        protected void gvConceptos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Page")
                    return;
                if (e.CommandName == "delete")
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // protected void gvConceptos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        // {
        //     gvConceptos.PageIndex = e.NewPageIndex;
        // }

        protected void gvConceptos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            // int index = Convert.ToInt32(e.RowIndex);
            // List<Entities.Conceptos> lst = (List<Entities.Conceptos>)Session["Detalle"];
            // lst.RemoveAt(index);
            // Session["Detalle"] = lst;
            // fillGrillaConceptos(lst);

        }

/// MODAL DE LA PLANTILLA
/// 


  protected void btnGenerar_Click(object sender, EventArgs e)
        {

        }

    }
}