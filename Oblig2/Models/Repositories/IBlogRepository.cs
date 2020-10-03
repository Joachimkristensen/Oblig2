using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Oblig2.Models;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models.Repositories
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetAll();

        public Task<BlogEditViewModel> GetWithId(int id);

        public BlogEditViewModel GetBlogEditViewModel();

        public Task Save(BlogEditViewModel blog, ClaimsPrincipal principal);
    }
}
