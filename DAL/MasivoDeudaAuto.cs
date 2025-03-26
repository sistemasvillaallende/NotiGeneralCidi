using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MasivoDeudaAuto : DALBase
    {

        public string dominio { get; set; }
        public string cuit { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int anio { get; set; }
        public bool exento { get; set; }
        
        public MasivoDeudaAuto()
        {
            dominio = string.Empty;
            cuit = string.Empty;
            nombre = string.Empty;
            apellido = string.Empty;
            anio = 0;
            exento = false;
            
        }

        public static List<MasivoDeudaAuto> readByAnio(int anio, bool exento)
        {
            try
            {
                List<MasivoDeudaAuto> lst = new List<MasivoDeudaAuto>();
                MasivoDeudaAuto obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    string sqlQuery = @"SELECT v.DOMINIO, vd.CUIT,
                                            vd.NOMBRE, vd.APELLIDO, v.ANIO,
                                            v.EXENTO FROM vehiculos v  
                                            JOIN VECINO_DIGITAL vd 
                                            ON v.CUIT = vd.CUIT
                                            WHERE V.ANIO = @anio AND
                                            v.EXENTO  = @exento";

                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@exento", exento);

                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int dominio = dr.GetOrdinal("DOMINIO");
                        int cuit = dr.GetOrdinal("CUIT");
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int apellido = dr.GetOrdinal("APELLIDO");
                        int anioV = dr.GetOrdinal("ANIO");
                        int exentoV = dr.GetOrdinal("EXENTO");
                        

                        while (dr.Read())
                        {
                            obj = new MasivoDeudaAuto();
                            if (!dr.IsDBNull(dominio)) { obj.dominio = dr.GetString(dominio); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(apellido)) { obj.apellido = dr.GetString(apellido); }
                            if (!dr.IsDBNull(anioV)) { obj.anio = dr.GetInt32(anioV); }
                            if (!dr.IsDBNull(exentoV)) { obj.exento = dr.GetBoolean(exentoV); }
                            

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
