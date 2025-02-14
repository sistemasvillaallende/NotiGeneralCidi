using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class BARRIOS : DALBase
    {
        public int COD_BARRIO { get; set; }
        public string NOM_BARRIO { get; set; }

        public BARRIOS()
        {
            NOM_BARRIO = string.Empty;
            COD_BARRIO = 0;
        }

        private static List<BARRIOS> mapeo(SqlDataReader dr)
        {
            List<BARRIOS> lst = new List<BARRIOS>();
            BARRIOS obj;
            if (dr.HasRows)
            {
                int COD_BARRIO = dr.GetOrdinal("COD_BARRIO");
                int NOM_BARRIO = dr.GetOrdinal("NOM_BARRIO");

                while (dr.Read())
                {
                    obj = new BARRIOS();
                    if (!dr.IsDBNull(COD_BARRIO)) { obj.COD_BARRIO = dr.GetInt32(COD_BARRIO); }
                    if (!dr.IsDBNull(NOM_BARRIO)) { obj.NOM_BARRIO = dr.GetString(NOM_BARRIO); }

                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<BARRIOS> read()
        {
            try
            {
                List<BARRIOS> lst = new List<BARRIOS>();
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT *FROM BARRIOS ORDER BY NOM_BARRIO";

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

        public static BARRIOS getByPk(int COD_BARRIO)
        {
            try
            {
                BARRIOS obj = null;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT *FROM BARRIOS 
                          WHERE COD_BARRIO=@COD_BARRIO";

                    cmd.Parameters.AddWithValue("@COD_BARRIO", COD_BARRIO);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<BARRIOS> lst = mapeo(dr);
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

        public static int insert(BARRIOS obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO BARRIOS(");
                sql.AppendLine("COD_BARRIO");
                sql.AppendLine(", NOM_BARRIO");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@COD_BARRIO");
                sql.AppendLine(", @NOM_BARRIO");
                sql.AppendLine(")");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@COD_BARRIO", obj.COD_BARRIO);
                    cmd.Parameters.AddWithValue("@NOM_BARRIO", obj.NOM_BARRIO);
                    cmd.Connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(BARRIOS obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  BARRIOS SET");
                sql.AppendLine("NOM_BARRIO=@NOM_BARRIO");
                sql.AppendLine("WHERE");
                sql.AppendLine("COD_BARRIO=@COD_BARRIO");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@COD_BARRIO", obj.COD_BARRIO);
                    cmd.Parameters.AddWithValue("@NOM_BARRIO", obj.NOM_BARRIO);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int COD_BARRIO)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  BARRIOS ");
                sql.AppendLine("WHERE");
                sql.AppendLine("COD_BARRIO=@COD_BARRIO");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@COD_BARRIO", COD_BARRIO);
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
