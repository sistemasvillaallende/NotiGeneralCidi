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
using System.Web.Services;
using System.Text;

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

            }
        }

        [System.Web.Services.WebMethod]
        public static string ObtenerPlantillas()
        {
            string urlBase = ConfigurationManager.AppSettings["urlBase"];
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
                return response.Content;
            }
            else
            {
                return "[]";
            }
        }

        [System.Web.Services.WebMethod]
        public static string GuardarPlantillaEnSesion(int idPlantilla)
        {
            try
            {
                HttpContext.Current.Session["PlantillaSeleccionada"] = idPlantilla;
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        [WebMethod]
        public static string ProcesarSeleccionados(List<ModeloNotificacionGeneral> seleccionados)
        {
            try
            {
                HttpContext.Current.Session["seleccionados"] = seleccionados;

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: a" + ex.Message;
            }
        }




        protected void btnFiltros_ServerClick(object sender, EventArgs e)
        {
            List<DAL.MasivoDeudaGeneral> lst = new List<DAL.MasivoDeudaGeneral>();
            divFiltros.Visible = false;
            divResultados.Visible = true;
        }



        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {
                List<ModeloNotificacionGeneral> todosLosRegistros = (List<ModeloNotificacionGeneral>)Session["registros_notificar"];

                List<ModeloNotificacionGeneral> lstFiltrada = new List<ModeloNotificacionGeneral>();

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
                int idPlantilla = Convert.ToInt32(Session["PlantillaSeleccionada"]);
                if (Session["PlantillaSeleccionada"] == null)
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
                    if (item.Cuit != null && item.Cuit.Length == 11)
                    {
                        DetNotificacionGeneral obj2 = new DetNotificacionGeneral();
                        obj2.Nro_Emision = obj.Nro_Emision;
                        obj2.Nro_Notificacion = nroNotificacion;
                        nroNotificacion++;
                        obj2.Cuit = item.Cuit;
                        obj2.Nombre = $"{item.Nombre} {item.Apellido}";
                        obj2.Dominio = item.Denominacion;
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

        protected void btnImportExcel_Click(object sender, EventArgs e)
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
               "$('#modalImportExcel').modal('show');", true);


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
                    List<ModeloNotificacionGeneral> lst = new List<ModeloNotificacionGeneral>();
                    lst = pasarALst(dt);
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


        private List<ModeloNotificacionGeneral> pasarALst(DataTable dt)
        {
            List<ModeloNotificacionGeneral> lst = new List<ModeloNotificacionGeneral>();
            string cuit = String.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ModeloNotificacionGeneral obj = new ModeloNotificacionGeneral();
                if (dt.Rows[i]["cuit"] != DBNull.Value)
                {
                    obj.Cuit = Convert.ToString(dt.Rows[i]["cuit"]);
                }

                if (dt.Rows[i]["Nombre"] != DBNull.Value)
                {
                    obj.Nombre = Convert.ToString(dt.Rows[i]["Nombre"]);
                }

                if (dt.Rows[i]["Apellido"] != DBNull.Value)
                {
                    obj.Apellido = Convert.ToString(dt.Rows[i]["Apellido"]);
                }

                if (dt.Rows[i]["Domicilio"] != DBNull.Value)
                {
                    obj.Domicilio = Convert.ToString(dt.Rows[i]["Domicilio"]);
                }

                if (dt.Rows[i]["Denominacion"] != DBNull.Value)
                {
                    obj.Denominacion = Convert.ToString(dt.Rows[i]["Denominacion"]);
                }
                lst.Add(obj);
            }

            return lst;
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

        protected void btnDescargarModelo_Click(object sender, EventArgs e)
        {
            try
            {
                GenerarExcelConHtmlTextWriter();
            }
            catch (Exception ex)
            {
                // falta maejar mejor este error
                Response.Write($"<script>alert('Error al generar el archivo: {ex.Message}');</script>");
            }
        }

        private void GenerarExcelConHtmlTextWriter()
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Modelo_Datos.xls");

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    hw.WriteLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                    hw.WriteLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                    hw.WriteLine("xmlns=\"http://www.w3.org/TR/REC-html40\">");
                    hw.WriteLine("<head>");
                    hw.WriteLine("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
                    hw.WriteLine("<meta name=ProgId content=Excel.Sheet>");
                    hw.WriteLine("<meta name=Generator content=\"Microsoft Excel 15\">");
                    hw.WriteLine("<!--[if gte mso 9]><xml>");
                    hw.WriteLine("<x:ExcelWorkbook>");
                    hw.WriteLine("<x:ExcelWorksheets>");
                    hw.WriteLine("<x:ExcelWorksheet>");
                    hw.WriteLine("<x:Name>Modelo_Datos</x:Name>");
                    hw.WriteLine("<x:WorksheetOptions><x:DefaultRowHeight>285</x:DefaultRowHeight></x:WorksheetOptions>");
                    hw.WriteLine("</x:ExcelWorksheet>");
                    hw.WriteLine("</x:ExcelWorksheets>");
                    hw.WriteLine("</x:ExcelWorkbook>");
                    hw.WriteLine("</xml><![endif]-->");
                    hw.WriteLine("</head>");
                    hw.WriteLine("<body>");
                    hw.WriteLine("<table>");

                    hw.WriteLine("<tr>");
                    string[] headers = { "CUIT", "Nombre", "Apellido", "Domicilio", "Denominacion" };

                    foreach (string header in headers)
                    {
                        hw.WriteLine($"<td>{header}</td>");
                    }
                    hw.WriteLine("</tr>");

                    hw.WriteLine("</table>");
                    Response.Write(sw.ToString());
                }
            }

            Response.End();
        }

    }
}