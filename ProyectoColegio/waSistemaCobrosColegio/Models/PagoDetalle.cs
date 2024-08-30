namespace waSistemaCobrosColegio.Models
{
    public class PagoDetalle
    {
        public int Id { get; set; }
        public int Id_Pago { get; set; }
        public int Id_Matricula_Detalle { get; set; }
        public string? Concepto { get; set; }
        public double Monto { get; set; }
        public string? Estado { get; set; }
    }
}
