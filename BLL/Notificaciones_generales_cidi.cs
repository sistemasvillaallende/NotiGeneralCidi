using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Notificaciones_generales_cidi
    {
        public static Notificaciones_generales_cidi read()
        {
            try
            {
                return Notificaciones_generales_cidi.read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Notificaciones_generales_cidi getByPk(int id)
        {
            try
            {
                return Notificaciones_generales_cidi.getByPk(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void insert(DAL.Notificaciones_generales_cidi obj,
            List<Detalle_notificaciones_generaes_cidi> lst)
        {
            try
            {
                DataTable lstDet =
                    Detalle_notificaciones_generaes_cidi.ConvertirDetalleEnDataTable(
                    lst);
                DAL.Notificaciones_generales_cidi.InsertarMaestroDetalle(obj, lstDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
