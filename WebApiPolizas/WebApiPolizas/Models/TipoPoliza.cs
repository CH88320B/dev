using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPolizas.Models;

public partial class TipoPoliza
{
    [Key]
    public int TipoPolizaId { get; set; }

    public string? Nombre { get; set; }

   // un tipo puede aplicarse a muchas pólizas
    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
