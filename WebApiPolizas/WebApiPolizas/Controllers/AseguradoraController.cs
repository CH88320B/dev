using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiPolizas.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiPolizas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AseguradoraController : ControllerBase
{
    private readonly PolizasDBContext _context;

    public AseguradoraController(PolizasDBContext context)
    {
        _context = context;
    }

    [HttpGet("lista")]
    public async Task<IActionResult> Get()
    {
        var lista = await _context.Aseguradoras.ToListAsync();
        return Ok(lista);
    }

    [HttpPost("nueva")]
    public async Task<IActionResult> Post([FromBody] Aseguradora aseguradora)
    {
        _context.Aseguradoras.Add(aseguradora);
        await _context.SaveChangesAsync();
        return Ok(new { mensaje = "Agregado" });
    }
}
