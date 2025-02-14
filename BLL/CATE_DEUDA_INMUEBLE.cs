using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class CATE_DEUDA_INMUEBLE
    {
        public static List<DAL.CATE_DEUDA_INMUEBLE> read()
        {
            try
            {
                return DAL.CATE_DEUDA_INMUEBLE.read();
                //return DAL.CATE_DEUDA_INMUEBLE.read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<DAL.CATE_DEUDA_INMUEBLE> readAuto()
        {
            try
            {
                return DAL.CATE_DEUDA_INMUEBLE.readAuto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<DAL.CATE_DEUDA_INMUEBLE> readIndyCom()
        {
            try
            {
                return DAL.CATE_DEUDA_INMUEBLE.readIndyCom();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
