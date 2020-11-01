
using Oblig2.Models.Entities;
using Oblig2.Models.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Oblig2.Models.Repositories
{
    public interface ICommentRepository
    {
        PostEditViewModel GetAll(int id);

        public Task<CommentViewModel> GetCommentViewModel(int id);

        public CommentViewModel GetCommentViewModel();

        public Task Save(Comment comment, ClaimsPrincipal principal);
    }
}
