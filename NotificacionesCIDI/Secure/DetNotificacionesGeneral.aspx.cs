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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 if (!IsPostBack)
                 {
                    nro_emision = Convert.ToInt32(Request.QueryString["nro_emision"]);
                    fillGrillas(nro_emision);
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
        public void fillGrillas(int nro_emision)
        {
            try
            {
                List<DetNotificacionGeneral> lst = BLL.DetNotificacionGenetalBLL.readDetNotNotificaciones(nro_emision);
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