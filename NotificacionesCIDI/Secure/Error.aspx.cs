using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotificacionesCIDI.Secure
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["VABack.CIDI"];
            if (cookie != null)
            {
                Response.Redirect("~/Secure/Inicio.aspx", true);
            }
        }
    }
}