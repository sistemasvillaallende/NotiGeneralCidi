using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class NotificacionGeneral : DALBase
    {

        public int Nro_Emision { get; set; }
        public DateTime Fecha_Emision { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public int Cod_tipo_notificacion { get; set; }
        public string Descripcion { get; set; }
        public int subsistema { get; set; }
        public int Cantidad_Reg { get; set; }
        public decimal Total { get; set; }
        public decimal Porcentaje { get; set; }


        public NotificacionGeneral()
        {
            Nro_Emision = 0;
            Fecha_Emision = DateTime.Now;
            Fecha_Vencimiento = DateTime.Now;
            Cod_tipo_notificacion = 0;
            Descripcion = string.Empty;
            subsistema = 0;
            Cantidad_Reg = 0;
            Total = 0;
            Porcentaje = 0;
        }

        public static List<NotificacionGeneral> read()
        {
            try
            {
                List<NotificacionGeneral> lst = new List<NotificacionGeneral>();
                NotificacionGeneral obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM CIDI_NOTIFICACION_GENERAL order by Nro_Emision ";
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            obj = new NotificacionGeneral();
                            obj.Nro_Emision = dr.GetInt32(0);
                            if (!dr.IsDBNull(1)) { obj.Fecha_Emision = dr.GetDateTime(1); }
                            if (!dr.IsDBNull(2)) { obj.Fecha_Vencimiento = dr.GetDateTime(2); }
                            if (!dr.IsDBNull(3)) { obj.Cod_tipo_notificacion = dr.GetInt32(3); }
                            if (!dr.IsDBNull(4)) { obj.Descripcion = dr.GetString(4); }
                            if (!dr.IsDBNull(5)) { obj.subsistema = dr.GetInt32(5); }
                            if (!dr.IsDBNull(6)) { obj.Cantidad_Reg = dr.GetInt32(6); }
                            if (!dr.IsDBNull(7)) { obj.Total = dr.GetDecimal(7); }
                            if (!dr.IsDBNull(8)) { obj.Porcentaje = dr.GetDecimal(8); }
                            lst.Add(obj);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
