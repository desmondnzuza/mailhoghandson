using Microsoft.AspNetCore.Mvc;

namespace Emailer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        // GET api/version
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "1.0.0";
        }
    }
}