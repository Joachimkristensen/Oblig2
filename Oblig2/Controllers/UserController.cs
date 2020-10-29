using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;

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
            var blogs = new List<BlogApplicationUser>();

            var result = await _repository.GetSubscribedBlogs(User);

            if (result != null)
            {
                blogs = result as List<BlogApplicationUser>;
            }

            return View(blogs);
        }
        
    }
}
