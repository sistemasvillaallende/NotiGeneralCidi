using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace NotificacionesCIDI
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected HtmlGenericControl liNombre;
        protected HtmlGenericControl liApellido;
        protected HtmlGenericControl mnuPcNombre;
        protected HtmlGenericControl mnuPcApellido;
        protected HtmlGenericControl SpanOficina;
        protected HtmlGenericControl mnuPcCuit;
        protected HtmlGenericControl mnuPcNivelCidi;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["VABack.CIDI"] == null || string.IsNullOrEmpty(Request.Cookies["VABack.CIDI"].Value))
            {
                Response.Redirect("Error.aspx");
                return;
            }
            if (!IsPostBack)
            {
                HttpCookie userCookie = Request.Cookies["VABack.CIDI"];
                if (userCookie != null)
                {
                    var parsed = HttpUtility.ParseQueryString(userCookie.Value);
                    if (liNombre != null)
                        liNombre.InnerText = parsed["nombre"] ?? liNombre.InnerText;
                    if (liApellido != null)
                        liApellido.InnerText = parsed["apellido"] ?? liApellido.InnerText;
                    if (mnuPcNombre != null)
                        mnuPcNombre.InnerText = parsed["nombre"] ?? mnuPcNombre.InnerText;
                    if (mnuPcApellido != null)
                        mnuPcApellido.InnerText = parsed["apellido"] ?? mnuPcApellido.InnerText;
                    if (SpanOficina != null)
                        SpanOficina.InnerText = parsed["nombre_oficina"] ?? SpanOficina.InnerText;
                    if (mnuPcCuit != null)
                        mnuPcCuit.InnerText = parsed["cuit_formateado"] ?? mnuPcCuit.InnerText;
                    if (mnuPcNivelCidi != null)
                        mnuPcNivelCidi.InnerText = (parsed["administrador"] == "1" ? "Administrador" : "Usuario");
                }
            }
        }

        protected void btnCerraSession_ServerClick(object sender, EventArgs e)
        {
            // Eliminar la cookie "VABack.CIDI"
            if (Request.Cookies["VABack.CIDI"] != null)
            {
                HttpCookie cookie = new HttpCookie("VABack.CIDI");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            Session.Abandon();
            Response.Redirect("Error.aspx");
        }
    }
}