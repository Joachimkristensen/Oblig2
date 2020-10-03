using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2.Authorization;
using Oblig2.Models.Repositories;
using Oblig2.Models.ViewModels;

namespace Oblig2.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _repository;
        private IAuthorizationService _authorizationService;

        public PostController(IPostRepository repository, IAuthorizationService authorizationService = null)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }

        public async Task <IActionResult> Index(int id)
        {
            return View(await _repository.GetAll(id));
        }

        
        [HttpPost]
        [Authorize]
        // Post: Post/Create
        public async Task<ActionResult> Create([Bind("Name, Description")] PostEditViewModel post, int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                post.BlogId = id;
                await _repository.Save(post, User);

                TempData["message"] = $"{post.Name} has been created";

                return RedirectToAction("Index", "Post", new {id});
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        // GET: Post/Create
        public ActionResult Create()
        {
            var post = _repository.GetPostEditViewModel();
            return View(post);
        }

        public async Task<ActionResult> Details(int id)
        {
            var post = await _repository.GetPostEditViewModel(id);
            return View(post);
        }

        // GET: Post/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var post = _repository.GetPost(id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, post, PostOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            else
            {
                return View(post);
            }
        }
    }
}
