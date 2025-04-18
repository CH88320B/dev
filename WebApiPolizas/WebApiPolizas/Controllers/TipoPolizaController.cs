using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPolizas.Models;

namespace WebApiPolizas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPolizaController : ControllerBase
    {
        private readonly PolizasDBContext _context;

        public TipoPolizaController(PolizasDBContext context)
        {
            _context = context;
        }

        // GET: api/TipoPoliza/Lista
        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<object>>> GetTiposPoliza()
        {
            var tipos = await _context.TiposPoliza
                .Select(tp => new
                {
                    tp.TipoPolizaId,
                    tp.Nombre
                })
                .ToListAsync();

            return Ok(tipos);
        }
    }
}
