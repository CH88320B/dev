using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiPolizas.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace WebApiPolizas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly PolizasDBContext dbContext;

        public PolizaController(PolizasDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            var planos = await dbContext.PolizaFlat
                .FromSqlRaw("EXEC sp_ListarPolizas")
                .AsNoTracking()
                .ToListAsync();

            var resultado = planos.Select(p => new Poliza
            {
                NumeroPoliza = p.NumeroPoliza,
                TipoPolizaId = p.TipoPolizaId,
                CedulaAsegurado = p.Poliza_CedulaAsegurado,
                MontoAsegurado = p.MontoAsegurado,
                FechaVencimiento = p.FechaVencimiento,
                FechaEmision = p.FechaEmision,
                CoberturaId = p.CoberturaId,
                EstadoPolizaId = p.EstadoPolizaId,
                Prima = p.Prima,
                Periodo = p.Periodo,
                FechaInclusion = p.FechaInclusion,
                AseguradoraId = p.AseguradoraId,

                TipoPoliza = new TipoPoliza
                {
                    TipoPolizaId = p.TipoPoliza_TipoPolizaId,
                    Nombre = p.TipoPoliza_Nombre
                },
                Cliente = new Cliente
                {
                    CedulaAsegurado = p.Cliente_CedulaAsegurado,
                    Nombre = p.Cliente_Nombre,
                    PrimerApellido = p.Cliente_PrimerApellido,
                    SegundoApellido = p.Cliente_SegundoApellido,
                    TipoPersona = p.Cliente_TipoPersona,
                    FechaNacimiento = p.Cliente_FechaNacimiento
                },
                Cobertura = new Cobertura
                {
                    CoberturaId = p.Cobertura_CoberturaId,
                    Nombre = p.Cobertura_Nombre
                },
                EstadoPoliza = new EstadoPoliza
                {
                    EstadoPolizaId = p.EstadoPoliza_EstadoPolizaId,
                    Nombre = p.EstadoPoliza_Nombre
                },
                Aseguradora = new Aseguradora
                {
                    AseguradoraId = p.Aseguradora_AseguradoraId,
                    Nombre = p.Aseguradora_Nombre
                }
            }).ToList();

            return Ok(resultado);
        }




        // 2. Buscar con filtros
        [HttpGet("Buscar")]
        public async Task<IActionResult> Buscar(
    [FromQuery] string? numeroPoliza,
    [FromQuery] int? tipoPolizaId,
    [FromQuery] DateTime? fechaVencimiento,
    [FromQuery] string? cedulaAsegurado,
    [FromQuery] string? nombreApellido)
        {
            var planos = await dbContext.PolizaFlat
                .FromSqlRaw("EXEC sp_BuscarPolizas @p0, @p1, @p2, @p3, @p4",
                    numeroPoliza ?? (object)DBNull.Value,
                    tipoPolizaId ?? (object)DBNull.Value,
                    fechaVencimiento ?? (object)DBNull.Value,
                    cedulaAsegurado ?? (object)DBNull.Value,
                    nombreApellido ?? (object)DBNull.Value)
                .AsNoTracking()
                .ToListAsync();

            var resultado = planos.Select(p => new Poliza
            {
                NumeroPoliza = p.NumeroPoliza,
                TipoPolizaId = p.TipoPolizaId,
                CedulaAsegurado = p.Poliza_CedulaAsegurado,
                MontoAsegurado = p.MontoAsegurado,
                FechaVencimiento = p.FechaVencimiento,
                FechaEmision = p.FechaEmision,
                CoberturaId = p.CoberturaId,
                EstadoPolizaId = p.EstadoPolizaId,
                Prima = p.Prima,
                Periodo = p.Periodo,
                FechaInclusion = p.FechaInclusion,
                AseguradoraId = p.AseguradoraId,

                TipoPoliza = new TipoPoliza
                {
                    TipoPolizaId = p.TipoPoliza_TipoPolizaId,
                    Nombre = p.TipoPoliza_Nombre
                },
                Cliente = new Cliente
                {
                    CedulaAsegurado = p.Cliente_CedulaAsegurado,
                    Nombre = p.Cliente_Nombre,
                    PrimerApellido = p.Cliente_PrimerApellido,
                    SegundoApellido = p.Cliente_SegundoApellido,
                    TipoPersona = p.Cliente_TipoPersona,
                    FechaNacimiento = p.Cliente_FechaNacimiento
                },
                Cobertura = new Cobertura
                {
                    CoberturaId = p.Cobertura_CoberturaId,
                    Nombre = p.Cobertura_Nombre
                },
                EstadoPoliza = new EstadoPoliza
                {
                    EstadoPolizaId = p.EstadoPoliza_EstadoPolizaId,
                    Nombre = p.EstadoPoliza_Nombre
                },
                Aseguradora = new Aseguradora
                {
                    AseguradoraId = p.Aseguradora_AseguradoraId,
                    Nombre = p.Aseguradora_Nombre
                }
            }).ToList();

            return Ok(resultado);
        }

        // 3. Insertar
        [HttpPost("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Poliza modelo)
        {
            await dbContext.Database.ExecuteSqlRawAsync(
        "EXEC sp_InsertarPoliza @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11",
        modelo.NumeroPoliza,
        modelo.TipoPolizaId,
        modelo.CedulaAsegurado,
        modelo.MontoAsegurado,
        modelo.FechaVencimiento,
        modelo.FechaEmision,
        modelo.CoberturaId,
        modelo.EstadoPolizaId,
        modelo.Prima,
        modelo.Periodo,
        modelo.FechaInclusion,
        modelo.AseguradoraId
    );
            return Ok(new { mensaje = "ok" });
        }

        // 4. Editar
        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] Poliza modelo)
        {
            await dbContext.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarPoliza @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11",
                modelo.NumeroPoliza,
                modelo.TipoPolizaId,
                modelo.CedulaAsegurado,
                modelo.MontoAsegurado,
                modelo.FechaVencimiento,
                modelo.FechaEmision,
                modelo.CoberturaId,
                modelo.EstadoPolizaId,
                modelo.Prima,
                modelo.Periodo,
                modelo.FechaInclusion,
                modelo.AseguradoraId
            );

            return Ok(new { mensaje = "ok" });
        }


        // 5. Elim}inar
        [HttpDelete("Eliminar/{numeroPoliza}")]
        public async Task<IActionResult> Eliminar(string numeroPoliza)
        {
            await dbContext.Database.ExecuteSqlRawAsync("EXEC sp_EliminarPoliza @p0", numeroPoliza);
            return Ok(new { mensaje = "ok" });
        }

    }
}
