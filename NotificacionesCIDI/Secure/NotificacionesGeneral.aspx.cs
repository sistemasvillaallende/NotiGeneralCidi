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
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}