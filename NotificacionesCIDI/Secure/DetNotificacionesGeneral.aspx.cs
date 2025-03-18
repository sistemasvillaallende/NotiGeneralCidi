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
                    fillNotas();
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

         private void fillNotas()
        {
            var listaNotas = BLL.NotasPlantillasBLL.read()
                   .Select(n => new { n.id, n.nom_plantilla, n.contenido })
                   .ToList();

            gvPlantilla.DataSource = listaNotas;
            gvPlantilla.DataBind();

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

        private List<DetalleNotificadorDTO> FilterData(List<DetalleNotificadorDTO> lst, int estadoCidi)
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


        protected void gvPlantilla_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtenemos el contenido y el ID
                string contenido = DataBinder.Eval(e.Row.DataItem, "contenido").ToString();
                string id = DataBinder.Eval(e.Row.DataItem, "id").ToString();

                // Seteamos atributos data- en el HTML de la fila
                e.Row.Attributes["data-contenido"] = contenido;
                e.Row.Attributes["data-id"] = id;

                // Esto hace que el cursor cambie y sea clickeable
                e.Row.Attributes["style"] = "cursor:pointer;";
            }
        }


        protected void gvPlantilla_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }

}
