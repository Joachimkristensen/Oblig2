using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/comment")]
    [ApiController]
    public class CommentApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentRepository _repository;

        public CommentApiController(ICommentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/CommentAPI
        /* [HttpGet]
         public async Task<IEnumerable<Comment>> GetComments()
         {
             return await _repository.GetAll();
         } */


        // GET: api/comment/5
        [HttpGet("{id}")]
        public ActionResult<List<Comment>> GetComments(int id)
        {
            var viewModel = _repository.GetAll(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return viewModel.Comments;
        }


        // GET: api/comment/5/5
        [HttpGet("{postId}/{commentId}")]
        public async Task<ActionResult<Comment>> GetComment(int postId, int commentId)
        {
            if (!CommentExists(commentId))
            {
                return NotFound();
            }

            var blogPost = await _context.Posts.FindAsync(postId);
            if (blogPost == null)
            {
                return NotFound();
            }

            var comment = blogPost.Comments.Find(c => c.CommentId == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/comment/
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            await _repository.Save(comment, User);

            return CreatedAtAction("GetComments", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/comment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
