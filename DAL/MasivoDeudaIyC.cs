using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MasivoDeudaIyC : DALBase
    {

        public int legajo { get; set; }
        public string cuit { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int cod_rubro { get; set; }
        public string concepto { get; set; }
        public string nom_calle { get; set; }
        public string Nom_barrio { get; set; }

        public MasivoDeudaIyC()
        {
            legajo = 0;
            cuit = string.Empty;
            nombre = string.Empty;
            apellido = string.Empty;
            cod_rubro = 0;
            concepto = string.Empty;
            nom_calle = string.Empty;
            Nom_barrio = string.Empty;
        }

     public static List<MasivoDeudaIyC> readByCalles(string calleDesde, string calleHasta, bool dado_baja, string cod_zona)
{
    try
    {
        List<MasivoDeudaIyC> lst = new List<MasivoDeudaIyC>();
        MasivoDeudaIyC obj;

        using (SqlConnection con = getConnection())
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            
            // Construct the dynamic SQL query
            string sqlQuery = @"SELECT DISTINCT (a.legajo),vd.CUIT,vd.NOMBRE,vd.APELLIDO ,d.cod_rubro,
                                    d.concepto,a.nom_calle, a.Nom_barrio             
                                FROM indycom a
                                JOIN rubros_x_iyc c on
                                    a.legajo=c.legajo
                                JOIN VECINO_DIGITAL vd on
                                a.nro_cuit = vd.CUIT 
                                JOIN rubros d on
                                    (d.cod_rubro<>71110000 AND d.cod_rubro<>71150000 AND
                                    d.cod_rubro<>71151000 AND d.cod_rubro<>73100000) AND
                                    c.cod_rubro=d.cod_rubro
                                FULL JOIN TIPOS_DE_IVA t on
                                    a.cod_cond_ante_iva=t.cod_cond_ante_iva
                                WHERE
                                a.dado_baja= @dado_baja
                                {0}
                                AND a.nom_calle between @nombredesde and @nombrehasta
                                ORDER BY  a.Nom_calle,  a.Nom_barrio";

            // Dynamically add zone condition only if cod_zona is not empty
            if (!string.IsNullOrWhiteSpace(cod_zona))
            {
                sqlQuery = string.Format(sqlQuery, "AND a.cod_zona = @cod_zona");
                cmd.Parameters.AddWithValue("@cod_zona", cod_zona);
            }
            else
            {
                sqlQuery = string.Format(sqlQuery, "");
            }

            cmd.CommandText = sqlQuery;
            cmd.Parameters.AddWithValue("@nombredesde", calleDesde);
            cmd.Parameters.AddWithValue("@nombrehasta", calleHasta);
            cmd.Parameters.AddWithValue("@dado_baja", dado_baja);

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                int legajo = dr.GetOrdinal("legajo");
                int cuit = dr.GetOrdinal("CUIT");
                int nombre = dr.GetOrdinal("NOMBRE");
                int apellido = dr.GetOrdinal("APELLIDO");
                int cod_rubro = dr.GetOrdinal("cod_rubro");
                int concepto = dr.GetOrdinal("concepto");
                int nombre_calle = dr.GetOrdinal("nom_calle");
                int nombre_barrio = dr.GetOrdinal("Nom_barrio");

                while (dr.Read())
                {
                    obj = new MasivoDeudaIyC();
                    if (!dr.IsDBNull(legajo)) { obj.legajo = dr.GetInt32(legajo); }
                    if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }
                    if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                    if (!dr.IsDBNull(apellido)) { obj.apellido = dr.GetString(apellido); }
                    if (!dr.IsDBNull(cod_rubro)) { obj.cod_rubro = dr.GetInt32(cod_rubro); }
                    if (!dr.IsDBNull(concepto)) { obj.concepto = dr.GetString(concepto); }
                    if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                    if (!dr.IsDBNull(nombre_barrio)) { obj.Nom_barrio = dr.GetString(nombre_barrio); }

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
        //             badec.NOMBRE,
        //             badec.CUIT,
        //             badec.NOMBRE_CALLE,
        //             b.NOM_BARRIO,
        //             badec.NRO_DOM 
        //         FROM 
        //             badec 
        //         FULL OUTER JOIN 
        //             BARRIOS b ON badec.COD_BARRIO = b.COD_BARRIO
        //             where  badec.CUIT  =  @cuit_list  ";
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


        // public static List<MasivoDeudaGeneral> read(List<string> barrios)
        // {
        //     try
        //     {
        //         List<MasivoDeudaGeneral> lst = new List<MasivoDeudaGeneral>();
        //         MasivoDeudaGeneral obj;

        //         using (SqlConnection con = getConnection())
        //         {
        //             SqlCommand cmd = con.CreateCommand();
        //             cmd.CommandType = CommandType.Text;

        //             if (barrios == null || barrios.Count == 0)
        //             {
        //                 cmd.CommandText = @"SELECT 
        //             badec.NOMBRE,
        //             badec.CUIT,
        //             badec.NOMBRE_CALLE,
        //             b.NOM_BARRIO,
        //             badec.NRO_DOM 
        //         FROM 
        //             badec 
        //         FULL OUTER JOIN 
        //             BARRIOS b ON badec.COD_BARRIO = b.COD_BARRIO";
        //             }
        //             else
        //             {
        //                 List<string> parametros = new List<string>();
        //                 for (int i = 0; i < barrios.Count; i++)
        //                 {
        //                     parametros.Add($"@barrio{i}");
        //                     cmd.Parameters.AddWithValue($"@barrio{i}", barrios[i]);
        //                 }

        //                 cmd.CommandText = $@"SELECT 
        //             badec.NOMBRE,
        //             badec.CUIT,
        //             badec.NOMBRE_CALLE,
        //             b.NOM_BARRIO,
        //             badec.NRO_DOM 
        //         FROM 
        //             badec 
        //         FULL OUTER JOIN 
        //             BARRIOS b ON badec.COD_BARRIO = b.COD_BARRIO
        //         WHERE b.NOM_BARRIO IN ({string.Join(", ", parametros)})";
        //             }

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

    }
}
