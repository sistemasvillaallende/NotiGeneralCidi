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
using System.IO;

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
                fillZonas();
                fillNotas();
                fillCalles();
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
            lstZonas.DataTextField = "categoria";
            lstZonas.DataValueField = "categoria";
            lstZonas.DataBind();
        }

        private void fillCalles()
        {
            lstCalles.DataSource = BLL.CallesBLL.readAll();
            lstCalles.DataTextField = "NOM_CALLE";
            lstCalles.DataValueField = "COD_CALLE";
            lstCalles.DataBind();
        }


        private void fillGrilla(int cod_barrio, int cod_calle, string cod_zona, int desde, int hasta)
        {
            lstFiltrada = BLL.MasivoDeudaGeneralBLL.readWithFilters(cod_barrio, cod_calle, cod_zona, desde, hasta);
            gvDeuda.DataSource = lstFiltrada;
            gvDeuda.DataBind();
            gvDeuda.UseAccessibleHeader = true;
            Session.Add("registros_notificar", lstFiltrada);
        }


        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {
            List<DAL.MasivoDeudaGeneral> lst = new List<DAL.MasivoDeudaGeneral>();

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

            fillGrilla(cod_barrio, cod_calle, cod_zona, desde, hasta);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }

        protected void btnClearFiltros_ServerClick(object sender, EventArgs e)
        {
            try
            {
                txtDesde.Text = "";
                txtHasta.Text = "";
                lstBarrios.ClearSelection();
                lstCalles.ClearSelection();
                lstZonas.ClearSelection();


                lstFiltrada = new List<DAL.MasivoDeudaGeneral>();
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
                        EnviarNotificacion(contenidoPersonalizado, item.cuit);
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


        /////////////////////////////// EXPORT EXCEL /////////////////////////////////////////


        protected void btnExportExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                lstFiltrada = (List<DAL.MasivoDeudaGeneral>)Session["registros_notificar"];

                if (lstFiltrada == null || lstFiltrada.Count == 0)
                {
                    lblError.Text = "No hay datos para exportar.";
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=DeudaGeneral_Export_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";

                Page.EnableViewState = false;

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    gvDeuda.AllowPaging = false;
                    gvDeuda.DataSource = lstFiltrada;
                    gvDeuda.DataBind();

                    gvDeuda.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                    gvDeuda.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
                    gvDeuda.RowStyle.BackColor = System.Drawing.Color.White;

                    gvDeuda.RenderControl(hw);
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