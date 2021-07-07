using System.Collections.Generic;

namespace Postie.Api.Models.Options
{
    public class SeedDataOptions
    {
        public List<DefaultUser> Users { get; set; }
    }

    public class DefaultUser
    {
        public string Username { get; set; }

        public string AuthId { get; set; }
    }
}
