using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Oblig2.Models;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models.Repositories
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetAll();

        public Task<Blog> GetBlogWithId(int id);

        public Task<List<BlogApplicationUser>> GetSubscribedBlogs(ClaimsPrincipal principal);

        public Task Save(Blog blog, ClaimsPrincipal principal);

        public Task Subscribe(int id, ClaimsPrincipal principal);
    }
}
