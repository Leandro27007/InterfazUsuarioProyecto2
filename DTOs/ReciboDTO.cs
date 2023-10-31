namespace DTOs
{
    public class ReciboDTO
    {
        public int IdRecibo { get; set; }
        public string NombreCliente { get; set; }
        public string? NombreCajero { get; set; }
        public string Estado { get; set; }
        public List<PruebaReciboDTO>? Pruebas { get; set; }
        public decimal? Total { get; set; }

    }
}