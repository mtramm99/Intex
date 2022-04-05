using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumCollisions { get; set; }
        public int CollisionsPerPage { get; set; }
        public int CurrentPage { get; set; }

        // Figure out num pages needed
        public int TotalPages => (int)Math.Ceiling((double)TotalNumCollisions / CollisionsPerPage);
    }
}