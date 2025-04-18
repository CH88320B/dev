using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPolizas.Models;

public partial class Aseguradora
{
    [Key]
    [MaxLength(20)]
    public int AseguradoraId { get; set; }

    public string? Nombre { get; set; }

    // Navegación inversa (una aseguradora puede tener muchas pólizas)
    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
