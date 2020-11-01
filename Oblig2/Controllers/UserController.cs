using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2.Models.Repositories;
using System.Threading.Tasks;

namespace Oblig2.Controllers
{
    public class UserController : Controller
    {
        private readonly IBlogRepository _repository;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IBlogRepository repository, IAuthorizationService authorizationService = null)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _repository.GetSubscribedBlogs(User);

            return View(blogs);
        }

    }
}
