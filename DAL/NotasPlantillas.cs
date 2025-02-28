using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL
{
    public class NotasPlantillas : DALBase
    {
        public int id { get; set; }
        public DateTime fecha_alta { get; set; }
        public int id_subsistema { get; set; }
        public string nom_plantilla { get; set; }
        public int id_secretaria { get; set; }
        public int id_oficina { get; set; }
        public string contenido { get; set; }


        public NotasPlantillas()
        {
            id = 0;
            fecha_alta = DateTime.Now;
            id_subsistema = 0;
            nom_plantilla = string.Empty;
            id_secretaria = 0;
            id_oficina = 0;
            contenido = string.Empty;
        }


        public static List<NotasPlantillas> mapeo(SqlDataReader dr)
        {
            List<NotasPlantillas> lst = new List<NotasPlantillas>();
            NotasPlantillas obj;
            if (dr.HasRows)
            {
                int id = dr.GetOrdinal("id");
                int fecha_alta = dr.GetOrdinal("fecha_alta");
                int id_subsistema = dr.GetOrdinal("id_subsistema");
                int nom_plantilla = dr.GetOrdinal("nom_plantilla");
                int id_secretaria = dr.GetOrdinal("id_secretaria");
                int id_oficina = dr.GetOrdinal("id_oficina");
                int contenido = dr.GetOrdinal("contenido");
                while (dr.Read())
                {
                    obj = new NotasPlantillas();
                    if (!dr.IsDBNull(id)) { obj.id = dr.GetInt32(id); }
                    if (!dr.IsDBNull(fecha_alta)) { obj.fecha_alta = dr.GetDateTime(fecha_alta); }
                    if (!dr.IsDBNull(id_subsistema)) { obj.id_subsistema = dr.GetInt32(id_subsistema); }
                    if (!dr.IsDBNull(nom_plantilla)) { obj.nom_plantilla = dr.GetString(nom_plantilla); }
                    if (!dr.IsDBNull(id_secretaria)) { obj.id_secretaria = dr.GetInt32(id_secretaria); }
                    if (!dr.IsDBNull(id_oficina)) { obj.id_oficina = dr.GetInt32(id_oficina); }
                    if (!dr.IsDBNull(contenido)) { obj.contenido = dr.GetString(contenido); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<NotasPlantillas> read()
        {
            try
            {
                List<NotasPlantillas> lst = new List<NotasPlantillas>();
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM CIDI_PLANTILLAS ";
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

        public static NotasPlantillas getByPk(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM FROM CIDI_PLANTILLAS WHERE");
                sql.AppendLine("id = @id");
                NotasPlantillas obj = null;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<NotasPlantillas> lst = mapeo(dr);
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

        public static void insert(NotasPlantillas obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO CIDI_PLANTILLAS (");
                sql.AppendLine("id, fecha_alta, id_subsistema, nom_plantilla, id_secretaria, id_oficina, contenido");
                sql.AppendLine(") VALUES (");
                sql.AppendLine("@id, @fecha_alta, @id_subsistema, @nom_plantilla, @id_secretaria, @id_oficina, @contenido");
                sql.AppendLine(");");

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@fecha_alta", obj.fecha_alta);
                    cmd.Parameters.AddWithValue("@id_subsistema", obj.id_subsistema);
                    cmd.Parameters.AddWithValue("@nom_plantilla", obj.nom_plantilla);
                    cmd.Parameters.AddWithValue("@id_secretaria", obj.id_secretaria);
                    cmd.Parameters.AddWithValue("@id_oficina", obj.id_oficina);
                    cmd.Parameters.AddWithValue("@contenido", obj.contenido);
                    cmd.Connection.Open();
                    cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void update(NotasPlantillas obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE CIDI_PLANTILLAS SET");
                sql.AppendLine("fecha_alta = @fecha_alta,");
                sql.AppendLine("id_subsistema = @id_subsistema,");
                sql.AppendLine("nom_plantilla = @nom_plantilla,");
                sql.AppendLine("id_secretaria = @id_secretaria,");
                sql.AppendLine("id_oficina = @id_oficina,");
                sql.AppendLine("contenido = @contenido");
                sql.AppendLine("WHERE id = @id");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@fecha_alta", obj.fecha_alta);
                    cmd.Parameters.AddWithValue("@id_subsistema", obj.id_subsistema);
                    cmd.Parameters.AddWithValue("@nom_plantilla", obj.nom_plantilla);
                    cmd.Parameters.AddWithValue("@id_secretaria", obj.id_secretaria);
                    cmd.Parameters.AddWithValue("@id_oficina", obj.id_oficina);
                    cmd.Parameters.AddWithValue("@contenido", obj.contenido);
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

        public static void delete(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  CIDI_PLANTILLAS ");
                sql.AppendLine("WHERE");
                sql.AppendLine("id=@id");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static int GetMaxId()
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    string query = "SELECT ISNULL(MAX(id), 0) FROM CIDI_PLANTILLAS";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el ID máximo", ex);
            }
        }



    }
}
