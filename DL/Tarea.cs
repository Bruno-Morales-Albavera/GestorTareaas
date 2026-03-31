using System;
using System.Collections.Generic;

namespace DL;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaLimite { get; set; }

    public string Prioridad { get; set; } = null!;

    public string Estatus { get; set; } = null!;
}
