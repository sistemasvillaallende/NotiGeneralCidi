using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
