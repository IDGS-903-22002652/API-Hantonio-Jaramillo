using System;
using System.ComponentModel.DataAnnotations;

namespace API_Hantonio_Jaramillo.DTOs;

public class UsuarioDtoResponse
{
    public int IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public int? IdRol { get; set; }

    public string? NombreRol { get; set; }

    public int? IdSucursal { get; set; }

    public string? NombreSucursal { get; set; }

    public bool Activo { get; set; }

    public DateTime? UltimoLogin { get; set; }
}

public class UsuarioDtoCreateRequest
{
    [Required(ErrorMessage = "El login es requerido")]
    [StringLength(50, ErrorMessage = "El login no puede exceder 50 caracteres")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    [StringLength(255, ErrorMessage = "La contraseña (hash) no puede exceder 255 caracteres")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre completo es requerido")]
    [StringLength(150, ErrorMessage = "El nombre completo no puede exceder 150 caracteres")]
    public string NombreCompleto { get; set; } = string.Empty;

    public int? IdRol { get; set; }

    public int? IdSucursal { get; set; }
}

public class UsuarioDtoUpdateRequest
{
    [StringLength(150, ErrorMessage = "El nombre completo no puede exceder 150 caracteres")]
    public string? NombreCompleto { get; set; }

    public int? IdRol { get; set; }

    public int? IdSucursal { get; set; }

    public bool? Activo { get; set; }
}

public class UsuarioDtoCambiarEstado
{
    public bool Activo { get; set; }
}