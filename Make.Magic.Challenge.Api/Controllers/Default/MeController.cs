using Microsoft.AspNetCore.Mvc;

namespace Make.Magic.Challenge.Api.Controllers.Default
{
    [ApiController, Route("api/[controller]")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { name = "Make Magic - Challenge", version = "1.0" });
    }
}
