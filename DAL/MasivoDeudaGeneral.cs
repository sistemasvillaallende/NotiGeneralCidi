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
        public string apellido { get; set; }
        public string cuit { get; set; }
        public string nom_calle { get; set; }
        public string nom_barrio { get; set; }
        public int nro_dom_esp { get; set; }


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
                            vd.NOMBRE,
                            vd.APELLIDO,
                            vd.CUIT,                            
                            c.NOM_CALLE,
                            b.NOM_BARRIO
                        FROM 
                            VECINO_DIGITAL vd 
                        LEFT JOIN INMUEBLES i 
                        ON vd.CUIT = i.cuil
                        Inner JOIN 
                            BARRIOS b ON i.cod_barrio = b.COD_BARRIO
                        Inner JOIN 
                         	CALLES c ON  i.cod_calle_dom_esp  = c.COD_CALLE 
                        where i.cod_barrio = @cod_barrio     ";
                    cmd.Parameters.AddWithValue("@cod_barrio", cod_barrio);

                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int apellido = dr.GetOrdinal("APELLIDO");
                        int cuit = dr.GetOrdinal("CUIT");
                        int nombre_calle = dr.GetOrdinal("NOM_CALLE");
                        int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");


                        while (dr.Read())
                        {
                            obj = new MasivoDeudaGeneral();
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(apellido)) { obj.apellido = dr.GetString(apellido); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                            if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                            if (!dr.IsDBNull(nombre_barrio)) { obj.nom_barrio = dr.GetString(nombre_barrio); }

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


        // public static List<MasivoDeudaGeneral> GetByCuit(string cuit_list)
        // {
        //     try
        //     {
        //         List<MasivoDeudaGeneral> lst = new List<MasivoDeudaGeneral>();
        //         MasivoDeudaGeneral obj;


        //         using (SqlConnection con = getConnection())
        //         {
        //             SqlCommand cmd = con.CreateCommand();
        //             cmd.CommandType = CommandType.Text;
        //             cmd.CommandText = @"SELECT 
        //                     badec.NOMBRE,
        //                     badec.CUIT,
        //                     badec.NOMBRE_CALLE,
        //                     b.NOM_BARRIO,
        //                     badec.NRO_DOM 
        //                 FROM 
        //                     badec 
        //                 FULL OUTER JOIN 
        //                     BARRIOS b ON badec.COD_BARRIO = b.COD_BARRIO
        //                     where  badec.CUIT  =  @cuit_list  ";
        //             cmd.Parameters.AddWithValue("@cuit_list", cuit_list);

        //             cmd.Connection.Open();

        //             SqlDataReader dr = cmd.ExecuteReader();

        //             if (dr.HasRows)
        //             {
        //                 int nombre = dr.GetOrdinal("NOMBRE");
        //                 int cuit = dr.GetOrdinal("CUIT");
        //                 int nombre_calle = dr.GetOrdinal("NOMBRE_CALLE");
        //                 int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");
        //                 int nro_dom = dr.GetOrdinal("NRO_DOM");


        //                 while (dr.Read())
        //                 {
        //                     obj = new MasivoDeudaGeneral();
        //                     if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
        //                     if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
        //                     if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
        //                     if (!dr.IsDBNull(nombre_barrio)) { obj.barrio = dr.GetString(nombre_barrio); }
        //                     if (!dr.IsDBNull(nro_dom)) { obj.nro_dom = dr.GetInt32(nro_dom); }

        //                     lst.Add(obj);
        //                 }
        //             }
        //         }
        //         return lst;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }


        public static List<MasivoDeudaGeneral> read(List<string> barrios)
        {
            try
            {
                List<MasivoDeudaGeneral> lst = new List<MasivoDeudaGeneral>();
                MasivoDeudaGeneral obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    if (barrios == null || barrios.Count == 0)
                    {
                        cmd.CommandText = @"
                        SELECT 
                            vd.NOMBRE,
                            vd.APELLIDO,
                            vd.CUIT,                            
                            c.NOM_CALLE,
                            b.NOM_BARRIO
                        FROM 
                            VECINO_DIGITAL vd 
                        LEFT JOIN INMUEBLES i 
                        ON vd.CUIT = i.cuil
                        Inner JOIN 
                            BARRIOS b ON i.cod_barrio = b.COD_BARRIO
                        Inner JOIN 
                         	CALLES c ON  i.cod_calle_dom_esp  = c.COD_CALLE ";
                    }
                    else
                    {
                        List<string> parametros = new List<string>();
                        for (int i = 0; i < barrios.Count; i++)
                        {
                            parametros.Add($"@barrio{i}");
                            cmd.Parameters.AddWithValue($"@barrio{i}", barrios[i]);
                        }

                        cmd.CommandText = $@"SELECT 
                            vd.NOMBRE,
                            vd.APELLIDO,
                            vd.CUIT,                            
                            c.NOM_CALLE,
                            b.NOM_BARRIO
                        FROM 
                            VECINO_DIGITAL vd 
                        LEFT JOIN INMUEBLES i 
                        ON vd.CUIT = i.cuil
                        Inner JOIN 
                            BARRIOS b ON i.cod_barrio = b.COD_BARRIO
                        Inner JOIN 
                         	CALLES c ON  i.cod_calle_dom_esp  = c.COD_CALLE 
                        WHERE b.NOM_BARRIO IN ({string.Join(", ", parametros)})";
                    }

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int apellido = dr.GetOrdinal("APELLIDO");
                        int cuit = dr.GetOrdinal("CUIT");
                        int nombre_calle = dr.GetOrdinal("NOM_CALLE");
                        int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");


                        while (dr.Read())
                        {
                            obj = new MasivoDeudaGeneral();
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(apellido)) { obj.apellido = dr.GetString(apellido); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                            if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                            if (!dr.IsDBNull(nombre_barrio)) { obj.nom_barrio = dr.GetString(nombre_barrio); }

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

        public static List<MasivoDeudaGeneral> readWithFilters(int cod_barrio, int cod_calle, string cod_zona, int desde, int hasta)
        {
            try
            {
                List<MasivoDeudaGeneral> lst = new List<MasivoDeudaGeneral>();
                MasivoDeudaGeneral obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    string sqlQuery = @"
                                SELECT DISTINCT 
                                    vd.NOMBRE,
                                    vd.APELLIDO,
                                    vd.CUIT,                            
                                    c.NOM_CALLE,
                                    b.NOM_BARRIO,
                                    i.nro_dom_esp 
                                FROM 
                                    VECINO_DIGITAL vd 
                                LEFT JOIN INMUEBLES i 
                                    ON vd.CUIT = i.cuil
                                INNER JOIN BARRIOS b 
                                    ON i.cod_barrio = b.COD_BARRIO
                                INNER JOIN CALLES c 
                                    ON i.cod_calle_dom_esp = c.COD_CALLE
                                WHERE 1=1";



                    if (desde >= 0 && hasta > 0)
                    {
                        sqlQuery += " AND i.nro_dom_esp BETWEEN @desde AND @hasta";
                        cmd.Parameters.AddWithValue("@desde", desde);
                        cmd.Parameters.AddWithValue("@hasta", hasta);

                    }

                    if (!string.IsNullOrWhiteSpace(cod_zona))
                    {
                        sqlQuery += " AND i.cod_categoria_zona_liq = @cod_zona";
                        cmd.Parameters.AddWithValue("@cod_zona", cod_zona);
                    }

                    if (cod_barrio > 0)
                    {
                        sqlQuery += " AND i.cod_barrio = @cod_barrio";
                        cmd.Parameters.AddWithValue("@cod_barrio", cod_barrio);
                    }

                    if (cod_calle > 0)
                    {
                        sqlQuery += " AND i.cod_calle_dom_esp = @cod_calle";
                        cmd.Parameters.AddWithValue("@cod_calle", cod_calle);
                    }

                    sqlQuery += " ORDER BY c.NOM_CALLE, b.NOM_BARRIO";


                    cmd.CommandText = sqlQuery;
         
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int apellido = dr.GetOrdinal("APELLIDO");
                        int cuit = dr.GetOrdinal("CUIT");
                        int nombre_calle = dr.GetOrdinal("NOM_CALLE");
                        int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");
                        int nro_dom = dr.GetOrdinal("nro_dom_esp");


                        while (dr.Read())
                        {
                            obj = new MasivoDeudaGeneral();
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(apellido)) { obj.apellido = dr.GetString(apellido); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                            if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                            if (!dr.IsDBNull(nombre_barrio)) { obj.nom_barrio = dr.GetString(nombre_barrio); }
                            if (!dr.IsDBNull(nro_dom)) { obj.nro_dom_esp = dr.GetInt32(nro_dom); }
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
