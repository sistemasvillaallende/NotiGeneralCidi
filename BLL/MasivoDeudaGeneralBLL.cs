using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MasivoDeudaGeneralBLL
    {
        public static List<MasivoDeudaGeneral> read(int cod_barrio)
        {
            try
            {
                return DAL.MasivoDeudaGeneral.read(cod_barrio);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public static List<MasivoDeudaGeneral> read(List<string> barrios)
        {
            try
            {
                return DAL.MasivoDeudaGeneral.read(barrios);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



    }
}
