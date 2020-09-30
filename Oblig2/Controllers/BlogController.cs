using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2.Models;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;

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

        [HttpPost]
        [Authorize]
        //POST: Blog/Create
        public async Task<ActionResult> Create([Bind("Name, Description")]
            BlogEditViewModel blog)
        {
            if (ModelState.IsValid)
            {
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
            else
            {
                return View();
            }

        }

        [Authorize]
        // GET: Blog/Create
        public ActionResult Create()
        {
            var blog = _repository.GetBlogEditViewModel();
            return View(blog);
        }
    }
}
