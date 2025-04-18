using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPolizas.Models;

namespace WebApiPolizas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoberturaController : ControllerBase
    {
        private readonly PolizasDBContext _context;

        public CoberturaController(PolizasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cobertura/Lista
        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<object>>> GetCobertura()
        {
            var tipos = await _context.Coberturas
                .Select(tp => new
                {
                    tp.CoberturaId,
                    tp.Nombre
                })
                .ToListAsync();

            return Ok(tipos);
        }
    }
}

