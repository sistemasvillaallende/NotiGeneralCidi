using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace NotificacionesCIDI.Secure
{
    public partial class DetNotificacionesGeneral : System.Web.UI.Page
    {

        int nro_emision;
        int subsistema;
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
                    fillGrillas(nro_emision, subsistema);
                    fillPlantilla(nro_emision);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    


        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {

        }
        public void fillGrillas(int nro_emision, int subsistema)
        {
            try
            {
                List<DetalleNotificadorDTO> lst = BLL.DetNotificacionGenetalBLL.getBySubsistemaDTO(nro_emision, subsistema);
                gvMasivosAut.DataSource = lst;
                gvMasivosAut.DataBind();

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

            List<DetalleNotificadorDTO> lst = BLL.DetNotificacionGenetalBLL.getBySubsistemaDTO(nro_emision, subsistema);

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

        protected void fillPlantilla(int nro_emision){

            NotificacionGeneral not = BLL.NotificacionGeneralBLL.readNotificacionByNroEmision(nro_emision);

            NotasPlantillas plantilla = BLL.NotasPlantillasBLL.getByPk(not.id_plantilla);

            divContenido.InnerHtml = plantilla.contenido;
            //  divContenido.InnerHtml = "<p>Texto de prueba simple</p>";  
        }

    }

}
