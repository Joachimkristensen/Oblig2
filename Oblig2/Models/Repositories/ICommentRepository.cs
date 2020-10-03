using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Oblig2.Models.ViewModels;

namespace Oblig2.Models.Repositories
{
    public interface ICommentRepository
    {
        PostEditViewModel GetAll(int id);

        public Task<CommentViewModel> GetCommentViewModel(int id);

        public CommentViewModel GetCommentViewModel();

        public Task Save(CommentViewModel viewModel, ClaimsPrincipal principal);
    }
}
