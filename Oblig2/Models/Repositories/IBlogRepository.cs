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

        public ViewModel GetBlogEditViewModel();

        public Task Save(ViewModel blog, ClaimsPrincipal principal);
    }
}
