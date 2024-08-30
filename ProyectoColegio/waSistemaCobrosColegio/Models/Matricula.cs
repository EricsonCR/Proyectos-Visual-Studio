using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace waSistemaCobrosColegio.Models
{
    public class Matricula
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        [Display(Name = "Estudiante")]
        public int Id_Estudiante { get; set; }
        public int Id_Apoderado { get; set; }

        [Required]
        public string? Periodo { get; set; }
        public string? Nivel { get; set; }
        public string? Grado { get; set; }
        public string? Seccion { get; set; }
        public string? Situacion { get; set; }
        public string? Descripcion { get; set; }

        [Display(Name = "Fecha Registro")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Name = "Fecha Actualizacion")]
        public DateTime Fecha_Actualizacion { get; set; }
        public string? Estado { get; set; }
    }
}
