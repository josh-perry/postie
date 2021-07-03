using System;
using System.ComponentModel.DataAnnotations;

namespace Postie.Api.Models.Db
{

    public class Board
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public User CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
