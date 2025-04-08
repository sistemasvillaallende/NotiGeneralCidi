using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using RestSharp;


namespace NotificacionesCIDI.Secure
{
    public partial class Plantillas : System.Web.UI.Page
    {
        string urlBase = ConfigurationManager.AppSettings["urlBase"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPlantillas();
            }
        }

        protected void GridPlantillas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GridPlantillas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            int id = Convert.ToInt32(e.CommandArgument);


            if (commandName == "Editar")
            {

                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (senderr, certificate, chain, sslPolicyErrors) => true
                };
                var client = new RestClient(options);
                var request = new RestRequest($"Plantillas/getPlantillaByPk?id={id}", Method.Get);
                RestResponse response = client.Execute(request);


                var plantilla = JsonConvert.DeserializeObject<DAL.NotasPlantillas>(response.Content);

                if (plantilla != null)
                {

                    Session["EditPlantillaId"] = id;

                    string contenidoEscapado = HttpUtility.JavaScriptStringEncode(plantilla.contenido);

                    string script = $@"
               $(document).ready(function() {{
                   $('#plantillaModalEditar').modal('show');
                   setTimeout(function() {{
                       setQuillContent('{contenidoEscapado}');
                   }}, 500); 
               }});
           ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenEditModal", script, true);
                }
            }


            if (commandName == "Eliminar")
            {

                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (senderr, certificate, chain, sslPolicyErrors) => true
                };
                var client = new RestClient(options);
                var request = new RestRequest($"Plantillas/deletePlantilla?id={id}", Method.Delete);
                RestResponse response = client.Execute(request);


                CargarPlantillas();

                ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModalExito", @"
               var modal = new bootstrap.Modal(document.getElementById('exampleModalDelete'));
               modal.show();

               document.querySelector('#exampleModalDelete .btn-close').addEventListener('click', function() {
                   modal.hide();
               });

               document.querySelector('#exampleModalDelete .btn-secondary').addEventListener('click', function() {
                   modal.hide();
               });
           ", true);
            }
        }

        private void savePlantillas(NotasPlantillas notas)
        {
            try
            {
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                var client = new RestClient(options);

                var requestMaxId = new RestRequest("Plantillas/getMaxId", Method.Get);
                RestResponse responseMaxId = client.Execute(requestMaxId);

                if (responseMaxId.IsSuccessful && !string.IsNullOrEmpty(responseMaxId.Content))
                {
                    int idMax = JsonConvert.DeserializeObject<int>(responseMaxId.Content);
                    notas.id = idMax + 1;

                    var requestInsert = new RestRequest("Plantillas/insertNuevaPlantilla", Method.Post);
                    requestInsert.AddJsonBody(notas);

                    RestResponse responseInsert = client.Execute(requestInsert);

                    if (!responseInsert.IsSuccessful)
                    {
                        Console.WriteLine($"Error al insertar plantilla: {responseInsert.StatusCode} - {responseInsert.ErrorMessage}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error al obtener ID máximo: {responseMaxId.StatusCode} - {responseMaxId.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al guardar plantilla: {ex.Message}");
                throw;
            }
        }

        protected void btnGuardarNota_Click(object sender, EventArgs e)
        {
            string contenido = MyHiddenControl2.Value;
            string nombreNota = MyHiddenControl.Value;
            string contenido2 = hiddenInput2.Text;


            if (string.IsNullOrWhiteSpace(nombreNota))
            {
                nombreNota = "Sin nombre";
            }

            NotasPlantillas notas = new NotasPlantillas();
            notas.nom_plantilla = nombreNota;
            notas.contenido = contenido;
            notas.id_subsistema = 8;

            savePlantillas(notas);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["EditPlantillaId"]);

            string contenido = hiddenInput3.Text;

            BLL.NotasPlantillasBLL.ActualizarContenido(id, contenido);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "$('#plantillaModalEditar').modal('hide');", true);

            Response.Redirect(Request.RawUrl);

        }

        public void fillGrillas(List<DAL.NotasPlantillas> lst)
        {
            try
            {
                GridPlantillas.DataSource = lst;
                GridPlantillas.DataBind();

                if (lst.Count > 0)
                {
                    GridPlantillas.UseAccessibleHeader = true;
                    GridPlantillas.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en fillGrillas: {ex.Message}");
                throw;
            }
        }

        public void CargarPlantillas()
        {
            try
            {

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

                    List<DAL.NotasPlantillas> lst = JsonConvert.DeserializeObject<List<DAL.NotasPlantillas>>(response.Content);
                    fillGrillas(lst);
                }
                else
                {
                    Console.WriteLine($"Error en la petición: {response.StatusCode} - {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.Message}");
                throw;
            }
        }
    }


}

