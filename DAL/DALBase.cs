using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DAL
{
    public class DALBase
    {
        public static SqlConnection getConnection()
        {
            try
            {
                return new SqlConnection(System.Configuration.
                    ConfigurationManager.ConnectionStrings["DBMain2"].ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static long NewID(string tableName, string campo)
        {
            long id = 0;

            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT MAX(nro_cedulon) As Mayor");
            strSQL.AppendLine("FROM " + tableName);

            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = null;

            cmd.Parameters.Add(new SqlParameter("@campo", campo));
            //cmd.Parameters.Add(new SqlParameter("@table", tableName));

            try
            {
                cn = getConnection();

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    id = dr.GetInt32(0) + 1;

                return id;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error, no se pudo obtener el prox. código, " + ex.Message);
                throw ex;
                /*EventLog.WriteEntry("netLibraty - nvoCodigo ", ex.Message);*/
            }
            finally { cn.Close(); }
        }
        public static SqlDataReader pivot(int anio, int liquidacion, int tipo)
        {
            try
            {
                SqlDataReader dr;
                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_detalle_sueldos_pivot";
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liquidacion", tipo);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", liquidacion);
                    cmd.Connection.Open();

                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                        }

                    }

                }
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
