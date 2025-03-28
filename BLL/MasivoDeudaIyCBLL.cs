using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MasivoDeudaIyCBLL
    {

         public static List<MasivoDeudaIyC> read(int calleDesde, int calleHasta,
                   int cod_calle, bool dado_baja, string cod_zona)
        {
            try
            {
                return DAL.MasivoDeudaIyC.readByCalles(calleDesde, calleHasta,cod_calle, dado_baja,cod_zona);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
