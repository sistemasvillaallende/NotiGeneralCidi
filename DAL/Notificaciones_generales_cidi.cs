using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Notificaciones_generales_cidi : DALBase
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int id_paltilla { get; set; }
        public string texto { get; set; }
        public DateTime fecha { get; set; }
        public int usuario_notifica { get; set; }

        public Notificaciones_generales_cidi()
        {
            id = 0;
            nombre = string.Empty;
            descripcion = string.Empty;
            id_paltilla = 0;
            texto = string.Empty;
            fecha = DateTime.Now;
            usuario_notifica = 0;
        }

        private static List<Notificaciones_generales_cidi> mapeo(SqlDataReader dr)
        {
            List<Notificaciones_generales_cidi> lst = new List<Notificaciones_generales_cidi>();
            Notificaciones_generales_cidi obj;
            if (dr.HasRows)
            {
                int id = dr.GetOrdinal("id");
                int nombre = dr.GetOrdinal("nombre");
                int descripcion = dr.GetOrdinal("descripcion");
                int id_paltilla = dr.GetOrdinal("id_paltilla");
                int texto = dr.GetOrdinal("texto");
                int fecha = dr.GetOrdinal("fecha");
                int usuario_notifica = dr.GetOrdinal("usuario_notifica");
                while (dr.Read())
                {
                    obj = new Notificaciones_generales_cidi();
                    if (!dr.IsDBNull(id)) { obj.id = dr.GetInt32(id); }
                    if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                    if (!dr.IsDBNull(descripcion)) { obj.descripcion = dr.GetString(descripcion); }
                    if (!dr.IsDBNull(id_paltilla)) { obj.id_paltilla = dr.GetInt32(id_paltilla); }
                    if (!dr.IsDBNull(texto)) { obj.texto = dr.GetString(texto); }
                    if (!dr.IsDBNull(fecha)) { obj.fecha = dr.GetDateTime(fecha); }
                    if (!dr.IsDBNull(usuario_notifica)) { obj.usuario_notifica = dr.GetInt32(usuario_notifica); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Notificaciones_generales_cidi> read()
        {
            try
            {
                List<Notificaciones_generales_cidi> lst = new List<Notificaciones_generales_cidi>();
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM Notificaciones_generales_cidi";
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

        public static Notificaciones_generales_cidi getByPk(
        int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM Notificaciones_generales_cidi WHERE");
                sql.AppendLine("id = @id");
                Notificaciones_generales_cidi obj = null;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Notificaciones_generales_cidi> lst = mapeo(dr);
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

        public static int insert(Notificaciones_generales_cidi obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Notificaciones_generales_cidi(");
                sql.AppendLine("nombre");
                sql.AppendLine(", descripcion");
                sql.AppendLine(", id_paltilla");
                sql.AppendLine(", texto");
                sql.AppendLine(", fecha");
                sql.AppendLine(", usuario_notifica");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@nombre");
                sql.AppendLine(", @descripcion");
                sql.AppendLine(", @id_paltilla");
                sql.AppendLine(", @texto");
                sql.AppendLine(", @fecha");
                sql.AppendLine(", @usuario_notifica");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@id_paltilla", obj.id_paltilla);
                    cmd.Parameters.AddWithValue("@texto", obj.texto);
                    cmd.Parameters.AddWithValue("@fecha", obj.fecha);
                    cmd.Parameters.AddWithValue("@usuario_notifica", obj.usuario_notifica);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsertarMaestroDetalle(Notificaciones_generales_cidi obj,
            DataTable lst)
        {
            using (SqlConnection con = getConnection())
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_recepcion_recoleciones";

                // Parámetros para la tabla maestra
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                cmd.Parameters.AddWithValue("@id_plantilla", obj.id_paltilla);
                cmd.Parameters.AddWithValue("@texto", obj.texto);
                cmd.Parameters.AddWithValue("@fecha", obj.fecha);
                cmd.Parameters.AddWithValue("@usuario_notifica", obj.usuario_notifica);

                // Parámetro para el detalle
                var parametroDetalle = new SqlParameter
                {
                    ParameterName = "@detalle",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.DetalleType",
                    Value = lst
                };

                cmd.Parameters.Add(parametroDetalle);
                cmd.Connection.Open();
                // Ejecutar el procedimiento almacenado
                cmd.ExecuteNonQuery();

            }
        }

        public static void update(Notificaciones_generales_cidi obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  Notificaciones_generales_cidi SET");
                sql.AppendLine("nombre=@nombre");
                sql.AppendLine(", descripcion=@descripcion");
                sql.AppendLine(", id_paltilla=@id_paltilla");
                sql.AppendLine(", texto=@texto");
                sql.AppendLine(", fecha=@fecha");
                sql.AppendLine(", usuario_notifica=@usuario_notifica");
                sql.AppendLine("WHERE");
                sql.AppendLine("id=@id");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@id_paltilla", obj.id_paltilla);
                    cmd.Parameters.AddWithValue("@texto", obj.texto);
                    cmd.Parameters.AddWithValue("@fecha", obj.fecha);
                    cmd.Parameters.AddWithValue("@usuario_notifica", obj.usuario_notifica);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Notificaciones_generales_cidi obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  Notificaciones_generales_cidi ");
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

    }
}

