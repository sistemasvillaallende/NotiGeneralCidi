using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Detalle_notificaciones_generaes_cidi : DALBase
    {
        public int id { get; set; }
        public int id_notificacion { get; set; }
        public string cuit { get; set; }
        public DateTime? fecha_primer_envio { get; set; }
        public string estado { get; set; }
        public string detalle_estado_cidi { get; set; }

        public Detalle_notificaciones_generaes_cidi()
        {
            id = 0;
            id_notificacion = 0;
            cuit = string.Empty;
            fecha_primer_envio = DateTime.Now;
            estado = string.Empty;
            detalle_estado_cidi = string.Empty;
        }

        private static List<Detalle_notificaciones_generaes_cidi> mapeo(SqlDataReader dr)
        {
            List<Detalle_notificaciones_generaes_cidi> lst = new List<Detalle_notificaciones_generaes_cidi>();
            Detalle_notificaciones_generaes_cidi obj;
            if (dr.HasRows)
            {
                int id = dr.GetOrdinal("id");
                int id_notificacion = dr.GetOrdinal("id_notificacion");
                int cuit = dr.GetOrdinal("cuit");
                int fecha_primer_envio = dr.GetOrdinal("fecha_primer_envio");
                int estado = dr.GetOrdinal("estado");
                int detalle_estado_cidi = dr.GetOrdinal("detalle_estado_cidi");
                while (dr.Read())
                {
                    obj = new Detalle_notificaciones_generaes_cidi();
                    if (!dr.IsDBNull(id)) { obj.id = dr.GetInt32(id); }
                    if (!dr.IsDBNull(id_notificacion)) { obj.id_notificacion = dr.GetInt32(id_notificacion); }
                    if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                    if (!dr.IsDBNull(fecha_primer_envio)) { obj.fecha_primer_envio = dr.GetDateTime(fecha_primer_envio); }
                    if (!dr.IsDBNull(estado)) { obj.estado = dr.GetString(estado); }
                    if (!dr.IsDBNull(detalle_estado_cidi)) { obj.detalle_estado_cidi = dr.GetString(detalle_estado_cidi); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Detalle_notificaciones_generaes_cidi> read()
        {
            try
            {
                List<Detalle_notificaciones_generaes_cidi> lst = new List<Detalle_notificaciones_generaes_cidi>();
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM Detalle_notificaciones_generaes_cidi";
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Detalle_notificaciones_generaes_cidi getByPk(
        int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM Detalle_notificaciones_generaes_cidi WHERE");
                sql.AppendLine("id = @id");
                Detalle_notificaciones_generaes_cidi obj = null;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Detalle_notificaciones_generaes_cidi> lst = mapeo(dr);
                    if (lst.Count != 0)
                        obj = lst[0];
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int insert(Detalle_notificaciones_generaes_cidi obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Detalle_notificaciones_generaes_cidi(");
                sql.AppendLine("id_notificacion");
                sql.AppendLine(", cuit");
                sql.AppendLine(", fecha_primer_envio");
                sql.AppendLine(", estado");
                sql.AppendLine(", detalle_estado_cidi");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@id_notificacion");
                sql.AppendLine(", @cuit");
                sql.AppendLine(", @fecha_primer_envio");
                sql.AppendLine(", @estado");
                sql.AppendLine(", @detalle_estado_cidi");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id_notificacion", obj.id_notificacion);
                    cmd.Parameters.AddWithValue("@cuit", obj.cuit);
                    cmd.Parameters.AddWithValue("@fecha_primer_envio", obj.fecha_primer_envio);
                    cmd.Parameters.AddWithValue("@estado", obj.estado);
                    cmd.Parameters.AddWithValue("@detalle_estado_cidi", obj.detalle_estado_cidi);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(Detalle_notificaciones_generaes_cidi obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  Detalle_notificaciones_generaes_cidi SET");
                sql.AppendLine("id_notificacion=@id_notificacion");
                sql.AppendLine(", cuit=@cuit");
                sql.AppendLine(", fecha_primer_envio=@fecha_primer_envio");
                sql.AppendLine(", estado=@estado");
                sql.AppendLine(", detalle_estado_cidi=@detalle_estado_cidi");
                sql.AppendLine("WHERE");
                sql.AppendLine("id=@id");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id_notificacion", obj.id_notificacion);
                    cmd.Parameters.AddWithValue("@cuit", obj.cuit);
                    cmd.Parameters.AddWithValue("@fecha_primer_envio", obj.fecha_primer_envio);
                    cmd.Parameters.AddWithValue("@estado", obj.estado);
                    cmd.Parameters.AddWithValue("@detalle_estado_cidi", obj.detalle_estado_cidi);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Detalle_notificaciones_generaes_cidi obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  Detalle_notificaciones_generaes_cidi ");
                sql.AppendLine("WHERE");
                sql.AppendLine("id=@id");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ConvertirDetalleEnDataTable(List<Detalle_notificaciones_generaes_cidi> detalles)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("cuit", typeof(string));
            dataTable.Columns.Add("fecha_primer_envio", typeof(DateTime));
            dataTable.Columns.Add("estado", typeof(string));
            dataTable.Columns.Add("detalle_estado_cidi", typeof(string));

            foreach (var detalle in detalles)
            {
                dataTable.Rows.Add(detalle.cuit, detalle.fecha_primer_envio, 
                    detalle.estado, detalle.detalle_estado_cidi);
            }

            return dataTable;
        }

    }
}

