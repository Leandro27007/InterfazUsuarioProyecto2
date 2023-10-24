namespace Proyecto2Maui.Modelos.Request
{
    public class ReciboRequest
    {
        public string idCajero { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string cedula { get; set; }
        public List<PruebaLab> pruebas { get; set; }
    }
}
