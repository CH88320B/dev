using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPolizas.Models;

namespace WebApiPolizas.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly PolizasDBContext _context;

    public ClientesController(PolizasDBContext context)
    {
        _context = context;
    }

    // GET: api/clientes/Lista
    [HttpGet("Lista")]
    public async Task<ActionResult<IEnumerable<Cliente>>> Lista()
    {
        return await _context.Clientes.ToListAsync();
    }

    // GET: api/clientes/Buscar/123456789
    [HttpGet("Buscar/{id}")]
    public async Task<ActionResult<Cliente>> Buscar(string id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            return NotFound();

        return cliente;
    }

    // POST: api/clientes/Agregar
    [HttpPost("Agregar")]
    public async Task<ActionResult<Cliente>> Agregar(Cliente cliente)
    {
        _context.Clientes.Add(cliente);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (ClienteExiste(cliente.CedulaAsegurado))
                return Conflict("Ya existe un cliente con esa cédula.");
            else
                throw;
        }

        return CreatedAtAction(nameof(Buscar), new { id = cliente.CedulaAsegurado }, cliente);
    }

    // PUT: api/clientes/Actualizar/123456789
    [HttpPut("Actualizar/{id}")]
    public async Task<IActionResult> Actualizar(string id, Cliente cliente)
    {
        if (id != cliente.CedulaAsegurado)
            return BadRequest("La cédula del cliente no coincide con la URL.");

        _context.Entry(cliente).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteExiste(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/clientes/Eliminar/123456789
    [HttpDelete("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(string id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return NotFound();

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ClienteExiste(string id)
    {
        return _context.Clientes.Any(e => e.CedulaAsegurado == id);
    }
}
