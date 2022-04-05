using Intex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {
        private ICollisionsRepository repo { get; set; }

        public HomeController(ICollisionsRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            var test = repo.Collisions.Where(x => x.CRASH_DATETIME == "2019-02-08T10:56:00.000").ToList();
            return View(test);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
