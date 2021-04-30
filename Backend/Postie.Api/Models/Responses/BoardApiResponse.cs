using System;

namespace Postie.Api.Models.Responses
{
    public class BoardApiResponse
    {
        public string Title { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
