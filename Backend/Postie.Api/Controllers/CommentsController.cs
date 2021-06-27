using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
using Postie.Api.Models.Db;
using Postie.Api.Models.Requests;
using Postie.Api.Repositories;
using Postie.Api.Services;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("comment")]
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IFetchPostService _fetchPostService;

        private readonly IUserRepository _userRepository;
        
        private readonly CommentResponseMapper _commentResponseMapper;

        public CommentsController(ICommentRepository commentRepository,
            IFetchPostService fetchPostService,
            IUserRepository userRepository,
            CommentResponseMapper commentResponseMapper)
        {
            _userRepository = userRepository;
            _commentResponseMapper = commentResponseMapper;
            _commentRepository = commentRepository;
            _fetchPostService = fetchPostService;
        }

        /// <summary>
        ///     Get comments by board and post
        /// </summary>
        /// <param name="boardUrl"></param>
        /// <param name="postUrl"></param>
        /// <param name="childrenOf"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("{boardUrl}/{postUrl}")]
        public IActionResult Get(string boardUrl, string postUrl, int childrenOf)
        {
            var comments = _commentRepository.GetCommentsForPost(boardUrl, postUrl, childrenOf);
            return Json(_commentResponseMapper.MapDbToResponseList(comments));
        }

        /// <summary>
        ///     Add a new comment to a post
        /// </summary>
        /// <param name="boardUrl"></param>
        /// <param name="postUrl"></param>
        /// <param name="postComment"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [Authorize]
        [Route("{boardUrl}/{postUrl}")]
        public IActionResult Post(string boardUrl, string postUrl, PostComment postComment)
        {
            var post = _fetchPostService.GetPostByBoardAndUrl(boardUrl, postUrl);
            var parentComment = postComment.ParentCommentId != null ? _commentRepository.GetCommentById(postComment.ParentCommentId.Value) : null;
            var user = _userRepository.GetUserByAuthId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (user == null)
                return BadRequest("User is null.");

            // If we're replying to a comment, ensure that comment is on the same post that we're attempting to comment on
            if (parentComment != null && parentComment.Post != post)
                return BadRequest("Parent comment doesn't belong to the post you're replying to!");

            var comment = new Comment
            {
                Content = postComment.Content,
                Post = post,
                ParentComment = parentComment,
                CreatedBy = user
            };

            _commentRepository.AddComment(comment);
            return Ok();
        }
    }
}
