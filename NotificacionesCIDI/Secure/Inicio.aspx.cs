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

