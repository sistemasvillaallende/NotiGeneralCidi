using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
