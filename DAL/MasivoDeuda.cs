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
    public class MasivoDeuda : DALBase
    {

        public int cir { get; set; }
        public int sec { get; set; }
        public int man { get; set; }
        public int par { get; set; }
        public int p_h { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cuit { get; set; }
        public string nom_calle { get; set; }
        public string nom_barrio { get; set; }


        public MasivoDeuda()
        {
            cir = 0;
            sec= 0;
            man = 0;
            par = 0;
            p_h = 0;
            nombre = string.Empty;
            apellido = string.Empty;
            cuit = string.Empty;
            nom_calle = string.Empty;
            nom_barrio = string.Empty;

        }




        public static List<MasivoDeuda> getByBarrios(List<string> barrios)
        {
            try
            {
                List<MasivoDeuda> lst = new List<MasivoDeuda>();
                MasivoDeuda obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    if (barrios == null || barrios.Count == 0)
                    {
                        cmd.CommandText = @"SELECT DISTINCT 
                                                    i.circunscripcion,
                                                    i.seccion,
                                                    i.manzana,
                                                    i.parcela,
                                                    i.p_h,
                                                    b.NOM_BARRIO,              
                                                    c.NOM_CALLE,               
                                                    vd.NOMBRE,
                                                    vd.APELLIDO,
                                                    vd.CUIT                          
                                                FROM 
                                                    CTASCTES_INMUEBLES ci
                                                INNER JOIN 
                                                    INMUEBLES i 
                                                    ON  ci.circunscripcion = i.circunscripcion
                                                    AND ci.seccion = i.seccion
                                                    AND ci.manzana = i.manzana
                                                    AND ci.parcela = i.parcela
                                                    AND ci.p_h = i.p_h
                                                INNER JOIN 
                                                    VECINO_DIGITAL vd
                                                    ON i.CUIT_VECINO_DIGITAL = vd.CUIT
                                                LEFT JOIN 
                                                    BARRIOS b
                                                    ON i.cod_barrio = b.COD_BARRIO
                                                LEFT JOIN 
                                                    CALLES c
                                                    ON i.cod_calle_dom_esp = c.COD_CALLE";
                    }
                    else
                    {
                        List<string> parametros = new List<string>();
                        for (int i = 0; i < barrios.Count; i++)
                        {
                            parametros.Add($"@barrio{i}");
                            cmd.Parameters.AddWithValue($"@barrio{i}", barrios[i]);
                        }

                        cmd.CommandText = $@"SELECT DISTINCT 
                                                i.circunscripcion,
                                                i.seccion,
                                                i.manzana,
                                                i.parcela,
                                                i.p_h,
                                                b.NOM_BARRIO,              
                                                c.NOM_CALLE,               
                                                vd.NOMBRE,
                                                vd.APELLIDO,
                                                vd.CUIT 
                                            FROM 
                                                CTASCTES_INMUEBLES ci
                                            INNER JOIN 
                                                INMUEBLES i 
                                                ON  ci.circunscripcion = i.circunscripcion
                                                AND ci.seccion = i.seccion
                                                AND ci.manzana = i.manzana
                                                AND ci.parcela = i.parcela
                                                AND ci.p_h = i.p_h
                                            INNER JOIN 
                                                VECINO_DIGITAL vd
                                                ON i.CUIT_VECINO_DIGITAL = vd.CUIT
                                            LEFT JOIN 
                                                BARRIOS b
                                                ON i.cod_barrio = b.COD_BARRIO
                                            LEFT JOIN 
                                                CALLES c
                                                ON i.cod_calle_dom_esp = c.COD_CALLE
                                            WHERE 
                                                b.NOM_BARRIO IN ({string.Join(", ", parametros)})";
                    }

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int cir = dr.GetOrdinal("circunscripcion");
                        int sec = dr.GetOrdinal("seccion");
                        int man = dr.GetOrdinal("manzana");
                        int par = dr.GetOrdinal("parcela");
                        int p_h = dr.GetOrdinal("p_h");
                        int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");
                        int nombre_calle = dr.GetOrdinal("NOM_CALLE");
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int apellido = dr.GetOrdinal("APELLIDO");
                        int cuit = dr.GetOrdinal("CUIT");

                        while (dr.Read())
                        {

                            obj = new MasivoDeuda();
                            if (!dr.IsDBNull(cir)) { obj.cir = dr.GetInt32(cir); }
                            if (!dr.IsDBNull(sec)) { obj.sec = dr.GetInt32(sec); }
                            if (!dr.IsDBNull(man)) { obj.man = dr.GetInt32(man); }
                            if (!dr.IsDBNull(par)) { obj.par = dr.GetInt32(par); }
                            if (!dr.IsDBNull(p_h)) { obj.p_h = dr.GetInt32(p_h); }
                            if (!dr.IsDBNull(nombre_barrio)) { obj.nom_barrio = dr.GetString(nombre_barrio); }
                            if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(apellido)) { obj.apellido = dr.GetString(apellido); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }

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

        public static List<MasivoDeuda> getByCategoriaDeuda(int catDeuda)
        {
            try
            {
                List<MasivoDeuda> lst = new List<MasivoDeuda>();
                MasivoDeuda obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    {

                        cmd.CommandText = $@"SELECT DISTINCT 
                                                i.circunscripcion,
                                                i.seccion,
                                                i.manzana,
                                                i.parcela,
                                                i.p_h,
                                                b.NOM_BARRIO,              
                                                c.NOM_CALLE,               
                                                vd.NOMBRE,
                                                vd.APELLIDO,
                                                vd.CUIT 
                                            FROM 
                                                CTASCTES_INMUEBLES ci
                                            INNER JOIN 
                                                INMUEBLES i 
                                                ON  ci.circunscripcion = i.circunscripcion
                                                AND ci.seccion = i.seccion
                                                AND ci.manzana = i.manzana
                                                AND ci.parcela = i.parcela
                                                AND ci.p_h = i.p_h
                                            INNER JOIN 
                                                VECINO_DIGITAL vd
                                                ON i.CUIT_VECINO_DIGITAL = vd.CUIT
                                            LEFT JOIN 
                                                BARRIOS b
                                                ON i.cod_barrio = b.COD_BARRIO
                                            LEFT JOIN 
                                                CALLES c
                                                ON i.cod_calle_dom_esp = c.COD_CALLE
                                                WHERE ci.categoria_deuda = @catDeuda";
                    }

                    cmd.Parameters.AddWithValue("@catDeuda", catDeuda);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int cir = dr.GetOrdinal("circunscripcion");
                        int sec = dr.GetOrdinal("seccion");
                        int man = dr.GetOrdinal("manzana");
                        int par = dr.GetOrdinal("parcela");
                        int p_h = dr.GetOrdinal("p_h");
                        int nombre_barrio = dr.GetOrdinal("NOM_BARRIO");
                        int nombre_calle = dr.GetOrdinal("NOM_CALLE");
                        int nombre = dr.GetOrdinal("NOMBRE");
                        int apellido = dr.GetOrdinal("APELLIDO");
                        int cuit = dr.GetOrdinal("CUIT");

                        while (dr.Read())
                        {

                            obj = new MasivoDeuda();
                            if (!dr.IsDBNull(cir)) { obj.cir = dr.GetInt32(cir); }
                            if (!dr.IsDBNull(sec)) { obj.sec = dr.GetInt32(sec); }
                            if (!dr.IsDBNull(man)) { obj.man = dr.GetInt32(man); }
                            if (!dr.IsDBNull(par)) { obj.par = dr.GetInt32(par); }
                            if (!dr.IsDBNull(p_h)) { obj.p_h = dr.GetInt32(p_h); }
                            if (!dr.IsDBNull(nombre_barrio)) { obj.nom_barrio = dr.GetString(nombre_barrio); }
                            if (!dr.IsDBNull(nombre_calle)) { obj.nom_calle = dr.GetString(nombre_calle); }
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(apellido)) { obj.apellido = dr.GetString(apellido); }
                            if (!dr.IsDBNull(cuit)) { obj.cuit = dr.GetString(cuit); }

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


        public static string armoDenominacion(int cir, int sec, int man, int par, int p_h)
        {
            try
            {
                StringBuilder denominacion = new StringBuilder();

                if (cir < 10)
                    denominacion.AppendFormat("CIR: 0{0} - ", cir);
                if (cir > 9 && cir < 100)
                    denominacion.AppendFormat("CIR: {0} - ", cir);

                if (sec < 10)
                    denominacion.AppendFormat("SEC: 0{0} - ", sec);
                if (sec > 9 && sec < 100)
                    denominacion.AppendFormat("SEC: {0} - ", sec);

                if (man < 10)
                    denominacion.AppendFormat("MAN: 00{0} - ", man);
                if (man > 9 && man < 100)
                    denominacion.AppendFormat("MAN: 0{0} - ", man);
                if (man > 99)
                    denominacion.AppendFormat("MAN: {0} - ", man);

                if (par < 10)
                    denominacion.AppendFormat("PAR: 00{0} - ", par);
                if (par > 9 && par < 100)
                    denominacion.AppendFormat("PAR: 0{0} - ", par);
                if (par > 99)
                    denominacion.AppendFormat("PAR: {0} - ", par);

                if (p_h < 10)
                    denominacion.AppendFormat("P_H: 00{0}", p_h);
                if (p_h > 9 && p_h < 100)
                    denominacion.AppendFormat("P_H: 0{0}", p_h);
                if (p_h > 99)
                    denominacion.AppendFormat("P_H: {0}", p_h);

                return denominacion.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
