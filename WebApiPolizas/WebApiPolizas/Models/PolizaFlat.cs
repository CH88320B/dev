using Microsoft.EntityFrameworkCore;

namespace WebApiPolizas.Models
{
    [Keyless]
    public class PolizaFlat
    {
        public string NumeroPoliza { get; set; }
        public int TipoPolizaId { get; set; }
        public string Poliza_CedulaAsegurado { get; set; }
        public decimal MontoAsegurado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaEmision { get; set; }
        public int CoberturaId { get; set; }
        public int EstadoPolizaId { get; set; }
        public decimal Prima { get; set; }
        public DateTime Periodo { get; set; }
        public DateTime FechaInclusion { get; set; }
        public int AseguradoraId { get; set; }
       

        // Datos anidados
        public int TipoPoliza_TipoPolizaId { get; set; }
        public string TipoPoliza_Nombre { get; set; }
      
        public string Cliente_CedulaAsegurado { get; set; }
        public string Cliente_Nombre { get; set; }
        public string Cliente_PrimerApellido { get; set; }
        public string Cliente_SegundoApellido { get; set; }
        public string Cliente_TipoPersona { get; set; }
        public DateTime Cliente_FechaNacimiento { get; set; }

        public int Cobertura_CoberturaId { get; set; }
        public string Cobertura_Nombre { get; set; }

        public int EstadoPoliza_EstadoPolizaId { get; set; }
        public string EstadoPoliza_Nombre { get; set; }

        public int Aseguradora_AseguradoraId { get; set; }
        public string Aseguradora_Nombre { get; set; }
    }

}
