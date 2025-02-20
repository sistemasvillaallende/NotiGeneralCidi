using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DetNotificacionGeneral : DALBase
    {
        public int Nro_Emision { get; set; }
        public int Nro_Notificacion { get; set; }
        public string Dominio { get; set; }
        public int Circunscripcion { get; set; }
        public int Seccion { get; set; }
        public int Manzana { get; set; }
        public int Parcela { get; set; }
        public int P_h { get; set; }
        public int Legajo { get; set; }
        public string Cuit { get; set; }
        public int Nro_Badec { get; set; }
        public string Nombre { get; set; }
        public int Cod_estado_cidi { get; set; }
        public DateTime Fecha_Inicio_Estado { get; set; }
        public DateTime Fecha_Fin_Estado { get; set; }
        public DateTime Vencimiento { get; set; }
        public int Nro_cedulon { get; set; }
        public decimal Debe { get; set; }
        public string Barcode39 { get; set; }
        public string Barcodeint25 { get; set; }
        public decimal Monto_original { get; set; }
        public decimal Interes { get; set; }
        public decimal Descuento { get; set; }
        public decimal Importe_pagar { get; set; }


        public DetNotificacionGeneral()
        {
            Nro_Emision = 0;
            Nro_Notificacion = 0;
            Dominio = string.Empty;
            Circunscripcion = 0;
            Seccion = 0;
            Manzana = 0;
            Parcela = 0;
            P_h = 0;
            Legajo = 0;
            Cuit = string.Empty;
            Nro_Badec = 0;
            Nombre = String.Empty;
            Cod_estado_cidi = 0;
            Fecha_Inicio_Estado = DateTime.Now;
            Fecha_Fin_Estado = DateTime.Now;
            Vencimiento = DateTime.Now;
            Nro_cedulon = 0;
            Debe = 0;
            Barcode39 = string.Empty;
            Barcodeint25 = string.Empty;
            Monto_original = 0;
            Interes = 0;
            Descuento = 0;
            Importe_pagar = 0;

        }

        public static List<DetNotificacionGeneral> read(int nro_emision)
        {
            try
            {
                List<DetNotificacionGeneral> lst = new List<DetNotificacionGeneral>();
                DetNotificacionGeneral obj;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = " SELECT * FROM CIDI_DET_NOTIFICACION_GENERAL WHERE Nro_Emision = @nro_emision  ";
                    cmd.Parameters.AddWithValue("@nro_emision", nro_emision); 
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            obj = new DetNotificacionGeneral();
                            obj.Nro_Emision = dr.GetInt32(0);
                            if (!dr.IsDBNull(1)) { obj.Nro_Notificacion = dr.GetInt32(1); }
                            if (!dr.IsDBNull(2)) { obj.Dominio = dr.GetString(2); }
                            if (!dr.IsDBNull(3)) { obj.Circunscripcion = dr.GetInt32(3); }
                            if (!dr.IsDBNull(4)) { obj.Seccion = dr.GetInt32(4); }
                            if (!dr.IsDBNull(5)) { obj.Manzana = dr.GetInt32(5); }
                            if (!dr.IsDBNull(6)) { obj.Parcela = dr.GetInt32(6); }
                            if (!dr.IsDBNull(7)) { obj.P_h = dr.GetInt32(7); }
                            if (!dr.IsDBNull(8)) { obj.Legajo = dr.GetInt32(8); }
                            if (!dr.IsDBNull(9)) { obj.Cuit = dr.GetString(9); }
                            if (!dr.IsDBNull(10)) { obj.Nro_Badec = dr.GetInt32(10); }
                            if (!dr.IsDBNull(11)) { obj.Nombre = dr.GetString(11); }
                            if (!dr.IsDBNull(12)) { obj.Cod_estado_cidi = dr.GetInt32(12); }
                            if (!dr.IsDBNull(13)) { obj.Fecha_Inicio_Estado = dr.GetDateTime(13); }
                            if (!dr.IsDBNull(14)) { obj.Fecha_Fin_Estado = dr.GetDateTime(14); }
                            if (!dr.IsDBNull(15)) { obj.Vencimiento = dr.GetDateTime(15); }
                            if (!dr.IsDBNull(16)) { obj.Nro_cedulon = dr.GetInt32(16); }
                            if (!dr.IsDBNull(17)) { obj.Debe = dr.GetDecimal(17); }
                            if (!dr.IsDBNull(18)) { obj.Barcode39 = dr.GetString(18); }
                            if (!dr.IsDBNull(19)) { obj.Barcodeint25 = dr.GetString(19); }
                            if (!dr.IsDBNull(20)) { obj.Monto_original = dr.GetDecimal(20); }
                            if (!dr.IsDBNull(21)) { obj.Interes = dr.GetDecimal(21); }
                            if (!dr.IsDBNull(22)) { obj.Descuento = dr.GetDecimal(22); }
                            if (!dr.IsDBNull(23)) { obj.Importe_pagar = dr.GetDecimal(23); }
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
