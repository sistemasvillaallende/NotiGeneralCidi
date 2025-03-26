using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class NotificacionGeneralBLL
    {
        public static List<DAL.NotificacionGeneral> readNotificacionGeneral()
        {
            try
            {
                return DAL.NotificacionGeneral.read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DAL.NotificacionGeneral> readNotificacionBySubsistema(int subsistema)
        {
            try
            {
                return DAL.NotificacionGeneral.readBySubsistema(subsistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public static DAL.NotificacionGeneral readNotificacionByNroEmision(int nro_emision)
        {
            try
            {
                return DAL.NotificacionGeneral.readByNroEmision(nro_emision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void insert(NotificacionGeneral obj)
        {
            try
            {
                DAL.NotificacionGeneral.insert(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int getMaxNroEmision()
        {
            try
            {
                return DAL.NotificacionGeneral.GetMaxNroEmision();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
