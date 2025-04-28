using System;


namespace DAL
{
    public class ClasificacionPersonal { 
    
        public int cod_clasif_per { get; set; }
        public DateTime fecha_alta_registro { get; set; }
        public string des_clasif_per { get; set; }

        public ClasificacionPersonal()
        {
            cod_clasif_per = 0;
            fecha_alta_registro = DateTime.Now;
            des_clasif_per = String.Empty;
        }
    }
}
