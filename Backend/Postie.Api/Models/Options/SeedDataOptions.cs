using System.Collections.Generic;

namespace Postie.Api.Models.Options
{
    public class SeedDataOptions
    {
        public List<DefaultUser> Users { get; set; }
        
        public List<DefaultBoard> SystemBoards { get; set; }
    }

    public class DefaultBoard
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
    }

    public class DefaultUser
    {
        public string Username { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
