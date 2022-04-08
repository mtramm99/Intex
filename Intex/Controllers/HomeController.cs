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
            //var cities = repo.Collisions.Select(x => x.CITY).Distinct().OrderBy(x => x);           

            //foreach (var c in cities)
            //{
            //    //List<Collision> crashList = repo.Collisions.Where(x => x.CITY == c).ToList();
            //    int crashNum = NumCrash(c);

            //    stats.Add(new Stat() { CityName = c, numCrash = crashNum });

            //}

            List<Stat> stats = new List<Stat>();

            decimal TotalCrashes = repo.Collisions.Select(x => x.CRASH_ID).Count();

            decimal teenCrash = repo.Collisions.Where(x => x.TEENAGE_DRIVER_INVOLVED == 1).Count();
            decimal teen = ((teenCrash / TotalCrashes));

            decimal nightCrash = repo.Collisions.Where(x => x.NIGHT_DARK_CONDITION == 1).Count();
            decimal night = ((nightCrash / TotalCrashes));

            decimal interCrash = repo.Collisions.Where(x => x.INTERSECTION_RELATED == 1).Count();
            decimal inter = ((interCrash / TotalCrashes));

            decimal distractCrash = repo.Collisions.Where(x => x.DISTRACTED_DRIVING == 1).Count();
            decimal distract = ((distractCrash / TotalCrashes));

            decimal drowsyCrash = repo.Collisions.Where(x => x.DROWSY_DRIVING == 1).Count();
            decimal drowsy = ((drowsyCrash / TotalCrashes));

            decimal duiCrash = repo.Collisions.Where(x => x.DUI == 1).Count();
            decimal dui = ((duiCrash / TotalCrashes));

            decimal commCrash = repo.Collisions.Where(x => x.COMMERCIAL_MOTOR_VEH_INVOLVED == 1).Count();
            decimal comm = ((commCrash / TotalCrashes));

            stats.Add(new Stat() { Factor = "Intersection Related", Perc = inter });
            stats.Add(new Stat() { Factor = "Night/Dark Condition", Perc = night });
            stats.Add(new Stat() { Factor = "Teenage Driver Involved", Perc = teen });
            stats.Add(new Stat() { Factor = "Distracted Driving", Perc = distract });
            stats.Add(new Stat() { Factor = "Commercial Vehicle Involved", Perc = comm });
            stats.Add(new Stat() { Factor = "DUI", Perc = dui });
            stats.Add(new Stat() { Factor = "Drowsy Driving", Perc = drowsy });

            decimal sevOne = repo.Collisions.Where(x => x.CRASH_SEVERITY_ID == 1).Count();
            decimal sevTwo = repo.Collisions.Where(x => x.CRASH_SEVERITY_ID == 2).Count();
            decimal sevThree = repo.Collisions.Where(x => x.CRASH_SEVERITY_ID == 3).Count();
            decimal sevFour = repo.Collisions.Where(x => x.CRASH_SEVERITY_ID == 4).Count();
            decimal sevFive = repo.Collisions.Where(x => x.CRASH_SEVERITY_ID == 5).Count();

            decimal onePerc = (sevOne / TotalCrashes);
            decimal twoPerc = (sevTwo / TotalCrashes);
            decimal threePerc = (sevThree / TotalCrashes);
            decimal fourPerc = (sevFour / TotalCrashes);
            decimal fivePerc = (sevFive / TotalCrashes);

            List<decimal> severities = new List<decimal>();

            severities.Add(onePerc);
            severities.Add(twoPerc);
            severities.Add(threePerc);
            severities.Add(fourPerc);
            severities.Add(fivePerc);

            ViewBag.Severities = severities;


            return View(stats);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //public int NumCrash(string c)
        //{
        //    int numCrash = repo.Collisions.Where(x => x.CITY == c).Count();

        //    return (numCrash);
        //}

    }
}
