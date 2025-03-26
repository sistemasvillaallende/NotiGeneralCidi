using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using ClosedXML.Excel;
using BLL;

namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeudaGeneral : System.Web.UI.Page
    {
        List<DAL.MasivoDeudaGeneral> lstFiltrada = null;
        int subsistema;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                Session["subsistema"] = subsistema;
                fillBarrios();
                fillNotas();
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
            lstBarrios.ClearSelection();
            List<DAL.MasivoDeudaGeneral> lst = BLL.MasivoDeudaGeneralBLL.read(10);
            Session["LST"] = lst;
            // Refresca la grilla con la lista completa
            fillGrilla(lst);
            Session["registros_notificar"] = lst;

            Session["Detalle"] = null;
            gvConceptos.DataSource = null;
            gvConceptos.DataBind();
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

        protected void gvDeuda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        private List<DAL.MasivoDeudaGeneral> filtroBarrios(List<DAL.MasivoDeudaGeneral> lst, List<string> seleccionados)
        {
            try
            {
                lst = BLL.MasivoDeudaGeneralBLL.read(seleccionados);
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
        protected void gvConceptos_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

        private void fillNotas()
        {
            var listaNotas = BLL.NotasPlantillasBLL.read()
                   .Select(n => new { n.id, n.nom_plantilla, n.contenido })
                   .ToList();

            gvPlantilla.DataSource = listaNotas;
            gvPlantilla.DataBind();

        }




    }
}