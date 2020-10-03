using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2.Authorization;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _db;
        private UserManager<IdentityUser> Manager { get; }
        private IAuthorizationService _authorizationService;

        public PostRepository(UserManager<IdentityUser> userManager, ApplicationDbContext db, IAuthorizationService authorizationService = null)
        {
            Manager = userManager;
            _db = db;
            _authorizationService = authorizationService;
        }

        public async Task <BlogEditViewModel> GetAll(int id)
        {
            var blog = await _db.Blogs.FirstOrDefaultAsync(posts => posts.BlogId == id);
            var viewModel = new BlogEditViewModel
            {
                BlogId = blog.BlogId,
                Name = blog.Name,
                Description = blog.Description,
                Owner = blog.Owner,
                CreationDate = blog.CreationDate,
                Posts = new List<Post>(await _db.Posts.Where(posts => posts.Blog.BlogId == id).ToListAsync())
            };

            return viewModel;
        }

        public async Task<PostEditViewModel> GetPostEditViewModel(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            return new PostEditViewModel
            {
                PostId = post.PostId,
                Description = post.Description,
                Name = post.Name,
                UserName = post.UserName,
                CreationDate = post.CreationDate,
                Comments = new List<Comment>(await _db.Comments.Where(comments => comments.Post.PostId == id)
                    .ToListAsync())
            };
        }

        public async Task<Post> GetPost(int id)
        {
            var post = (from p in _db.Posts
                where p.PostId == id
                select p).FirstOrDefaultAsync();

            return await post;
        }


        public PostEditViewModel GetPostEditViewModel()
        {
            return new PostEditViewModel();
        }


        public async Task Save(PostEditViewModel viewModel, ClaimsPrincipal principal)
        {
            var owner = await Manager.FindByNameAsync(principal.Identity.Name);

            var newPost = new Post
            {
                Owner = owner,
                UserName = owner.UserName,
                CreationDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Description = viewModel.Description,
                Name = viewModel.Name,
                Blog = await _db.Blogs.FindAsync(viewModel.BlogId)
            };

            await _db.Posts.AddAsync(newPost);
            var result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Error saving changes");
            }
        }
    }

}

