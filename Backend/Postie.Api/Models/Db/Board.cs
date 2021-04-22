using System;

namespace Postie.Api.Models.Db
{

    public class Board
    {
        public int ID { get; set; }
        
        public string Title { get; set; }
        
        public User CreatedBy { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}
