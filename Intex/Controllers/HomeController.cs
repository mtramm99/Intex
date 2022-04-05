using Intex.Models;
using Intex.Models.ViewModels;
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
        private ICollisionsRepository repo;
        public HomeController( ICollisionsRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CollisionSummary(int collisionType, int pageNum = 1)
        {
            int pageSize = 50;            

            var x = new CollisionsViewModel
            {
                Collisions = repo.Collisions
                .Where(c => c.CRASH_SEVERITY_ID == collisionType || collisionType == 0)
                .OrderBy(c => c.CITY)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCollisions =
                    (collisionType == 0
                    ? repo.Collisions.Count()
                    : repo.Collisions.Where(x => x.CRASH_SEVERITY_ID == collisionType).Count()),
                    CollisionsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            return View(x);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
