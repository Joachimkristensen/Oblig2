using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BlogEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private UserManager<IdentityUser> Manager { get; }
        internal DbSet<TEntity> DbSet;

        public GenericRepository(UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
        {
            Manager = userManager;
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }

        public ViewModel GetBlogEditViewModel()
        {
            return new ViewModel();
        }


        public async Task Save(ViewModel blog, ClaimsPrincipal principal)
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

            await _dbContext.Blogs.AddAsync(newBlog);
            var result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Error saving changes");
            }
        }
    }
}
