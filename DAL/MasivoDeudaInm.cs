using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MasivoDeudaInm : DALBase
    {
        public string cir { get; set; }
        public string sec { get; set; }
        public string man { get; set; }
        public string par { get; set; }
        public string p_h { get; set; }
        public string nro_cta { get; set; }
        public string titular { get; set; }
        public string dniTit { get; set; }
        public string ocupante { get; set; }
        public string barrio { get; set; }
        public decimal deudaJudicial { get; set; }
        public decimal deudaPreJudicial { get; set; }
        public decimal deudaAdministrativa { get; set; }
        public decimal deudaNormal { get; set; }
        public string zona { get; set; }
        public int categoria_deuda { get; set; }
        public string cuil { get; set; }
        public string NOM_CALLE { get; set; }
        public int nro_dom_esp { get; set; }

        public static List<MasivoDeudaInm> read(DateTime desde, DateTime hasta, List<string> lstCod)
        {
            try
            {
                List<MasivoDeudaInm> lst = new List<MasivoDeudaInm>();
                MasivoDeudaInm obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lstCod.Count == 0)
                    {
                        cmd.CommandText = "sp_proc_get_deuda_x_situacion_x_categoria";
                        cmd.CommandTimeout = 600;
                        cmd.Parameters.AddWithValue("@desde", desde);
                        cmd.Parameters.AddWithValue("@hasta", hasta);
                        string codigos = string.Empty;
                        List<CATE_DEUDA> lstCate = CATE_DEUDA.read();
                        for (int i = 0; i < lstCate.Count; i++)
                        {
                            codigos += lstCate[i].cod_categoria;
                            if (i != lstCate.Count - 1)
                                codigos += ",";
                        }
                        cmd.Parameters.AddWithValue("@cate", codigos);
                    }
                    else
                    {
                        cmd.CommandText = "sp_proc_get_deuda_x_situacion_x_categoria";
                        cmd.Parameters.AddWithValue("@desde", desde);
                        cmd.Parameters.AddWithValue("@hasta", hasta);
                        string codigos = string.Empty;
                        for (int i = 0; i < lstCod.Count; i++)
                        {
                            codigos += lstCod[i];
                            if (i != lstCod.Count - 1)
                                codigos += ",";
                        }
                        cmd.Parameters.AddWithValue("@cate", codigos);
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
                        //int nro_cta = dr.GetOrdinal("circunscripcion");
                        int titular = dr.GetOrdinal("Nombre");
                        // int ocupante = dr.GetOrdinal("circunscripcion");
                        int barrio = dr.GetOrdinal("NOM_BARRIO");
                        int deudaJudicial = dr.GetOrdinal("DEUDA_JUDICIAL");
                        int deudaPreJudicial = dr.GetOrdinal("DEUDA_PRE-JUDICIAL");
                        int deudaAdministrativa = dr.GetOrdinal("DEUDA_ADMINISTRATIVA");
                        int deudaNormal = dr.GetOrdinal("DEUDA_NORMAL");
                        int zona = dr.GetOrdinal("nom_zona");
                        int ocupante = dr.GetOrdinal("ocupante");
                        int cuil = dr.GetOrdinal("cuil");
                        int NOM_CALLE = dr.GetOrdinal("NOM_CALLE");
                        int nro_dom_esp = dr.GetOrdinal("nro_dom_esp");

                        while (dr.Read())
                        {
                            obj = new MasivoDeudaInm();
                            if (!dr.IsDBNull(cir)) { obj.cir = dr.GetInt32(cir).ToString(); }
                            if (!dr.IsDBNull(sec)) { obj.sec = dr.GetInt32(sec).ToString(); }
                            if (!dr.IsDBNull(man)) { obj.man = dr.GetInt32(man).ToString(); }
                            if (!dr.IsDBNull(par)) { obj.par = dr.GetInt32(par).ToString(); }
                            if (!dr.IsDBNull(p_h)) { obj.p_h = dr.GetInt32(p_h).ToString(); }

                            if (!dr.IsDBNull(titular)) { obj.titular = dr.GetString(titular); }
                            if (!dr.IsDBNull(ocupante)) { obj.ocupante = dr.GetString(ocupante); }
                            if (!dr.IsDBNull(barrio)) { obj.barrio = dr.GetString(barrio); }

                            if (!dr.IsDBNull(deudaJudicial)) { obj.deudaJudicial = dr.GetDecimal(deudaJudicial); }
                            if (!dr.IsDBNull(deudaPreJudicial)) { obj.deudaPreJudicial = dr.GetDecimal(deudaPreJudicial); }
                            if (!dr.IsDBNull(deudaAdministrativa)) { obj.deudaAdministrativa = dr.GetDecimal(deudaAdministrativa); }
                            if (!dr.IsDBNull(deudaNormal)) { obj.deudaNormal = dr.GetDecimal(deudaNormal); }

                            if (!dr.IsDBNull(zona)) { obj.zona = dr.GetString(zona); }
                            if (!dr.IsDBNull(cuil)) { obj.cuil = dr.GetString(cuil); }
                            if (!dr.IsDBNull(NOM_CALLE)) { obj.NOM_CALLE = dr.GetString(NOM_CALLE); }
                            if (!dr.IsDBNull(nro_dom_esp)) { obj.nro_dom_esp = dr.GetInt32(nro_dom_esp); }

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
