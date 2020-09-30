using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using Oblig2.Models.ViewModels;

namespace Oblig2.Controllers
{
    public class BlogController : Controller
    {
        private readonly GenericRepository<Blog> _repository;

        public BlogController(IGenericRepository<Blog> repository)
        {
            _repository = (GenericRepository<Blog>)repository;
        }

        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        [HttpPost]
        [Authorize]
        //POST: Blog/Create
        public async Task<ActionResult> Create([Bind("Name, Description")]
            ViewModel blog)
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
