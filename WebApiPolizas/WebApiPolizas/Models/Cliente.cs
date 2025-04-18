using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPolizas.Models;
public partial class Cliente
{
    [Key]
    [MaxLength(20)]
    public string CedulaAsegurado { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string? TipoPersona { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    // Navegación inversa si se necesita (Polizas)
    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}

