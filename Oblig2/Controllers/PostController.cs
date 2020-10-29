using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2.Authorization;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;

namespace Oblig2.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _repository;
        private readonly IAuthorizationService _authorizationService;

        public PostController(IPostRepository repository, IAuthorizationService authorizationService = null)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }

        public async Task <IActionResult> Index(int id)
        {
            return View(await _repository.GetAll(id));
        }


        [HttpGet]
        [Authorize]
        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        // Post: Post/Create
        public async Task<ActionResult> Create([Bind("Name, Description")] Post post, int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                post.BlogId = id;
                await _repository.Save(post, User);

                TempData["message"] = $"Post: '{post.Name}' has been created";

                return RedirectToAction("Index", "Post", new {id});
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var post = await _repository.GetPostEditViewModel(id);
            return View(post);
        }

        [Authorize]
        // GET: Post/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var task = _repository.GetPost(id);
            var post = task.Result;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, post, EntityOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        // POST: Post/Edit
        public async Task<IActionResult> Edit([Bind("Name, Description")] Post newPost, int postId)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                var post = _repository.GetPost(postId).Result;

                post.Name = newPost.Name;
                post.Description = newPost.Description;

                var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                            User, post, EntityOperations.Update);

                if (!isAuthorized.Succeeded)
                {
                    return new ChallengeResult();
                }

                await _repository.Edit(post);

                TempData["message"] = $"Post: '{post.Name}' has been edited";

                var id = post.BlogId;

                return RedirectToAction("Index", "Post", new {id});
            }
            catch
            {
                return View();
            }
        }


        // GET: Post/Delete
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var post = _repository.GetPost(id).Result;

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, post, EntityOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            return View(post);
        }

        // POST: Post/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] 
        public async Task<IActionResult> Delete(Post post, int postId)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                post = _repository.GetPost(postId).Result;

                var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                            User, post, EntityOperations.Delete);
                if (!isAuthorized.Succeeded)
                {
                    return new ChallengeResult();
                }

                await _repository.Delete(post);

                TempData["message"] = $"Post: '{post.Name}' has been deleted";

                var id = post.BlogId;

                return RedirectToAction("Index", "Post", new { id });
            }
            catch
            {
                return View();
            }
        }

    }
}
