using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace NotificacionesCIDI.Secure
{
    public partial class NotificacionesGeneral : System.Web.UI.Page
    {
        int subsistema;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    subsistema = Convert.ToInt32(Request.QueryString["subsistema"]);
                    fillGrillas(subsistema);
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
        public void fillGrillas(int subsistema)
        {
            try
            {
                List<NotificacionGeneral> lst = BLL.NotificacionGeneralBLL.readNotificacionBySubsistema(subsistema);
                gvMasivosAut.DataSource = lst;
                gvMasivosAut.DataBind();
                // Forzar que el encabezado se renderice en <thead>
                if (gvMasivosAut.HeaderRow != null)
                {
                    gvMasivosAut.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}