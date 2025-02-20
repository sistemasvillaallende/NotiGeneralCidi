using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class CATE_DEUDA
    {
        public static List<DAL.CATE_DEUDA> readInmueble()
        {
            try
            {
                return DAL.CATE_DEUDA.read();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<DAL.CATE_DEUDA> readAuto()
        {
            try
            {
                return DAL.CATE_DEUDA.readAuto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<DAL.CATE_DEUDA> readIndyCom()
        {
            try
            {
                return DAL.CATE_DEUDA.readIndyCom();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
