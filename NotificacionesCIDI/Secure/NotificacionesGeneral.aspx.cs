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

                var lst = BLL.NotificacionGeneralBLL.readNotificacionBySubsistema(subsistema);

                var dtoList = new List<NotificadorGeneralDTO>();

                foreach (var notif in lst)
                {
                    var detallesPorEmision = BLL.DetNotificacionGenetalBLL.readDetNotNotificaciones(notif.Nro_Emision);

                    int sinNotificar = detallesPorEmision.Count(d => d.Cod_estado_cidi == 0);
                    int notificadas = detallesPorEmision.Count(d => d.Cod_estado_cidi == 1);
                    int rechazadas = detallesPorEmision.Count(d => d.Cod_estado_cidi == 2);

                    dtoList.Add(new NotificadorGeneralDTO
                    {
                        Nro_Emision = notif.Nro_Emision,
                        Fecha_Emision = notif.Fecha_Emision,
                        Descripcion = notif.Descripcion,
                        Subsistema = notif.subsistema,
                        Cantidad_Reg = notif.Cantidad_Reg,
                        SinNotificar = sinNotificar,
                        Notificadas = notificadas,
                        Rechazadas = rechazadas
                    });
                }

                gvMasivosAut.DataSource = dtoList;
                gvMasivosAut.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}