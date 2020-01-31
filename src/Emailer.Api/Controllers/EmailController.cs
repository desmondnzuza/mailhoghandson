using Emailer.Api.Models;
using Emailer.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Emailer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public void Post([FromBody] GreetingRequest request)
        {
            var mailer = new EmailerClient(new HardCodedEmailSettingsProvider());
            mailer.SendGreetingEmailTo(request.EmailAddressToUse);
        }
    }
}