using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace NotificacionesCIDI.Secure
{
    public partial class MasivoDeudaGeneral : System.Web.UI.Page
    {
        List<DAL.MasivoDeudaGeneral> lstFiltrada = null;
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
            if (listaNotas.Count > 0)
            {
                gvPlantilla.UseAccessibleHeader = true;
                gvPlantilla.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }


        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {
            List<DAL.MasivoDeudaGeneral> lst = new List<DAL.MasivoDeudaGeneral>();

            //fillGrilla(cod_barrio, cod_calle, cod_zona, desde, hasta);
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }




        protected void gvDeuda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {
                List<DAL.MasivoDeudaGeneral> todosLosRegistros = (List<DAL.MasivoDeudaGeneral>)Session["registros_notificar"];

                List<DAL.MasivoDeudaGeneral> lstFiltrada = new List<DAL.MasivoDeudaGeneral>();

                foreach (GridViewRow row in gvConceptos.Rows)
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
                if (fUploadConceptos.HasFile)
                {
                    lblFileError.Visible = false;
                    ImportExcel();

                    lblUploadStatus.Text = "Archivo  subido exitosamente";
                    lblUploadStatus.Visible = true;
                }
                else
                {
                    lblFileError.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
               "$('#modalConceptos').modal('show');", true);


                }
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
                    Session["registros_notificar"] = lst;
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
            string cuit = String.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DAL.MasivoDeudaGeneral obj = new DAL.MasivoDeudaGeneral();
                if (dt.Rows[i]["cuit"] != DBNull.Value)
                {
                    cuit = Convert.ToString(dt.Rows[i]["cuit"]);

                    obj = GetVecinoDigitalByCuit(cuit);

                    if(obj != null)
                    {
                    lstConcepto.Add(obj);

                    }

                }
            }


            return lstConcepto;
        }

        private DAL.MasivoDeudaGeneral GetVecinoDigitalByCuit(string cuit)
        {
            try
            {
                DAL.MasivoDeudaGeneral vd = null;
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new RestClient(options);
                var request = new RestRequest($"NotificadorGenerico/getVecinoDigitalByCuit?cuit={cuit}", Method.Get);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    vd = JsonConvert.DeserializeObject<DAL.MasivoDeudaGeneral>(response.Content);
                }
                return vd;

            }
            catch (Exception)
            {
                throw;
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
                    gvConceptos.AllowPaging = false;
                    gvConceptos.DataSource = lstFiltrada;
                    gvConceptos.DataBind();
                    gvConceptos.Columns[0].Visible = false;
                    gvConceptos.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                    gvConceptos.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
                    gvConceptos.RowStyle.BackColor = System.Drawing.Color.White;

                    gvConceptos.RenderControl(hw);
                    gvConceptos.Columns[0].Visible = true;
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