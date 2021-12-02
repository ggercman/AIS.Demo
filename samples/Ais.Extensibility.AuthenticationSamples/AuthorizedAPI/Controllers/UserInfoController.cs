using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizedAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var userClaims = this.User.Claims
                .Select(f => f.Type + ": " + f.Value)
                .DefaultIfEmpty()
                .Aggregate(string.Empty, (a, b) => a + ", " + b);

            return Ok(new
            {
                Admin = false,
                Claims = this.User.Claims.Select(f => new { Type = f.Type, Value = f.Value }),
                Summary = userClaims
            });
        }

        [Authorize(Roles = "Administrators")]
        [HttpGet("Admin")]
        public IActionResult GetForAdmin()
        {
            var userClaims = this.User.Claims
                .Select(f => f.Type + ": " + f.Value)
                .DefaultIfEmpty()
                .Aggregate(string.Empty, (a, b) => a + ", " + b);

            return Ok(new
            {
                Admin = true,
                Claims = this.User.Claims.Select(f => new { Type = f.Type, Value = f.Value }),
                Summary = userClaims
            });
        }
    }
}