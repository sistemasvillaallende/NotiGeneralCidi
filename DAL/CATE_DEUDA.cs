using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CATE_DEUDA:DALBase
    {
        public int cod_categoria { get; set; }
        public string des_categoria { get; set; }


        public static List<CATE_DEUDA> read()
        {
            try
            {
                List<CATE_DEUDA> lst = new List<CATE_DEUDA>();
                CATE_DEUDA obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM CATE_DEUDA_INMUEBLE ORDER BY cod_categoria";
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            obj = new CATE_DEUDA();
                            obj.cod_categoria = dr.GetInt32(0);
                            if (!dr.IsDBNull(1)) { obj.des_categoria = dr.GetString(1); }
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
        public static List<CATE_DEUDA> readAuto()
        {
            try
            {
                List<CATE_DEUDA> lst = new List<CATE_DEUDA>();
                CATE_DEUDA obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM CATE_DEUDA_AUTOMOTOR ORDER BY cod_categoria";
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            obj = new CATE_DEUDA();
                            obj.cod_categoria = dr.GetInt32(0);
                            if (!dr.IsDBNull(1)) { obj.des_categoria = dr.GetString(1); }
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
        public static List<CATE_DEUDA> readIndyCom()
        {
            try
            {
                List<CATE_DEUDA> lst = new List<CATE_DEUDA>();
                CATE_DEUDA obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM CATE_DEUDA_INDYCOM ORDER BY cod_categoria";
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            obj = new CATE_DEUDA();
                            obj.cod_categoria = dr.GetInt32(0);
                            if (!dr.IsDBNull(1)) { obj.des_categoria = dr.GetString(1); }
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
