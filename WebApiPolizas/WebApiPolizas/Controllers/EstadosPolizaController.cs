using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPolizas.Models;

namespace WebApiPolizas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosPolizaController : ControllerBase
    {
        private readonly PolizasDBContext _context;

        public EstadosPolizaController(PolizasDBContext context)
        {
            _context = context;
        }

        // GET: api/EstadosPoliza/Lista
        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<object>>> GetCobertura()
        {
            var tipos = await _context.EstadosPoliza
                .Select(tp => new
                {
                    tp.EstadoPolizaId,
                    tp.Nombre
                })
                .ToListAsync();

            return Ok(tipos);
        }
    }
}


