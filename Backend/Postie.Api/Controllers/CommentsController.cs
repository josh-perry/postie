using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
using Postie.Api.Models.Db;
using Postie.Api.Models.Requests;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("comment")]
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        private readonly CommentResponseMapper _commentResponseMapper;

        private readonly IPostRepository _postRepository;

        private readonly IUserRepository _userRepository;

        public CommentsController(ICommentRepository commentRepository,
            IPostRepository postRepository,
            IUserRepository userRepository,
            CommentResponseMapper commentResponseMapper)
        {
            _userRepository = userRepository;
            _commentResponseMapper = commentResponseMapper;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        /// <summary>
        ///     Get comments by board and post and optionally only the children of specified
        ///     post ID
        /// </summary>
        /// <param name="boardUrl"></param>
        /// <param name="postUrl"></param>
        /// <param name="childrenOf"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("{boardUrl}/{postUrl}")]
        public IActionResult GetByUrls(string boardUrl, string postUrl, int childrenOf)
        {
            var comments = _commentRepository.GetCommentsForPost(boardUrl, postUrl, childrenOf);
            return Json(_commentResponseMapper.MapDbToResponseList(comments));
        }

        /// <summary>
        ///     Add a new comment to a post
        /// </summary>
        /// <param name="boardUrl"></param>
        /// <param name="postUrl"></param>
        /// <param name="addCommentRequest"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [Authorize]
        [Route("{boardUrl}/{postUrl}")]
        public IActionResult Post(string boardUrl, string postUrl, AddCommentRequest addCommentRequest)
        {
            var post = _postRepository.GetPostByBoardAndUrl(boardUrl, postUrl);
            var parentComment = addCommentRequest.ParentCommentId != null
                ? _commentRepository.GetCommentById(addCommentRequest.ParentCommentId.Value)
                : null;

            var user = _userRepository.GetUserByUsername(User.FindFirst(ClaimTypes.Name)?.Value);

            if (user == null)
                return BadRequest("User is null.");

            // If we're replying to a comment, ensure that comment is on the same post that we're attempting to comment on
            if (parentComment != null && parentComment.Post != post)
                return BadRequest("Parent comment doesn't belong to the post you're replying to!");

            var comment = new Comment
            {
                Content = addCommentRequest.Content,
                Post = post,
                ParentComment = parentComment,
                CreatedBy = user,
                CreatedDateTime = DateTime.UtcNow
            };

            if(!_commentRepository.AddComment(comment))
                return Problem("Failed to create comment");

            return Json(_commentResponseMapper.MapDbToResponse(comment));
        }
    }
}
