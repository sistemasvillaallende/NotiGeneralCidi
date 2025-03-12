using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MasivoDeudaBLL
    {
        public static List<MasivoDeuda> getByCategoriaDeuda(int catDeuda)
        {
            try
            {
                return DAL.MasivoDeuda.getByCategoriaDeuda(catDeuda);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public static List<MasivoDeuda> getByBarrios(List<string> barrios)
        {
            try
            {
                return DAL.MasivoDeuda.getByBarrios(barrios);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string armoDenominacion(int cir, int sec, int man, int par, int p_h)
        {
            try
            {
                return DAL.MasivoDeuda.armoDenominacion(cir,sec,man,par,p_h);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }




    }
}
