using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class BarriosBLL
    {
        public static List<BARRIOS> read()
        {
            try
            {
                return DAL.BARRIOS.read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static BARRIOS getByPK(int cod_barrios)
        {
            try
            {
                return DAL.BARRIOS.getByPk(cod_barrios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void insert(BARRIOS obj)
        {
            try
            {
                DAL.BARRIOS.insert(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void update(BARRIOS obj)
        {
            try
            {
                DAL.BARRIOS.update(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void delete(int cod_barrio)
        {
            try
            {
                DAL.BARRIOS.delete(cod_barrio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
