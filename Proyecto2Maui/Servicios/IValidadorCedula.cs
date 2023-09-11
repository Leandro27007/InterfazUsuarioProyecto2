using Proyecto2Maui.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2Maui.Servicios
{
    public interface IValidadorCedula
    {

        Task<EstadoCedula> ValidarCedula(string cedula);

    }
}
