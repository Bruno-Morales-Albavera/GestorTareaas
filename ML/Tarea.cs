using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        [Required(ErrorMessage ="Campo Titulo obligatorio")]
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Required(ErrorMessage ="Fecha límite obligatorio")]
        public DateTime FechaLimite { get; set; }
        [Required]
        public string? Prioridad { get; set; }
        [Required]
        public string? Estatus { get; set; }

        public List<Tarea>? Tareas { get; set; }
    }
}
