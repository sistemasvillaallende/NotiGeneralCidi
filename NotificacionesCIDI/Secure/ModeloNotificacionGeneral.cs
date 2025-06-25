using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificacionesCIDI.Secure
{
    public class ModeloNotificacionGeneral
    {
        public string Cuit { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }      
        public string Domicilio { get; set; }
        public string Denominacion { get; set; }
    }
}