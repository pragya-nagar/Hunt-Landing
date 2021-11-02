using System.Reflection;

using Microsoft.AspNetCore.Mvc;

namespace Synergy.Landing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : Controller
    {
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            var version = Assembly.GetEntryAssembly()
                                  .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                  .InformationalVersion;
            return Ok(new { Version = version });
        }
    }
}