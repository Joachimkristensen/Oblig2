using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2.Data;
using Oblig2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Oblig2.Models.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> Manager { get; }

        public BlogRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            Manager = userManager;
            _db = db;
        }

        public IEnumerable<Blog> GetAll()
        {
            return _db.Blogs;
        }

        public async Task<Blog> GetBlogWithId(int id)
        {
            var blog = await _db.Blogs.FindAsync(id);
            return blog;
        }

        public async Task<List<BlogApplicationUser>> GetSubscribedBlogs(ClaimsPrincipal principal)
        {
            var user = await Manager.FindByNameAsync(principal.Identity.Name);

            return user?.Blogs;
        }

        public async Task Save(Blog blog, ClaimsPrincipal principal)
        {
            var owner = await Manager.FindByNameAsync(principal.Identity.Name);

            var newBlog = new Blog
            {
                Owner = owner,
                UserName = owner.UserName,
                CreationDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Description = blog.Description,
                Name = blog.Name
            };

            await _db.Blogs.AddAsync(newBlog);
            var result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Error saving changes");
            }
        }

        public async Task Edit(Blog blog, ClaimsPrincipal principal)
        {
            var editedBlog = await (from b in _db.Blogs
                                    where b.BlogId == blog.BlogId
                                    select b).FirstOrDefaultAsync();

            _db.Blogs.Update(editedBlog);
            var result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Error editing blog");
            }
        }

        public async Task Subscribe(int blogId, ClaimsPrincipal principal)
        {
            var subscriber = await Manager.FindByNameAsync(principal.Identity.Name);

            var blog = await (from b in _db.Blogs
                              where b.BlogId == blogId
                              select b).FirstOrDefaultAsync();

            var subscription = new BlogApplicationUser
            {
                Blog = blog,
                User = subscriber
            };

            blog.Subscribers.Add(subscription);
            var result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Error creating subscription");
            }
        }
    }
}
