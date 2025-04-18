using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPolizas.Models;

public partial class EstadoPoliza
{

    [Key]
    [MaxLength(20)]
    public int EstadoPolizaId { get; set; }

    public string? Nombre { get; set; }

    // Navegación inversa (un estado puede tener muchas pólizas)
    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
