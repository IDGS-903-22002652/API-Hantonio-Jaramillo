namespace API_Hantonio_Jaramillo.DTOs;

public class LoginRequest
{
    public string Login { get; set; } = string.Empty;
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
    public string? Login { get; set; }
    public string? Rol { get; set; }
}