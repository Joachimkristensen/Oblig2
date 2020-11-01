using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Oblig2.Models.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> Manager { get; }

        public CommentRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            Manager = userManager;
            _db = db;
        }

        public PostEditViewModel GetAll(int id)
        {
            var post = _db.Posts.FirstOrDefault(comments => comments.PostId == id);
            var viewModel = new PostEditViewModel()
            {
                PostId = post.PostId,
                Name = post.Name,
                Description = post.Description,
                Owner = post.Owner,
                CreationDate = post.CreationDate,
                Comments = new List<Comment>(_db.Comments.Where(comments => comments.Post.PostId == id).ToList())
            };

            return viewModel;
        }


        public async Task<IEnumerable<Comment>> GetAll()
        {
            IEnumerable<Comment>
                comments = await _db.Comments
                    .ToListAsync();

            return comments;
        }

        public async Task<CommentViewModel> GetCommentViewModel(int id)
        {
            var comment = await _db.Comments.FindAsync(id);
            return new CommentViewModel
            {
                CommentId = comment.CommentId,
                Description = comment.Description,
                UserName = comment.UserName,
                CreationDate = comment.CreationDate,
            };
        }

        public CommentViewModel GetCommentViewModel()
        {
            return new CommentViewModel();
        }

        public async Task Save(Comment comment, ClaimsPrincipal principal)
        {
            var owner = await Manager.FindByNameAsync(principal.Identity.Name);

            var newComment = new Comment()
            {
                Owner = owner,
                UserName = owner.UserName,
                CreationDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Description = comment.Description,
                Post = await _db.Posts.FindAsync(comment.PostId)
            };

            await _db.Comments.AddAsync(newComment);
            var result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Error saving changes");
            }
        }
    }
}
