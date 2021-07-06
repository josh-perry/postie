using System;
using System.Collections.Generic;
using Postie.Api.Models.Db;

namespace Postie.Api.Models.Responses
{
    public class CommentApiResponse
    {
        public int ID { get; set; }
        
        public string User { get; set; }

        public string Content { get; set; }

        public int? ParentCommentId { get; set; }
        
        public List<Comment> Children { get; set; }
        
        public string Board { get; set; }
        
        public string Post { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}
