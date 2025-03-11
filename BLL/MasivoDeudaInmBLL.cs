using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class MasivoDeudaInmBLL
    {
        public static List<MasivoDeudaInm> read(DateTime desde, DateTime hasta, List<string> lstCod)
        {
            try
            {
                return DAL.MasivoDeudaInm.read(desde, hasta, lstCod);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        
    }
}
