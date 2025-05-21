using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificacionesCIDI.Secure
{
    public class EnvioNotificacion
    {
        public string cuerpoNotif { get; set; }
        public string cuit_filter { get; set; }
        public int Nro_Emision { get; set; }
        public int Nro_notificacion { get; set; }
        public string subsistema { get; set; }
    }
}