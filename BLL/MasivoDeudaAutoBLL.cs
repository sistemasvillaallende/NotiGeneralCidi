using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MasivoDeudaAutoBLL
    {

        public static List<MasivoDeudaAuto> read(int anio, bool exento)
        {
            try
            {
                return DAL.MasivoDeudaAuto.readByAnio(anio,exento);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
