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
        public int id_plantilla { get; set; }


        public NotificacionGeneral()
        {
            Nro_Emision = 0;
            Fecha_Emision = DateTime.Now;
            Fecha_Vencimiento = DateTime.Now.AddMonths(1);
            Cod_tipo_notificacion = 0;
            Descripcion = string.Empty;
            subsistema = 0;
            Cantidad_Reg = 0;
            Total = 0;
            Porcentaje = 0;
            id_plantilla = 0;
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
                            if (!dr.IsDBNull(9)) { obj.id_plantilla = dr.GetInt32(9); }
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


        public static List<NotificacionGeneral> readBySubsistema(int subsistema)
        {
            try
            {
                List<NotificacionGeneral> lst = new List<NotificacionGeneral>();
                NotificacionGeneral obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM CIDI_NOTIFICACION_GENERAL where Subsistema = @subsistema";
                    cmd.Parameters.AddWithValue("@subsistema", subsistema);
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
                            if (!dr.IsDBNull(9)) { obj.id_plantilla = dr.GetInt32(9); }
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

        public static NotificacionGeneral readByNroEmision(int nro_emision)
        {
            try
            {
                NotificacionGeneral obj = null;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM CIDI_NOTIFICACION_GENERAL where Nro_Emision = @nro_emision";
                    cmd.Parameters.AddWithValue("@Nro_Emision", nro_emision);
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read()) 
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
                            if (!dr.IsDBNull(9)) { obj.id_plantilla = dr.GetInt32(9); }
                            
                        
                    }
                }
                      return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insert(NotificacionGeneral obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO CIDI_NOTIFICACION_GENERAL(");
                sql.AppendLine("  Nro_Emision");
                sql.AppendLine(", Fecha_Emision");
                sql.AppendLine(", Fecha_Vencimiento");
                sql.AppendLine(", Cod_tipo_notificacion");
                sql.AppendLine(", Descripcion");
                sql.AppendLine(", subsistema");
                sql.AppendLine(", Cantidad_Reg");
                sql.AppendLine(", Total");
                sql.AppendLine(", Porcentaje");
                sql.AppendLine(", id_plantilla");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("  @Nro_Emision");
                sql.AppendLine(", @Fecha_Emision");
                sql.AppendLine(", @Fecha_Vencimiento");
                sql.AppendLine(", @Cod_tipo_notificacion");
                sql.AppendLine(", @Descripcion");
                sql.AppendLine(", @subsistema");
                sql.AppendLine(", @Cantidad_Reg");
                sql.AppendLine(", @Total");
                sql.AppendLine(", @Porcentaje");
                sql.AppendLine(", @id_plantilla");
                sql.AppendLine(")");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@Nro_Emision", obj.Nro_Emision);
                    cmd.Parameters.AddWithValue("@Fecha_Emision", obj.Fecha_Emision);
                    cmd.Parameters.AddWithValue("@Fecha_Vencimiento", obj.Fecha_Vencimiento);
                    cmd.Parameters.AddWithValue("@Cod_tipo_notificacion", obj.Cod_tipo_notificacion);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@subsistema", obj.subsistema);
                    cmd.Parameters.AddWithValue("@Cantidad_Reg", obj.Cantidad_Reg);
                    cmd.Parameters.AddWithValue("@Total", obj.Total);
                    cmd.Parameters.AddWithValue("@Porcentaje", obj.Porcentaje);
                    cmd.Parameters.AddWithValue("@id_plantilla", obj.id_plantilla);
                    cmd.Connection.Open();
                    cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetMaxNroEmision()
        {
            try
            {
                int maxNroEmision = 0;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT ISNULL(MAX(Nro_Emision), 0) FROM CIDI_NOTIFICACION_GENERAL";

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        maxNroEmision = Convert.ToInt32(result);
                    }
                }

                return maxNroEmision;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
