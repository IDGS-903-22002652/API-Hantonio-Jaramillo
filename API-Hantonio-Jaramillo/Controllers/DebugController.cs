using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace API_Hantonio_Jaramillo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    [HttpGet("claims")]
    [Authorize]
    public IActionResult GetClaims()
    {
        var isAuthenticated = User?.Identity?.IsAuthenticated ?? false;
        var name = User?.Identity?.Name;

        List<object> claims;
        if (User?.Claims != null)
        {
            claims = User.Claims
                .Select(c => (object)new { Type = c.Type, Value = c.Value })
                .ToList();
        }
        else
        {
            claims = new List<object>();
        }

        var roles = User?.Claims
            .Where(c => c.Type == "role" || c.Type == System.Security.Claims.ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList() ?? new List<string>();

        return Ok(new { isAuthenticated, name, roles, claims });
    }
}