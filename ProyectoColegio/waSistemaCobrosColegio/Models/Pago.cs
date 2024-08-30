using System.Data;

namespace waSistemaCobrosColegio.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int Id_Matricula { get; set; }
        public decimal Monto_Total { get; set; }
        public string? Metodo_Pago { get; set; }
        public string? Numero_Op { get; set; }
        public string? Url_Voucher { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public DateTime Fecha_Actualizacion { get; set; }
        public string? Estado { get; set; }
        public IEnumerable<PagoDetalle>? Pago_Detalle { get; set; }
    }
}
