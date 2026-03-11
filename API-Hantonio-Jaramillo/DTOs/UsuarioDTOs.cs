using System;
using System.ComponentModel.DataAnnotations;

namespace API_Hantonio_Jaramillo.DTOs;

public class UsuarioDtoResponse
{
    public int IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string NombreUsuario { get; set; } = string.Empty;

    public string? Email { get; set; }

    public int IdRol { get; set; }

    public string? NombreRol { get; set; }

    public int? IdSucursal { get; set; }

    public string? NombreSucursal { get; set; }

    public bool Estatus { get; set; }

    public DateTime? UltimoLogin { get; set; }
}

public class UsuarioDtoCreateRequest
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    [StringLength(255, ErrorMessage = "La contraseña es demasiado larga")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre completo es requerido")]
    [StringLength(150, ErrorMessage = "El nombre completo no puede exceder 150 caracteres")]
    public string NombreCompleto { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
    [StringLength(100)]
    public string? Email { get; set; } 

    [Required(ErrorMessage = "El Rol es obligatorio")]
    public int IdRol { get; set; }

    public int? IdSucursal { get; set; }
}


public class UsuarioDtoUpdateRequest
{
    [StringLength(150)]
    public string? NombreCompleto { get; set; }

    [StringLength(50)]
    public string? NombreUsuario { get; set; }

    public string? Password { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    public int? IdRol { get; set; }

    public int? IdSucursal { get; set; }

    public bool? Estatus { get; set; }
}

public class UsuarioDtoCambiarEstado
{
    [Required]
    public bool Estatus { get; set; }
}