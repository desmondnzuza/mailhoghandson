using System.Collections.Generic;

namespace Emailer.IntegrationTests.Helpers.Models
{
    public class EmailHeaders
    {
        public List<string> To { get; set; }
        public List<string> From { get; set; }
        public List<string> Subject { get; set; }
    }
}
