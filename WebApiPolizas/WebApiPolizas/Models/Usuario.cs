namespace WebApiPolizas.Models;

public partial class Usuario
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Contrasena { get; set; } = null!;
    public string? NombreCompleto { get; set; }
    public string? Rol { get; set; }
    public bool Activo { get; set; }
}
