using System.ComponentModel.DataAnnotations;

namespace waSistemaCobrosColegio.Models
{
    public class Apoderado
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Documento")]
        public string? Tipo_Documento { get; set; }

        [Display(Name = "Numero")]
        public string? Numero_Documento { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        [Display(Name = "Nombre")]
        public string? Nombre_Completo { get { return Nombre + " " + Apellido; } }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Relacion { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        public DateTime Fecha_Nacimiento { get; set; }

        [Display(Name = "Fecha Registro")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Name = "Fecha Actualizacion")]
        public DateTime Fecha_Actualizacion { get; set; }
        public bool Estado { get; set; }
    }
}
