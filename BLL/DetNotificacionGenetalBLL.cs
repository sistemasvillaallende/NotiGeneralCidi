using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DetNotificacionGenetalBLL
    {
        public static List<DAL.DetNotificacionGeneral> readDetNotNotificaciones(int nro_emision)
        {
            try
            {
                return DAL.DetNotificacionGeneral.read(nro_emision);

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
                DAL.DetNotificacionGeneral.insert(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void insertMasivo(List<DetNotificacionGeneral> lst)
        {
            try
            {
                DAL.DetNotificacionGeneral.InsertMasivo(lst);
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
               return  DAL.DetNotificacionGeneral.getBySubsistemaDTO(nro_emision,subsistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
