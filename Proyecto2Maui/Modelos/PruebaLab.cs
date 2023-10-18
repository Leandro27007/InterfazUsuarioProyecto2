﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2Maui.Modelos
{
    public class PruebaLab
    {
        public int idPrueba { get; set; }
        public string nombrePrueba { get; set; }
        public decimal? precio { get; set; }
        public bool isChecked { get; set; } = false;
    }
}