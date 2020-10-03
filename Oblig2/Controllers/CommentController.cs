using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblig2.Models.Repositories;
using Oblig2.Models.ViewModels;

namespace Oblig2.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repository;

        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int id)
        {
            return View(_repository.GetAll(id));
        }

        [HttpPost]
        [Authorize]
        //POST: Comment/Create
        public async Task<ActionResult> Create([Bind("Description")]
            CommentViewModel comment, int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                comment.PostId = id;
                await _repository.Save(comment, User);

                TempData["message"] = "Your comment has been created";

                return RedirectToAction("Index", "Comment", new {id});
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        // GET: Comment/Create
        public ActionResult Create()
        {
            var comment = _repository.GetCommentViewModel();
            return View(comment);
        }
    }
}
