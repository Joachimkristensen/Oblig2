﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;

namespace Oblig2.Models.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        private UserManager<IdentityUser> Manager { get; }

        public BlogRepository(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            Manager = userManager;
            _db = db;
        }

        public IEnumerable<Blog> GetAll()
        {
            return _db.Blogs;
        }

        public BlogEditViewModel GetBlogEditViewModel()
        {
            return new BlogEditViewModel();
        }


        public async Task Save(BlogEditViewModel blog, ClaimsPrincipal principal)
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
    }
}