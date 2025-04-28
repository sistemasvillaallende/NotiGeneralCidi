using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;


namespace NotificacionesCIDI.Secure
{
    public partial class Inicio : Page
    {

        // ...existing code...
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    fillGrillas();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // HACER UN ENDPOINT PARA SUBSISTEMAS DESPUES
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
                default:
                    return "Desconocido";
            }
        }
        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {

        }
        public void fillGrillas()
        {
            try
            {
                List<NotificacionGeneral> lst = BLL.NotificacionGeneralBLL.readNotificacionGeneral();
                gvMasivosAut.DataSource = lst;
                gvMasivosAut.DataBind();
                // Add header row as thead for DataTables
                if (gvMasivosAut.HeaderRow != null)
                {
                    gvMasivosAut.HeaderRow.TableSection = System.Web.UI.WebControls.TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

