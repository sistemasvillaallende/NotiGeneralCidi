using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Personal
    {
        public string cuil { get; set; }
        public string nombre { get; set; }
        public string des_clasif_per { get; set; }

        public Personal()
        {
            cuil = string.Empty;
            nombre = string.Empty;
            des_clasif_per = string.Empty;
        }

    }
}
