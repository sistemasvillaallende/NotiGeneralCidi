using System;
using System.Web.UI;

namespace NotificacionesCIDI
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ...existing code...
        }

        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {
            // Add your session closing logic here
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}