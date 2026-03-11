namespace API_Hantonio_Jaramillo.DTOs;
public class LoginRequest
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LogoutRequest
{
    public int IdUsuario { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public UsuarioInfo? Usuario { get; set; }
}

public class UsuarioInfo
{
    public int IdUsuario { get; set; }
    public string? NombreCompleto { get; set; }
    public string? NombreUsuario { get; set; }
    public string? Email { get; set; }
    public string? Rol { get; set; }
    public int? IdSucursal { get; set; }
    public string? NombreSucursal { get; set; }
}