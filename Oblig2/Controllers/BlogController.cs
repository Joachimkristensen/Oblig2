using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using Oblig2.Models.ViewModels;

namespace Oblig2.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _repository;

        public BlogController(IBlogRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        [Authorize]
        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        //POST: Blog/Create
        public async Task<ActionResult> Create([Bind("Name, Description")]
            Blog blog)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _repository.Save(blog, User);

                TempData["message"] = $"{blog.Name} has been created";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        // Get: Blog/Details
        public async Task<ActionResult> Details(int id)
        {
            return View(await _repository.GetBlogWithId(id));
        }

        public async Task<RedirectToActionResult> Subscribe(int id)
        {
            await _repository.Subscribe(id, User);

            var blog = await _repository.GetBlogWithId(id);

            TempData["message"] = $"Subscribed to: {blog.Name}!";

            return RedirectToAction("Details", new {id});
        }
    }
}
