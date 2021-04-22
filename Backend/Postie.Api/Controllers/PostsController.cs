using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Models;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : Controller
    {
        private readonly List<PostListing> _posts = new List<PostListing>
        {
            new PostListing
            {
                Title = "Post the first",
                Hidden = false,
                Username = "/u/someone",
                CommentCount = 52,
                PostedDateTime = DateTime.Now.AddHours(-5)
            },
            new PostListing
            {
                Title = "Post the second",
                Hidden = false,
                Username = "/u/someone-else",
                CommentCount = 10,
                PostedDateTime = DateTime.Now.AddHours(-2)
            },
           new PostListing
           {
               Title = "Secret post",
               Hidden = true,
               Username = "/u/someone-else",
               CommentCount = 2,
               PostedDateTime = DateTime.Now.AddHours(-3)
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