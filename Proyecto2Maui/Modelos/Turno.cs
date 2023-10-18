using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto2Maui.Modelos;

namespace Proyecto2Maui.Modelos
{
    public class Turno
    {
        public string idTurno { get; set; }
        public List<PruebaLab> pruebasLab { get; set; } = new ();

    }



}
