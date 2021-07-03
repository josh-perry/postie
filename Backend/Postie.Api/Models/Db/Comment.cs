using System;
using System.ComponentModel.DataAnnotations;

namespace Postie.Api.Models.Db
{
    public class Comment
    {
        public int ID { get; set; }

        [Required]
        public User CreatedBy { get; set; }

        [Required]
        public Post Post { get; set; }

        public Comment ParentComment { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(4000)]
        public string Content { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
