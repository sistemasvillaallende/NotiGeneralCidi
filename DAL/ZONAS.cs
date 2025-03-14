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
        public string categoria{ get; set; }
        public decimal monto_x_mts_edificado { get; set; }
        public decimal minimo_edificado{ get; set; }
        public decimal monto_x_mts_baldio { get; set; }
        public decimal minimo_baldio{ get; set; }

        public ZONAS()
        {
            categoria = string.Empty;
            monto_x_mts_edificado = 0;
            minimo_edificado = 0;
            monto_x_mts_baldio = 0;
            minimo_baldio = 0;
        }

        private static List<ZONAS> mapeo(SqlDataReader dr)
        {
            List<ZONAS> lst = new List<ZONAS>();
            ZONAS obj;
            if (dr.HasRows)
            {
                int categoria = dr.GetOrdinal("categoria");
                int monto_x_mts_edificado = dr.GetOrdinal("monto_x_mts_edificado");
                int minimo_edificado = dr.GetOrdinal("minimo_edificado");
                int monto_x_mts_baldio = dr.GetOrdinal("monto_x_mts_baldio");
                int minimo_baldio = dr.GetOrdinal("minimo_baldio");

                while (dr.Read())
                {
                    obj = new ZONAS();
                    if (!dr.IsDBNull(categoria)) { obj.categoria = dr.GetString(categoria); }
                    if (!dr.IsDBNull(monto_x_mts_edificado)) { obj.monto_x_mts_edificado = dr.GetDecimal(monto_x_mts_edificado); }
                    if (!dr.IsDBNull(minimo_edificado)) { obj.minimo_edificado = dr.GetDecimal(minimo_edificado); }
                    if (!dr.IsDBNull(monto_x_mts_baldio)) { obj.monto_x_mts_baldio = dr.GetDecimal(monto_x_mts_baldio); }
                    if (!dr.IsDBNull(minimo_baldio)) { obj.minimo_baldio = dr.GetDecimal(minimo_baldio); }
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
                    cmd.CommandText = "SELECT *FROM CATEGORIAS_LIQUIDACION_TASA";
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

        public static ZONAS getByPk(string categoria)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM CATEGORIAS_LIQUIDACION_TASA WHERE");
                sql.AppendLine("categoria = @categoria");
                ZONAS obj = null;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@categoria", categoria);
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
                sql.AppendLine("INSERT INTO CATEGORIAS_LIQUIDACION_TASA (");
                sql.AppendLine("categoria");
                sql.AppendLine(", monto_x_mts_edificado");
                sql.AppendLine(", minimo_edificado");
                sql.AppendLine(", monto_x_mts_baldio");
                sql.AppendLine(", minimo_baldio");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@categoria");
                sql.AppendLine(", @monto_x_mts_edificado");
                sql.AppendLine(", @minimo_edificado");
                sql.AppendLine(", @monto_x_mts_baldio");
                sql.AppendLine(", @minimo_baldio");
                sql.AppendLine(")");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@categoria", obj.categoria);
                    cmd.Parameters.AddWithValue("@monto_x_mts_edificado", obj.monto_x_mts_edificado);
                    cmd.Parameters.AddWithValue("@minimo_edificado", obj.minimo_edificado);
                    cmd.Parameters.AddWithValue("@monto_x_mts_baldio", obj.monto_x_mts_baldio);
                    cmd.Parameters.AddWithValue("@minimo_baldio", obj.minimo_baldio);
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
                sql.AppendLine("UPDATE CATEGORIAS_LIQUIDACION_TASA SET");
                sql.AppendLine(", monto_x_mts_edificado=@monto_x_mts_edificado");
                sql.AppendLine(", minimo_edificado=@minimo_edificado");
                sql.AppendLine(", monto_x_mts_baldio=@monto_x_mts_baldio");
                sql.AppendLine(", minimo_baldio=@minimo_baldio");
                sql.AppendLine("WHERE");
                sql.AppendLine("categoria=@categoria");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@categoria", obj.categoria);
                    cmd.Parameters.AddWithValue("@monto_x_mts_edificado", obj.monto_x_mts_edificado);
                    cmd.Parameters.AddWithValue("@minimo_edificado", obj.minimo_edificado);
                    cmd.Parameters.AddWithValue("@monto_x_mts_baldio", obj.monto_x_mts_baldio);
                    cmd.Parameters.AddWithValue("@minimo_baldio", obj.minimo_baldio);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(string categoria)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  CATEGORIAS_LIQUIDACION_TASA  ");
                sql.AppendLine("WHERE");
                sql.AppendLine("categoria=@categoria");
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@categoria", categoria);
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
