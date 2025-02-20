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

    }
}
