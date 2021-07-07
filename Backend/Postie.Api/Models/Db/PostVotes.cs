using System.ComponentModel.DataAnnotations;

namespace Postie.Api.Models.Db
{
    public class PostVote
    {
        public int ID { get; set; }

        [Required]
        public Post Post { get; set; }

        public int PostID { get; set; }

        [Required]
        public User User { get; set; }

        public int UserID { get; set; }

        [Required]
        public bool Up { get; set; }
    }
}
