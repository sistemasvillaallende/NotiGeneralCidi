using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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
        int subsistema;
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
                gvMasivosAut.DataSource = lst;
                gvMasivosAut.DataBind();
                if (lst.Count > 0)
                {
                    gvMasivosAut.UseAccessibleHeader = true;
                    gvMasivosAut.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            gvMasivosAut.DataSource = filteredList;
            gvMasivosAut.DataBind();
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

        }
    }
}

