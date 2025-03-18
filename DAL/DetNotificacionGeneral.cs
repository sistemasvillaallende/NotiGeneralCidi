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
            Fecha_Fin_Estado = DateTime.Now.AddMonths(1);
            Vencimiento = DateTime.Now.AddMonths(1);
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
                            if (!dr.IsDBNull(12)) { obj.Cod_estado_cidi = dr.GetInt16(12); }
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


        public static void insert(DetNotificacionGeneral obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO CIDI_DET_NOTIFICACION_GENERAL(");
                sql.AppendLine("  Nro_Emision");
                sql.AppendLine(", Nro_Notificacion");
                sql.AppendLine(", Dominio");
                sql.AppendLine(", Circunscripcion");
                sql.AppendLine(", Seccion");
                sql.AppendLine(", Manzana");
                sql.AppendLine(", Parcela");
                sql.AppendLine(", P_h");
                sql.AppendLine(", Legajo");
                sql.AppendLine(", Cuit");
                sql.AppendLine(", Nro_Badec");
                sql.AppendLine(", Nombre");
                sql.AppendLine(", Cod_estado_cidi");
                sql.AppendLine(", Fecha_Inicio_Estado");
                sql.AppendLine(", Fecha_Fin_Estado");
                sql.AppendLine(", Vencimiento");
                sql.AppendLine(", Nro_cedulon");
                sql.AppendLine(", Debe");
                sql.AppendLine(", Barcode39");
                sql.AppendLine(", Barcodeint25");
                sql.AppendLine(", Monto_original");
                sql.AppendLine(", Interes");
                sql.AppendLine(", Descuento");
                sql.AppendLine(", Importe_pagar");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("  @Nro_Emision");
                sql.AppendLine(", @Nro_Notificacion");
                sql.AppendLine(", @Dominio");
                sql.AppendLine(", @Circunscripcion");
                sql.AppendLine(", @Seccion");
                sql.AppendLine(", @Manzana");
                sql.AppendLine(", @Parcela");
                sql.AppendLine(", @P_h");
                sql.AppendLine(", @Legajo");
                sql.AppendLine(", @Cuit");
                sql.AppendLine(", @Nro_Badec");
                sql.AppendLine(", @Nombre");
                sql.AppendLine(", @Cod_estado_cidi");
                sql.AppendLine(", @Fecha_Inicio_Estado");
                sql.AppendLine(", @Fecha_Fin_Estado");
                sql.AppendLine(", @Vencimiento");
                sql.AppendLine(", @Nro_cedulon");
                sql.AppendLine(", @Debe");
                sql.AppendLine(", @Barcode39");
                sql.AppendLine(", @Barcodeint25");
                sql.AppendLine(", @Monto_original");
                sql.AppendLine(", @Interes");
                sql.AppendLine(", @Descuento");
                sql.AppendLine(", @Importe_pagar");
                sql.AppendLine(")");

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();

                    cmd.Parameters.AddWithValue("@Nro_Emision", obj.Nro_Emision);
                    cmd.Parameters.AddWithValue("@Nro_Notificacion", obj.Nro_Notificacion);
                    cmd.Parameters.AddWithValue("@Dominio", obj.Dominio ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Circunscripcion", obj.Circunscripcion);
                    cmd.Parameters.AddWithValue("@Seccion", obj.Seccion);
                    cmd.Parameters.AddWithValue("@Manzana", obj.Manzana);
                    cmd.Parameters.AddWithValue("@Parcela", obj.Parcela);
                    cmd.Parameters.AddWithValue("@P_h", obj.P_h);
                    cmd.Parameters.AddWithValue("@Legajo", obj.Legajo);
                    cmd.Parameters.AddWithValue("@Cuit", obj.Cuit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Nro_Badec", obj.Nro_Badec);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cod_estado_cidi", obj.Cod_estado_cidi);
                    cmd.Parameters.AddWithValue("@Fecha_Inicio_Estado", obj.Fecha_Inicio_Estado);
                    cmd.Parameters.AddWithValue("@Fecha_Fin_Estado", obj.Fecha_Fin_Estado);
                    cmd.Parameters.AddWithValue("@Vencimiento", obj.Vencimiento);
                    cmd.Parameters.AddWithValue("@Nro_cedulon", obj.Nro_cedulon);
                    cmd.Parameters.AddWithValue("@Debe", obj.Debe);
                    cmd.Parameters.AddWithValue("@Barcode39", obj.Barcode39 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Barcodeint25", obj.Barcodeint25 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Monto_original", obj.Monto_original);
                    cmd.Parameters.AddWithValue("@Interes", obj.Interes);
                    cmd.Parameters.AddWithValue("@Descuento", obj.Descuento);
                    cmd.Parameters.AddWithValue("@Importe_pagar", obj.Importe_pagar);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void InsertMasivo(List<DetNotificacionGeneral> lista)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        foreach (var obj in lista)
                        {
                            StringBuilder sql = new StringBuilder();
                            sql.AppendLine("INSERT INTO CIDI_DET_NOTIFICACION_GENERAL(");
                            sql.AppendLine("  Nro_Emision");
                            sql.AppendLine(", Nro_Notificacion");
                            sql.AppendLine(", Dominio");
                            sql.AppendLine(", Circunscripcion");
                            sql.AppendLine(", Seccion");
                            sql.AppendLine(", Manzana");
                            sql.AppendLine(", Parcela");
                            sql.AppendLine(", P_h");
                            sql.AppendLine(", Legajo");
                            sql.AppendLine(", Cuit");
                            sql.AppendLine(", Nro_Badec");
                            sql.AppendLine(", Nombre");
                            sql.AppendLine(", Cod_estado_cidi");
                            sql.AppendLine(", Fecha_Inicio_Estado");
                            sql.AppendLine(", Fecha_Fin_Estado");
                            sql.AppendLine(", Vencimiento");
                            sql.AppendLine(", Nro_cedulon");
                            sql.AppendLine(", Debe");
                            sql.AppendLine(", Barcode39");
                            sql.AppendLine(", Barcodeint25");
                            sql.AppendLine(", Monto_original");
                            sql.AppendLine(", Interes");
                            sql.AppendLine(", Descuento");
                            sql.AppendLine(", Importe_pagar");
                            sql.AppendLine(")");
                            sql.AppendLine("VALUES");
                            sql.AppendLine("(");
                            sql.AppendLine("  @Nro_Emision");
                            sql.AppendLine(", @Nro_Notificacion");
                            sql.AppendLine(", @Dominio");
                            sql.AppendLine(", @Circunscripcion");
                            sql.AppendLine(", @Seccion");
                            sql.AppendLine(", @Manzana");
                            sql.AppendLine(", @Parcela");
                            sql.AppendLine(", @P_h");
                            sql.AppendLine(", @Legajo");
                            sql.AppendLine(", @Cuit");
                            sql.AppendLine(", @Nro_Badec");
                            sql.AppendLine(", @Nombre");
                            sql.AppendLine(", @Cod_estado_cidi");
                            sql.AppendLine(", @Fecha_Inicio_Estado");
                            sql.AppendLine(", @Fecha_Fin_Estado");
                            sql.AppendLine(", @Vencimiento");
                            sql.AppendLine(", @Nro_cedulon");
                            sql.AppendLine(", @Debe");
                            sql.AppendLine(", @Barcode39");
                            sql.AppendLine(", @Barcodeint25");
                            sql.AppendLine(", @Monto_original");
                            sql.AppendLine(", @Interes");
                            sql.AppendLine(", @Descuento");
                            sql.AppendLine(", @Importe_pagar");
                            sql.AppendLine(")");

                            using (SqlCommand cmd = new SqlCommand(sql.ToString(), con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Nro_Emision", obj.Nro_Emision);
                                cmd.Parameters.AddWithValue("@Nro_Notificacion", obj.Nro_Notificacion);
                                cmd.Parameters.AddWithValue("@Dominio", obj.Dominio ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Circunscripcion", obj.Circunscripcion);
                                cmd.Parameters.AddWithValue("@Seccion", obj.Seccion);
                                cmd.Parameters.AddWithValue("@Manzana", obj.Manzana);
                                cmd.Parameters.AddWithValue("@Parcela", obj.Parcela);
                                cmd.Parameters.AddWithValue("@P_h", obj.P_h);
                                cmd.Parameters.AddWithValue("@Legajo", obj.Legajo);
                                cmd.Parameters.AddWithValue("@Cuit", obj.Cuit ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Nro_Badec", obj.Nro_Badec);
                                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Cod_estado_cidi", obj.Cod_estado_cidi);
                                cmd.Parameters.AddWithValue("@Fecha_Inicio_Estado", obj.Fecha_Inicio_Estado);
                                cmd.Parameters.AddWithValue("@Fecha_Fin_Estado", obj.Fecha_Fin_Estado);
                                cmd.Parameters.AddWithValue("@Vencimiento", obj.Vencimiento);
                                cmd.Parameters.AddWithValue("@Nro_cedulon", obj.Nro_cedulon);
                                cmd.Parameters.AddWithValue("@Debe", obj.Debe);
                                cmd.Parameters.AddWithValue("@Barcode39", obj.Barcode39 ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Barcodeint25", obj.Barcodeint25 ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Monto_original", obj.Monto_original);
                                cmd.Parameters.AddWithValue("@Interes", obj.Interes);
                                cmd.Parameters.AddWithValue("@Descuento", obj.Descuento);
                                cmd.Parameters.AddWithValue("@Importe_pagar", obj.Importe_pagar);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static List<DetalleNotificadorDTO> getBySubsistemaDTO(int subsistema, int nro_emision)
        {
            try
            {
                List<DetalleNotificadorDTO> lst = new List<DetalleNotificadorDTO>();
                DetNotificacionGeneral obj;
                DetalleNotificadorDTO obj2;

                using (SqlConnection con = getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT 
                                            c.Nro_Notificacion, 
                                            c.Cod_estado_cidi,
                                            c.Circunscripcion,
                                            c.Seccion,
                                            c.Manzana,
                                            c.Parcela,
                                            c.P_h,
                                            c.Legajo,
                                            c.Dominio,                                      
                                            c.CUIT,  c.NOMBRE
                                        FROM CIDI_DET_NOTIFICACION_GENERAL c
                                        WHERE Nro_Emision = @nro_emision   ";

                    cmd.Parameters.AddWithValue("@nro_emision", nro_emision);
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            obj = new DetNotificacionGeneral();

                            if (!dr.IsDBNull(0)) { obj.Nro_Notificacion = dr.GetInt32(0); }
                            if (!dr.IsDBNull(1)) { obj.Cod_estado_cidi = dr.GetInt16(1); }
                            if (!dr.IsDBNull(2)) { obj.Circunscripcion = dr.GetInt32(2); }
                            if (!dr.IsDBNull(3)) { obj.Seccion = dr.GetInt32(3); }
                            if (!dr.IsDBNull(4)) { obj.Manzana = dr.GetInt32(4); }
                            if (!dr.IsDBNull(5)) { obj.Parcela = dr.GetInt32(5); }
                            if (!dr.IsDBNull(6)) { obj.P_h = dr.GetInt32(6); }
                            if (!dr.IsDBNull(7)) { obj.Legajo = dr.GetInt32(7); }
                            if (!dr.IsDBNull(8)) { obj.Dominio = dr.GetString(8); }
                            if (!dr.IsDBNull(9)) { obj.Cuit = dr.GetString(9); }
                            if (!dr.IsDBNull(10)) { obj.Nombre = dr.GetString(10); }

                            obj2 = new DetalleNotificadorDTO();
                            obj2.Nro_Notificacion = obj.Nro_Notificacion;
                            obj2.Cod_estado_cidi = obj.Cod_estado_cidi;
                            obj2.Denominacion = GenerateDenominacion(subsistema, obj);
                            obj2.Cuit = obj.Cuit;
                            obj2.Nombre = obj.Nombre;
                            lst.Add(obj2);
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

        private static string GenerateDenominacion(int subsistema, DetNotificacionGeneral obj)
        {
            string denominacion = string.Empty;

            switch (subsistema)
            {
                case 1:
                    denominacion = "CIR: " + obj.Circunscripcion.ToString().PadLeft(2, '0') + " - " +
                                   "SEC: " + obj.Seccion.ToString().PadLeft(2, '0') + " - " +
                                   "MAN: " + obj.Manzana.ToString().PadLeft(3, '0') + " - " +
                                   "PAR: " + obj.Parcela.ToString().PadLeft(3, '0') + " - " +
                                   "P_H: " + obj.P_h.ToString().PadLeft(3, '0');
                    break;
                case 3:
                    denominacion = obj.Legajo.ToString();
                    break;
                case 4:
                    denominacion = obj.Dominio ?? string.Empty;
                    break;
                default:
                    denominacion = string.Empty;
                    break;
            }

            return denominacion;
        }

    }
}
