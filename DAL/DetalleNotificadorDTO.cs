using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DetalleNotificadorDTO : DALBase
    {
        public int Nro_Notificacion { get; set; }
        public int Cod_estado_cidi { get; set; }
        public string Denominacion { get; set; }
        public string Cuit { get; set; }
        public string Nombre { get; set; }

        public DetalleNotificadorDTO() 
        {
            Nro_Notificacion = 0;
            Cod_estado_cidi = 0;
            Denominacion = string.Empty;
            Cuit = string.Empty;
            Nombre = string.Empty;
        }

    }
}
