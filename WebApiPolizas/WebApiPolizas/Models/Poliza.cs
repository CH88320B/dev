using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApiPolizas.Models;

public class Poliza
{
    [Key]
    public string NumeroPoliza { get; set; } = null!;

    public int TipoPolizaId { get; set; }

    public string CedulaAsegurado { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal MontoAsegurado { get; set; }

    [Column(TypeName = "date")]
    public DateTime FechaVencimiento { get; set; }

    [Column(TypeName = "date")]
    public DateTime FechaEmision { get; set; }

    public int CoberturaId { get; set; }
    public int EstadoPolizaId { get; set; }

    public decimal Prima { get; set; }

    [Column(TypeName = "date")]
    public DateTime Periodo { get; set; }

    [Column(TypeName = "date")]
    public DateTime FechaInclusion { get; set; }

    public int AseguradoraId { get; set; }

    // RELACIONES ANIDADAS (ahora nullable y con JsonIgnore opcional)

    [ForeignKey(nameof(TipoPolizaId))]
    
    public virtual TipoPoliza? TipoPoliza { get; set; }

    [ForeignKey(nameof(CedulaAsegurado))]
   
    public virtual Cliente? Cliente { get; set; }

    [ForeignKey(nameof(CoberturaId))]
    
    public virtual Cobertura? Cobertura { get; set; }

    [ForeignKey(nameof(EstadoPolizaId))]
   
    public virtual EstadoPoliza? EstadoPoliza { get; set; }

    [ForeignKey(nameof(AseguradoraId))]
   
    public virtual Aseguradora? Aseguradora { get; set; }
}
