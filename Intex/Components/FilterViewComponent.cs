using Intex.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Components
{
    public class FilterViewComponent : ViewComponent
    {
        private ICollisionsRepository repo { get; set; }

        public FilterViewComponent(ICollisionsRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Counties = repo.Collisions.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x);
            ViewBag.Cities = repo.Collisions.Select(x => x.CITY).Distinct().OrderBy(x => x);

            return View(new Collision());
        }
    }
}
