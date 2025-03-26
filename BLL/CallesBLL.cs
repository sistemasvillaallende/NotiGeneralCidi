using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CallesBLL
    {

        public static List<CALLES> read(int cod_barrio)
        {
            try
            {
                return DAL.CALLES.read(cod_barrio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public static List<CALLES> readAll()
        {
            try
            {
                return DAL.CALLES.readAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<CALLES> getByCallesByBarrio(List<string> barrios)
        {
            try
            {
                return DAL.CALLES.getByCallesByBarrio(barrios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
