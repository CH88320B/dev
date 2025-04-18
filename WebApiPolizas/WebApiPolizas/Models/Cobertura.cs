using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPolizas.Models;

public partial class Cobertura
{
    [Key]
    public int CoberturaId { get; set; }

    public string? Nombre { get; set; }

    // Navegación inversa (una cobertura puede aplicarse a muchas pólizas)
    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
