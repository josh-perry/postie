using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Postie.Api.Controllers
{
    public class Post
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public bool Hidden { get; set; }
    }

    [ApiController]
    [Route("posts")]
    public class PostsController : Controller
    {
        private readonly List<Post> _posts = new List<Post>
        {
            new Post
            {
                Name = "Post the first",
                Content = "some content",
                Hidden = false
            },
            new Post
            {
                Name = "Post the second",
                Content = "some content",
                Hidden = false
            },
            new Post
            {
                Name = "Hidden post",
                Content = "logged in users only!",
                Hidden = true
            }
        };
        
        [HttpGet]
        [Route("")]
        public IActionResult ListAll()
        {
            if (User.Identity.IsAuthenticated)
                return Json(_posts);

            return Json(_posts.Where(x => !x.Hidden));
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var post = _posts.ElementAtOrDefault(id);

            if (post == null || post.Hidden)
            {
                return NotFound();
            }

            return Json(post);
        }
    }
}