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

        //public IActionResult Index(/*int collisionType, */int pageNum = 1)
        //{
        //    int pageSize = 15;
        //    // collisionType = repo.Collisions

        //    //var x = new CollisionsViewModel
        //    //{
        //    //    Collisions = repo.Collisions
        //    //    .Where(c => c.CRASH_SEVERITY_ID == collisionType)
        //    //    .OrderBy(c => c.CITY)
        //    //    .Skip((pageNum - 1) * pageSize)
        //    //    .Take(pageSize),

        //    //    PageInfo = new PageInfo
        //    //    {
        //    //        TotalNumCollisions =
        //    //        ()
        //    //    }
        //    //};
        //    return View();
        //}

        public IActionResult Index()
        {
            var test = repo.Collisions.Where(x => x.CRASH_DATETIME == "2019-02-08T10:56:00.000").ToList();
            return View(test);
        }

        // Example shtuff

        //public IActionResult Index(string bookType, int pageNum = 1)
        //{
        //    int pageSize = 10;

        //    var x = new BooksViewModel
        //    {
        //        Books = repo.Books
        //        .Where(b => b.Category == bookType || bookType == null)
        //        .OrderBy(b => b.Title)
        //        .Skip((pageNum - 1) * pageSize)
        //        .Take(pageSize),

        //        PageInfo = new PageInfo
        //        {
        //            TotalNumBooks =
        //            (bookType == null
        //            ? repo.Books.Count()
        //            : repo.Books.Where(x => x.Category == bookType).Count()),
        //            BooksPerPage = pageSize,
        //            CurrentPage = pageNum
        //        }
        //    };
        //    return View(x);
        //}

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
