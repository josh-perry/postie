using System.ComponentModel.DataAnnotations;

namespace Postie.Api.Models.Db
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string AuthId { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
