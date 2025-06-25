using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Newtonsoft.Json;
using RestSharp;

namespace NotificacionesCIDI.Secure
{
    public partial class DetNotificacionesGeneral : System.Web.UI.Page
    {

        int nro_emision;
        public int subsistema;
        string urlBase = ConfigurationManager.AppSettings["urlBase"];
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    nro_emision = Convert.ToInt32(Request.QueryString["nro_emision"]);
                    subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                    Session["nro_emision"] = nro_emision;
                    Session["subsistema"] = subsistema;
                    switch (subsistema)
                    {
                        case 1:
                            spanIcono.Attributes.Remove("class");
                            spanIcono.Attributes.Add("class", "fa fa-home");
                            spanSubsistema.InnerHtml = "Inmuebles";
                            break;
                        case 2:
                            break;
                        case 3:
                            spanIcono.Attributes.Remove("class");
                            spanIcono.Attributes.Add("class", "fa fa-industry");
                            spanSubsistema.InnerHtml = "Industria y Comercio";
                            break;
                        case 4:
                            spanIcono.Attributes.Remove("class");
                            spanIcono.Attributes.Add("class", "fa fa-car-side");
                            spanSubsistema.InnerHtml = "Automotores";
                            break;
                        case 8:
                            spanIcono.Attributes.Remove("class");
                            spanIcono.Attributes.Add("class", "fa fa-globe");
                            spanSubsistema.InnerHtml = "General";
                            break;
                        case 20:
                            spanIcono.Attributes.Remove("class");
                            spanIcono.Attributes.Add("class", "fa fa-user");
                            spanSubsistema.InnerHtml = "Personal";
                            break;
                        default:
                            break;
                    }
                    FillPlantilla(nro_emision);
                    CargarDetallesNotificaciones(nro_emision, subsistema);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void fillGrillas(List<DAL.DetalleNotificadorDTO> lst)
        {
            try
            {
                gvNotGeneral.DataSource = lst;
                gvNotGeneral.DataBind();
                if (lst.Count > 0)
                {
                    gvNotGeneral.UseAccessibleHeader = true;
                    gvNotGeneral.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string GetEstadoCidi(object estado)
        {
            int estadoCidi = Convert.ToInt32(estado);
            switch (estadoCidi)
            {
                case 0:
                    return "Sin Notificar";
                case 1:
                    return "Notificado";
                case 2:
                    return "Rechazado";
                default:
                    return "Desconocido";
            }
        }
        protected void DDLEstEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt32(DDLEstEnv.SelectedValue);
            int nro_emision = Convert.ToInt32(Session["nro_emision"]);
            int subsistema = Convert.ToInt32(Session["subsistema"]);
            List<DAL.DetalleNotificadorDTO> lst = null;

            var options = new RestClientOptions(urlBase)
            {
                MaxTimeout = -1,
                RemoteCertificateValidationCallback = (senderr, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"DetalleNotificador/getBySubsistema?nro_emision={nro_emision}&subsistema={subsistema}", Method.Get);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                lst = JsonConvert.DeserializeObject<List<DAL.DetalleNotificadorDTO>>(response.Content);
            }

            List<DetalleNotificadorDTO> filteredList = FilterData(lst, selectedValue);
            gvNotGeneral.DataSource = filteredList;
            gvNotGeneral.DataBind();
        }

        protected List<DetalleNotificadorDTO> FilterData(List<DetalleNotificadorDTO> lst, int estadoCidi)
        {
            {
                if (estadoCidi != -1)
                {
                    lst = lst.Where(x => x.Cod_estado_cidi == estadoCidi).ToList();

                    return lst;
                }
                else
                {
                    return lst;
                }
            }
        }

        public void CargarDetallesNotificaciones(int nro_emision, int subsistema)
        {
            try
            {
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };
                var client = new RestClient(options);
                var request = new RestRequest($"DetalleNotificador/getBySubsistema?nro_emision={nro_emision}&subsistema={subsistema}", Method.Get);

                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {

                    List<DAL.DetalleNotificadorDTO> lst = JsonConvert.DeserializeObject<List<DAL.DetalleNotificadorDTO>>(response.Content);
                    fillGrillas(lst);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.Message}");
                throw;
            }
        }


        public void FillPlantilla(int nro_emision)
        {
            NotificacionGeneral notificacion = null;
            NotasPlantillas plantilla = null;

            var options = new RestClientOptions(urlBase)
            {
                MaxTimeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"NotificadorGeneral/readNotificacionByNroEmision?nro_emision={nro_emision}", Method.Get);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {

               notificacion = JsonConvert.DeserializeObject<DAL.NotificacionGeneral>(response.Content);
            }
            var requestPlantilla = new RestRequest($"Plantillas/getPlantillaByPk?id={notificacion.id_plantilla}", Method.Get);
            RestResponse responsePlantilla = client.Execute(requestPlantilla);

            if (responsePlantilla.IsSuccessful && !string.IsNullOrEmpty(responsePlantilla.Content))
            {

                 plantilla = JsonConvert.DeserializeObject<DAL.NotasPlantillas>(responsePlantilla.Content);

            }

            divContenido.InnerHtml = plantilla.contenido;
            Session.Add("plantilla", plantilla.contenido);
        }


        protected void btnGenerarNoti_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string contenidoPlantilla = Convert.ToString(Session["plantilla"]);
                int nroEmision = Convert.ToInt32(Session["nro_emision"]);
                int subsistema = Convert.ToInt32(Session["subsistema"]);
                string subsistemaNombre = GetSubsistema(subsistema);
                List<DAL.DetNotificacionGeneral> lstCompleted = CargarDetallesNotificacionesCompleta(nroEmision);
                List<DAL.DetNotificacionGeneral> lstSeleccionados = new List<DAL.DetNotificacionGeneral>();
                string denominacion = null;

                foreach (GridViewRow row in gvNotGeneral.Rows)
                {
                    CheckBox cb = (CheckBox)row.FindControl("chkSelect");
                    if (cb != null && cb.Checked)
                    {
                        int nroNotificacion = Convert.ToInt32(row.Cells[1].Text);
                        denominacion = row.Cells[3].Text;
                        var registroEncontrado = lstCompleted.FirstOrDefault(item => item.Nro_Notificacion == nroNotificacion);
                        if (registroEncontrado != null)
                        {
                            lstSeleccionados.Add(registroEncontrado);
                        }
                    }
                }

                string nombre, apellido;
                foreach (var item in lstSeleccionados)
                {
                    if (item.Cuit.Trim() != null && item.Cuit.Trim().Length == 11)
                    {
                        string nombreCompleto = item.Nombre;
                        SepararNombreApellido(nombreCompleto, out nombre, out apellido);
                        string contenidoPersonalizado = ReemplazarVariables(contenidoPlantilla, nombre, apellido, item.Cuit,denominacion );
                        EnviarNotificacion(contenidoPersonalizado, item.Cuit, nroEmision, item.Nro_Notificacion,subsistemaNombre);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Tuve que colocar este switch ya que el endpoint arriba no me trae PERSONAL y GENERAL
        private string GetSubsistema(object subsistema)
        {
            int sub = Convert.ToInt32(subsistema);
            switch (sub)
            {

                case 1:
                    return "Inmueble";
                case 2:
                    return "Facturacion";
                case 3:
                    return "Industria y Comercio";
                case 4:
                    return "Automotor";
                case 8:
                    return "General";
                case 20:
                    return "Recursos humanos";
                default:
                    return "Desconocido";
            }
        }


        private string ReemplazarVariables(string plantilla, string nombre, string apellido, string cuit, string denominacion)
        {
            string resultado = plantilla;
            resultado = resultado.Replace("{nombre}", nombre);
            resultado = resultado.Replace("{apellido}", apellido);
            resultado = resultado.Replace("{cuit}", cuit);
            resultado = resultado.Replace("{denominación}", denominacion);
            return resultado;
        }

        private void EnviarNotificacion(string contenidoPersonalizado, string cuit, int Nro_Emision, int Nro_notificacion, string subsistemaNombre)
        {
            try
            {
                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                EnvioNotificacion objDet = new EnvioNotificacion();
                objDet.cuerpoNotif=contenidoPersonalizado;
                objDet.cuit_filter = cuit;
                objDet.Nro_notificacion = Nro_notificacion;
                objDet.Nro_Emision = Nro_Emision;
                objDet.subsistema=subsistemaNombre;

                var json = new JavaScriptSerializer().Serialize(objDet);
                var client = new RestClient(options);

                var request = new RestRequest($"/EnvioNotificacionCIDI/enviarNotificacion", Method.Post);

                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                                string cookieHeaderValue = HttpContext.Current.Request.Headers["Cookie"];
                request.AddHeader("Cookie", cookieHeaderValue);
                request.AddParameter("data", json); // esto lo pone en el cuerpo como form-data

                var response = client.Execute(request); // SINCRÓNICO

                if (response.IsSuccessful)
                {
                    string contenido = response.Content;
                    // procesar respuesta
                }
                else
                {
                    // manejar error
                    string error = response.ErrorMessage;
                }
                /*var client = new RestClient(options);
                var request = new RestRequest($"/EnvioNotificacionCIDI/enviarNotificacion?cuerpoNotif={contenidoPersonalizado}&cuit_filter={cuit}&Nro_Emision={Nro_Emision}&Nro_notificacion={Nro_notificacion}&subsistemaNombre={subsistemaNombre}", Method.Post);
                string cookieHeaderValue = HttpContext.Current.Request.Headers["Cookie"];
                request.AddHeader("Cookie", cookieHeaderValue);

                RestResponse response = client.Execute(request);*/

            }
            catch (Exception)
            {
                throw;
            }


        }

        public List<DAL.DetNotificacionGeneral> CargarDetallesNotificacionesCompleta(int nro_emision)
        {
            try
            {
                List<DAL.DetNotificacionGeneral> lst = null;

                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };
                var client = new RestClient(options);
                var request = new RestRequest($"DetalleNotificador/readDetNotNotificaciones?nro_emision={nro_emision}", Method.Get);

                RestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {

                     lst = JsonConvert.DeserializeObject<List<DAL.DetNotificacionGeneral>>(response.Content);

                }

                return lst;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.Message}");
                throw;
            }
        }

        public void SepararNombreApellido(string nombreCompleto, out string nombre, out string apellido)
        {
            string[] palabras = nombreCompleto.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (palabras.Length == 0)
            {
                nombre = string.Empty;
                apellido = string.Empty;
                return;
            }

            if (palabras.Length == 1)
            {
                nombre = palabras[0];
                apellido = string.Empty;
                return;
            }

            if (palabras.Length == 2)
            {
                nombre = palabras[0];
                apellido = palabras[1];
                return;
            }

            if (palabras.Length == 4)
            {
                nombre = string.Join(" ", palabras, 0, 2);
                apellido = string.Join(" ", palabras, 2, 2);
                return;
            }

            nombre = string.Join(" ", palabras, 0, palabras.Length - 1);
            apellido = palabras[palabras.Length - 1];
        }
    }
}

