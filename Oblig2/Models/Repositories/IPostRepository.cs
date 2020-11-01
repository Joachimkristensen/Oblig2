using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Oblig2.Models.Repositories
{
    public interface IPostRepository
    {
        public Task<Blog> GetAll(int id);

        public Task<PostEditViewModel> GetPostEditViewModel(int id);

        public Task Save(Post post, ClaimsPrincipal principal);

        public Task<Post> GetPost(int id);

        public Task Edit(Post post);

        public Task Delete(Post post);
    }
}
