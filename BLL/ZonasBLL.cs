using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class ZonasBLL:DALBase
    {
        public static List<DAL.ZONAS> read()
        {
            try
            {
                return DAL.ZONAS.read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DAL.ZONAS getByPy(int cod_zona)
        {
            try
            {
                return DAL.ZONAS.getByPk(cod_zona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int insert(ZONAS obj)
        {
            try
            {
                return ZONAS.insert(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void update(ZONAS obj)
        {
            try
            {
                ZONAS.update(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int cod_zona)
        {
            try
            {
                ZONAS.delete(cod_zona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
