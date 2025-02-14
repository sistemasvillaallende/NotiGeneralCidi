using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ZONAS:DALBase
    {
        public int cod_zona { get; set; }
        public string nom_zona { get; set; }
        public decimal tasa_basica_edificado { get; set; }
        public decimal excedente_edificado { get; set; }
        public decimal tasa_basica_baldio { get; set; }
        public decimal excedente_baldio { get; set; }

        public ZONAS()
        {
            cod_zona = 0;
            nom_zona = string.Empty;
            tasa_basica_edificado = 0;
            excedente_edificado = 0;
            tasa_basica_baldio = 0;
            excedente_baldio = 0;
        }

        private static List<ZONAS> mapeo(SqlDataReader dr)
        {
            List<ZONAS> lst = new List<ZONAS>();
            ZONAS obj;
            if (dr.HasRows)
            {
                int cod_zona = dr.GetOrdinal("cod_zona");
                int nom_zona = dr.GetOrdinal("nom_zona");
                int tasa_basica_edificado = dr.GetOrdinal("tasa_basica_edificado");
                int excedente_edificado = dr.GetOrdinal("excedente_edificado");
                int tasa_basica_baldio = dr.GetOrdinal("tasa_basica_baldio");
                int excedente_baldio = dr.GetOrdinal("excedente_baldio");
                while (dr.Read())
                {
                    obj = new ZONAS();
                    if (!dr.IsDBNull(cod_zona)) { obj.cod_zona = dr.GetInt32(cod_zona); }
                    if (!dr.IsDBNull(nom_zona)) { obj.nom_zona = dr.GetString(nom_zona); }
                    if (!dr.IsDBNull(tasa_basica_edificado)) { obj.tasa_basica_edificado = dr.GetDecimal(tasa_basica_edificado); }
                    if (!dr.IsDBNull(excedente_edificado)) { obj.excedente_edificado = dr.GetDecimal(excedente_edificado); }
                    if (!dr.IsDBNull(tasa_basica_baldio)) { obj.tasa_basica_baldio = dr.GetDecimal(tasa_basica_baldio); }
                    if (!dr.IsDBNull(excedente_baldio)) { obj.excedente_baldio = dr.GetDecimal(excedente_baldio); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<ZONAS> read()
        {
            try
            {
                List<ZONAS> lst = new List<ZONAS>();
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM Zonas";
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

        public static ZONAS getByPk(int cod_zona)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM Zonas WHERE");
                sql.AppendLine("cod_zona = @cod_zona");
                ZONAS obj = null;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@cod_zona", cod_zona);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<ZONAS> lst = mapeo(dr);
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

        public static int insert(ZONAS obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Zonas(");
                sql.AppendLine("cod_zona");
                sql.AppendLine(", nom_zona");
                sql.AppendLine(", tasa_basica_edificado");
                sql.AppendLine(", excedente_edificado");
                sql.AppendLine(", tasa_basica_baldio");
                sql.AppendLine(", excedente_baldio");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@cod_zona");
                sql.AppendLine(", @nom_zona");
                sql.AppendLine(", @tasa_basica_edificado");
                sql.AppendLine(", @excedente_edificado");
                sql.AppendLine(", @tasa_basica_baldio");
                sql.AppendLine(", @excedente_baldio");
                sql.AppendLine(")");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@cod_zona", obj.cod_zona);
                    cmd.Parameters.AddWithValue("@nom_zona", obj.nom_zona);
                    cmd.Parameters.AddWithValue("@tasa_basica_edificado", obj.tasa_basica_edificado);
                    cmd.Parameters.AddWithValue("@excedente_edificado", obj.excedente_edificado);
                    cmd.Parameters.AddWithValue("@tasa_basica_baldio", obj.tasa_basica_baldio);
                    cmd.Parameters.AddWithValue("@excedente_baldio", obj.excedente_baldio);
                    cmd.Connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(ZONAS obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  Zonas SET");
                sql.AppendLine("nom_zona=@nom_zona");
                sql.AppendLine(", tasa_basica_edificado=@tasa_basica_edificado");
                sql.AppendLine(", excedente_edificado=@excedente_edificado");
                sql.AppendLine(", tasa_basica_baldio=@tasa_basica_baldio");
                sql.AppendLine(", excedente_baldio=@excedente_baldio");
                sql.AppendLine("WHERE");
                sql.AppendLine("cod_zona=@cod_zona");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@cod_zona", obj.cod_zona);
                    cmd.Parameters.AddWithValue("@nom_zona", obj.nom_zona);
                    cmd.Parameters.AddWithValue("@tasa_basica_edificado", obj.tasa_basica_edificado);
                    cmd.Parameters.AddWithValue("@excedente_edificado", obj.excedente_edificado);
                    cmd.Parameters.AddWithValue("@tasa_basica_baldio", obj.tasa_basica_baldio);
                    cmd.Parameters.AddWithValue("@excedente_baldio", obj.excedente_baldio);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int cod_zona)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  Zonas ");
                sql.AppendLine("WHERE");
                sql.AppendLine("cod_zona=@cod_zona");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@cod_zona", cod_zona);
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
