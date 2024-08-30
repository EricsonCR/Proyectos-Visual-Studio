namespace waSistemaCobrosColegio.Models
{
    public class MatriculaDetalle
    {
        public int Id { get; set; }
        public int Id_Matricula { get; set; }
        public string? Concepto { get; set; }
        public decimal Monto { get; set; }
        public string? Estado { get; set; }
    }
}
