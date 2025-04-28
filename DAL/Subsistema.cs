using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Subsistema
    {
        public int tipo_subsistema { get; set; }
        public string nombre { get; set; }

        public Subsistema()
        {
            tipo_subsistema = 0;
            nombre = string.Empty;
        }
    }
}
