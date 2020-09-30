using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Oblig2.Models;
using Oblig2.Models.Entities;

namespace Oblig2.Models.Repositories
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetAll();

        public BlogEditViewModel GetBlogEditViewModel();

        public Task Save(BlogEditViewModel blog, ClaimsPrincipal principal);
    }
}
