using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiPolizas.Models;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly PolizasDBContext _context;
    private readonly ILogger<AuthController> _logger;
    

   

    public AuthController(PolizasDBContext context, ILogger<AuthController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("Acceso")]
    public async Task<IActionResult> Login([FromBody] LoginModel logines)
    {
        // 🔹 Paso 1: Traé los usuarios activos de la BD (solo los necesarios)
        var usuariosActivos = await _context.Usuarios
            .Where(u => u.Activo)
            .ToListAsync();

        // 🔹 Paso 2: Hacé la comparación manualmente en memoria
        var loginSanitizado = logines.Login?.Trim().ToLower();
        var contrasenaSanitizada = logines.Contrasena?.Trim();

        var user = usuariosActivos.FirstOrDefault(u =>
            u.Login.Trim().ToLower() == loginSanitizado &&
            u.Contrasena.Trim() == contrasenaSanitizada);


        Console.WriteLine($"Intentando login: {logines.Login}, contraseña: {logines.Contrasena}");


        if (user == null)
        {

            _logger.LogWarning("Intento fallido de login para usuario: {Login}", logines.Login);
            return Unauthorized("Usuario o contraseña incorrectos");
        }

        //  Obtener clave JWT desde tabla Configuracion
        var jwtKey = await _context.Configuracion
            .FirstOrDefaultAsync(c => c.Clave == "JWT_SECRET");

        if (jwtKey == null || string.IsNullOrWhiteSpace(jwtKey.Valor))
            return StatusCode(500, "No se encontró la clave JWT");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey.Valor));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.NombreCompleto ?? user.Login),
        new Claim(ClaimTypes.Role, user.Rol ?? "Usuario")
    };

        var token = new JwtSecurityToken(
            issuer: "tu-app",
            audience: "tu-app",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            nombre = user.NombreCompleto,
            rol = user.Rol
        });
    }


}
