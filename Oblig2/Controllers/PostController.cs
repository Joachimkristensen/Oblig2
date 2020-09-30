using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oblig2.Controllers
{
    public class PostController : Controller
    {
        //private readonly -i

        public IActionResult Index()
        {
            return View();
        }
    }
}
