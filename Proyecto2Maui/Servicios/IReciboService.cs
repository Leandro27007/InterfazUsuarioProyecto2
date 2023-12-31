﻿using DTOs;
using Proyecto2Maui.Modelos.Request;
using Proyecto2Maui.Modelos.Response;

namespace Proyecto2Maui.Servicios
{
    public interface IReciboService
    {
        Task<ReciboResponse> GenerarRecibo(ReciboRequest recibo);
        Task<List<ReciboDTO>> ListarRecibos();
        Task<bool> Reembolsar(ReembolsoRequest reembolsoRequest);

    }
}
