using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class CALLES : DALBase
    {
        public int COD_CALLE { get; set; }
        public string NOM_CALLE { get; set; }
        public int? COD_BARRIO { get; set; }

        public CALLES()
        {
            COD_CALLE = 0;
            NOM_CALLE = string.Empty;
            COD_BARRIO = 0;
        }

        private static List<CALLES> mapeo(SqlDataReader dr)
        {
            List<CALLES> lst = new List<CALLES>();
            CALLES obj;
            if (dr.HasRows)
            {
                int COD_CALLE = dr.GetOrdinal("COD_CALLE");
                int NOM_CALLE = dr.GetOrdinal("NOM_CALLE");
                int COD_BARRIO = dr.GetOrdinal("COD_BARRIO");

                while (dr.Read())
                {
                    obj = new CALLES();
                    if (!dr.IsDBNull(COD_CALLE)) { obj.COD_CALLE = dr.GetInt32(COD_CALLE); }
                    if (!dr.IsDBNull(NOM_CALLE)) { obj.NOM_CALLE = dr.GetString(NOM_CALLE); }
                    if (!dr.IsDBNull(COD_BARRIO)) { obj.COD_BARRIO = dr.GetInt32(COD_BARRIO); }

                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<CALLES> read(int COD_BARRIO)
        {
            try
            {
                List<CALLES> lst = new List<CALLES>();
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"  SELECT A.COD_CALLE, A.NOM_CALLE, B.COD_BARRIO  FROM Calles A
                          INNER JOIN CALLES_X_BARRIO cxb ON A.COD_CALLE = cxb.cod_calle 
                          INNER JOIN BARRIOS B ON B.COD_BARRIO = cxb.cod_barrio 
                          where B.COD_BARRIO = @COD_BARRIO";

                    cmd.Parameters.AddWithValue("@COD_BARRIO", COD_BARRIO);

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

        public static List<CALLES> readAll()
        {
            try
            {
                List<CALLES> lst = new List<CALLES>();
                CALLES obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM CALLES";
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int COD_CALLE = dr.GetOrdinal("COD_CALLE");
                         int NOM_CALLE = dr.GetOrdinal("NOM_CALLE");

                        while (dr.Read())
                        {
                            obj = new CALLES();
                            if (!dr.IsDBNull(COD_CALLE)) { obj.COD_CALLE = dr.GetInt32(COD_CALLE); }
                            if (!dr.IsDBNull(NOM_CALLE)) { obj.NOM_CALLE = dr.GetString(NOM_CALLE); }
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
        public static CALLES getByPk(int COD_CALLE)
        {
            try
            {
                CALLES obj = null;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT *FROM Calles A
                          INNER JOIN BARRIOS B ON A.COD_BARRIO:B.COD_BARRIO
                          WHERE A.COD_CALLE=@COD_CALLE";

                    cmd.Parameters.AddWithValue("@COD_CALLE", COD_CALLE);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<CALLES> lst = mapeo(dr);
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

        public static int insert(CALLES obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Calles(");
                sql.AppendLine("COD_CALLE");
                sql.AppendLine(", NOM_CALLE");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@COD_CALLE");
                sql.AppendLine(", @NOM_CALLE");
                sql.AppendLine(")");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@COD_CALLE", obj.COD_CALLE);
                    cmd.Parameters.AddWithValue("@NOM_CALLE", obj.NOM_CALLE);
                    cmd.Connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(CALLES obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  Calles SET");
                sql.AppendLine("NOM_CALLE=@NOM_CALLE");
                sql.AppendLine("WHERE");
                sql.AppendLine("COD_CALLE=@COD_CALLE");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@COD_CALLE", obj.COD_CALLE);
                    cmd.Parameters.AddWithValue("@NOM_CALLE", obj.NOM_CALLE);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int cod_calle)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  Calles ");
                sql.AppendLine("WHERE");
                sql.AppendLine("COD_CALLE=@COD_CALLE");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@COD_CALLE", cod_calle);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<CALLES> getByCallesByBarrio(List<string> barrios)
        {
            try
            {
                List<CALLES> lst = new List<CALLES>();
                CALLES obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;


                    List<string> parametros = new List<string>();
                    for (int i = 0; i < barrios.Count; i++)
                    {
                        parametros.Add($"@barrio{i}");
                        cmd.Parameters.AddWithValue($"@barrio{i}", barrios[i]);
                    }

                    cmd.CommandText = $@"    SELECT A.COD_CALLE, A.NOM_CALLE, B.COD_BARRIO  FROM Calles A
                          INNER JOIN CALLES_X_BARRIO cxb ON A.COD_CALLE = cxb.cod_calle 
                          INNER JOIN BARRIOS B ON B.COD_BARRIO = cxb.cod_barrio 
                          WHERE B.NOM_BARRIO IN ({string.Join(", ", parametros)}) ";

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int COD_CALLE = dr.GetOrdinal("COD_CALLE");
                        int NOM_CALLE = dr.GetOrdinal("NOM_CALLE");
                        int COD_BARRIO = dr.GetOrdinal("COD_BARRIO");

                        while (dr.Read())
                        {

                            obj = new CALLES();
                            if (!dr.IsDBNull(COD_CALLE)) { obj.COD_CALLE = dr.GetInt32(COD_CALLE); }
                            if (!dr.IsDBNull(NOM_CALLE)) { obj.NOM_CALLE = dr.GetString(NOM_CALLE); }
                            if (!dr.IsDBNull(COD_BARRIO)) { obj.COD_BARRIO = dr.GetInt32(COD_BARRIO); }

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
