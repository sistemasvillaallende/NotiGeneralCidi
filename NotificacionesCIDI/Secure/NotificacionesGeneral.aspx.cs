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
    public partial class NotificacionesGeneral : System.Web.UI.Page
    {
        int subsistema;
        string urlBase = ConfigurationManager.AppSettings["urlBase"];
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                    CargarNotificacionGeneral(subsistema);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void fillGrillas(List<DAL.NotificacionGeneral> lst, int subsistema)
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
                Console.WriteLine($"Error en fillGrillas: {ex.Message}");
                throw;
            }
        }

        protected string GetSubsistema(object subsistema)
        {
            int sub = Convert.ToInt32(subsistema);
            switch (sub)
            {
                
                case 1:
                    return "INMUEBLES";
                case 2:
                    return "FACTURACION";
                case 3:
                    return "INDYCOM";
                case 4:
                    return "AUTOMOTORES";
                case 8:
                    return "GENERAL";
                case 20:
                    return "PERSONAL";
                default:
                    return "Desconocido";
            }
        }

        public void CargarNotificacionGeneral(int subsistema)
        {
            try
            {

                var options = new RestClientOptions(urlBase)
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };
                var client = new RestClient(options);
                var request = new RestRequest("NotificadorGeneral/greadNotificacionBySubsistema?subsistema=" + subsistema, Method.Get);

                RestResponse response = client.Execute(request);


                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {

                    List<DAL.NotificacionGeneral> lst = JsonConvert.DeserializeObject<List<DAL.NotificacionGeneral>>(response.Content);
                    fillGrillas(lst,subsistema);
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
