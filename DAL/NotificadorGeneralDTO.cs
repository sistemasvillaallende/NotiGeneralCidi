using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NotificadorGeneralDTO
    {
      
            public int Nro_Emision { get; set; }
            public DateTime Fecha_Emision { get; set; }
            public string Descripcion { get; set; }
            public int Subsistema { get; set; }
            public int Cantidad_Reg { get; set; }
            public int SinNotificar { get; set; }     
            public int Notificadas { get; set; }       
            public int Rechazadas { get; set; }         
        
    }
}
