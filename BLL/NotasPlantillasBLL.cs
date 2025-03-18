using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class NotasPlantillasBLL
    {
        public static List<NotasPlantillas> read()
        {
            try
            {
                return NotasPlantillas.read();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void insert(NotasPlantillas obj)
        {
            try
            {
                NotasPlantillas.insert(obj);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void delete(int id)
        {
            try
            {
                NotasPlantillas.delete(id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void update(NotasPlantillas obj)
        {
            try
            {
                NotasPlantillas.update(obj);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void ActualizarContenido(int id, string contenido)
        {
            try
            {
                NotasPlantillas.ActualizarContenido(id, contenido);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static NotasPlantillas getByPk(int id)
        {
            try
            {
                return NotasPlantillas.getByPk(id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int getMaxId()
        {
            try
            {
                return NotasPlantillas.GetMaxId();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }

}

