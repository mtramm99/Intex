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

        public IActionResult CollisionSummary(int pageNum = 1, string city = null, string county = null, float severity = 0, DateTime? date = null)
        {
            int pageSize = 50;


            if (city == null && county == null && severity == 0 && date == null)
            {
                var x = new CollisionsViewModel
                {
                    Collisions = repo.Collisions
                    .OrderBy(c => c.CITY)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                    PageInfo = new PageInfo
                    {
                        TotalNumCollisions =
                        (city == null
                        ? repo.Collisions.Count()
                        : repo.Collisions.Count()),
                        CollisionsPerPage = pageSize,
                        City = null,
                        CurrentPage = pageNum
                    }
                };

                //if (col.CITY != null)
                //{
                //    x.Collisions = x.Collisions.Where(x => x.CITY == col.CITY);
                //}

                return View(x);
            }
            else
            {
                var x = new CollisionsViewModel
                {
                    Collisions = repo.Collisions
                        .Where(x => date != new DateTime(1,1,1) ? x.CRASH_DATETIME.Date == date : true)
                        .Where(x => city != null ? x.CITY == city : true)
                        .Where(x => county != null ? x.COUNTY_NAME == county : true)
                        .Where(x => severity != 0 ? x.CRASH_SEVERITY_ID == severity : true)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize),

                    PageInfo = new PageInfo
                    {
                        TotalNumCollisions =
                        repo.Collisions
                        .Where(x => date != new DateTime(1,1,1) ? x.CRASH_DATETIME.Date == date : true)
                        .Where(x => city != null ? x.CITY == city : true)
                        .Where(x => county != null ? x.COUNTY_NAME == county : true)
                        .Where(x => severity != 0 ? x.CRASH_SEVERITY_ID == severity : true)
                        .Count(),
                        CollisionsPerPage = pageSize,
                        City = city,
                        Severity = severity,
                        County = county,
                        Date = date,
                        CurrentPage = pageNum
                    }
                };

                ViewBag.Filter = x;

                return View(x);
            }
            

            
        }

        [HttpPost]
        public IActionResult Filter(int pageNum = 1, Collision col = null)
        {
            int pageSize = 50;

            var x = new CollisionsViewModel
            {
                Collisions = repo.Collisions
                    .Where(x => col.CRASH_DATETIME.Date != new DateTime(1,1,1) ? x.CRASH_DATETIME.Date == col.CRASH_DATETIME.Date : true)
                    .Where(x => col.CITY != null ? x.CITY == col.CITY : true)
                    .Where(x => col.COUNTY_NAME != null ? x.COUNTY_NAME == col.COUNTY_NAME : true)
                    .Where(x => col.CRASH_SEVERITY_ID != 0 ? x.CRASH_SEVERITY_ID == col.CRASH_SEVERITY_ID : true)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCollisions =
                    (repo.Collisions
                        .Where(x => col.CRASH_DATETIME.Date != new DateTime(1,1,1) ? x.CRASH_DATETIME.Date == col.CRASH_DATETIME.Date : true)
                        .Where(x => col.CITY != null ? x.CITY == col.CITY : true)
                        .Where(x => col.COUNTY_NAME != null ? x.COUNTY_NAME == col.COUNTY_NAME : true)
                        .Where(x => col.CRASH_SEVERITY_ID != 0 ? x.CRASH_SEVERITY_ID == col.CRASH_SEVERITY_ID : true)
                        .Count()),
                    CollisionsPerPage = pageSize,
                    City = col.CITY,
                    County = col.COUNTY_NAME,
                    Severity = col.CRASH_SEVERITY_ID,
                    Date = col.CRASH_DATETIME.Date,
                    CurrentPage = pageNum
                }
            };

            ViewBag.Filter = x;

            return View("CollisionSummary", x);            
        }

        public IActionResult Details (int id)
        {
            var x = repo.Collisions.Single(x => x.CRASH_ID == id);

            return View(x);
        }

        public IActionResult CityAnalytics ()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
