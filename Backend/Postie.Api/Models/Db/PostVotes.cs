using System.ComponentModel.DataAnnotations;

namespace Postie.Api.Models.Db
{
    public class PostVote
    {
        public int ID { get; set; }
        
        [Required]
        public Post Post { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public bool Up { get; set; }
    }
}
