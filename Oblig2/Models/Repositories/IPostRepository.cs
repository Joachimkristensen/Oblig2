using System.Security.Claims;
using System.Threading.Tasks;
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models.Repositories
{
    public interface IPostRepository
    {
        public Task<BlogEditViewModel> GetAll(int id);

        public Task<PostEditViewModel> GetPostEditViewModel(int id);

        public PostEditViewModel GetPostEditViewModel();

        public Task Save(PostEditViewModel viewModel, ClaimsPrincipal principal);

        public Task<Post> GetPost(int id);
    }
}
