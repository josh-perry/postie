using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var claims = HttpContext.User.Claims.Select(x => x.Value);
            return Json(claims);
        }
    }
}
