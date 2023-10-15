using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2Maui.Modelos
{
    public class Turno
    {
        public string idTurno { get; set; }
        public List<Pruebaslab> pruebasLab { get; set; } = new ();

    }

    public class Pruebaslab
    {
        public int idPruebaLab { get; set; }
        public string nombrePrueba { get; set; }
    }

}
