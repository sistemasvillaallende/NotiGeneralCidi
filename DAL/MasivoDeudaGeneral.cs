using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.OleDb;


namespace DAL
{
    public class MasivoDeudaGeneral : DALBase
    {

        public string nombre { get; set; }
        public string cuit { get; set; }
        public string nom_calle { get; set; }
        public string barrio { get; set; }
        public int nro_dom { get; set; }

        public static List<MasivoDeudaGeneral> read(int cod_barrio)
        {
            try
            {
                List<MasivoDeudaGeneral> lst = new List<MasivoDeudaGeneral>();
                MasivoDeudaGeneral obj;


                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT 
                            badec.NOMBRE,
                            badec.CUIT,
                            badec.NOMBRE_CALLE,
                            b.NOM_BARRIO,
                            badec.NRO_DOM 
                        FROM 
                            badec 
                        FULL OUTER JOIN 
                            BARRIOS b ON badec.COD_BARRIO = b.COD_BARRIO
                            where badec.COD_BARRIO  =  @cod_barrio  ";
                    cmd.Parameters.AddWithValue("@cod_barrio", cod_barrio);

                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int cuit = dr.GetOrdinal("CUIT");
                        int nombre_calle = dr.GetOrdinal("NOMBRE_CALLE");
                        int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");
                        int nro_dom = dr.GetOrdinal("NRO_DOM");


                        while (dr.Read())
                        {
                            obj = new MasivoDeudaGeneral();
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                            if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                            if (!dr.IsDBNull(nombre_barrio)) { obj.barrio = dr.GetString(nombre_barrio); }
                            if (!dr.IsDBNull(nro_dom)) { obj.nro_dom = dr.GetInt32(nro_dom); }

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


        public static List<MasivoDeudaGeneral> GetByCuit(string cuit_list)
        {
            try
            {
                List<MasivoDeudaGeneral> lst = new List<MasivoDeudaGeneral>();
                MasivoDeudaGeneral obj;


                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT 
                            badec.NOMBRE,
                            badec.CUIT,
                            badec.NOMBRE_CALLE,
                            b.NOM_BARRIO,
                            badec.NRO_DOM 
                        FROM 
                            badec 
                        FULL OUTER JOIN 
                            BARRIOS b ON badec.COD_BARRIO = b.COD_BARRIO
                            where  badec.CUIT  =  @cuit_list  ";
                    cmd.Parameters.AddWithValue("@cuit_list",cuit_list);

                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int cuit = dr.GetOrdinal("CUIT");
                        int nombre_calle = dr.GetOrdinal("NOMBRE_CALLE");
                        int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");
                        int nro_dom = dr.GetOrdinal("NRO_DOM");


                        while (dr.Read())
                        {
                            obj = new MasivoDeudaGeneral();
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                            if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                            if (!dr.IsDBNull(nombre_barrio)) { obj.barrio = dr.GetString(nombre_barrio); }
                            if (!dr.IsDBNull(nro_dom)) { obj.nro_dom = dr.GetInt32(nro_dom); }

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
