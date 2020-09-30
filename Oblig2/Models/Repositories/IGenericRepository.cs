using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BlogEntity
    {
        IEnumerable<TEntity> GetAll();
        ViewModel GetBlogEditViewModel();
        Task Save(ViewModel blog, ClaimsPrincipal principal);
    }
}