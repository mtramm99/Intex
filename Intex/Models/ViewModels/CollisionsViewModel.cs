using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models.ViewModels
{
    public class CollisionsViewModel
    {
        public IQueryable<Collision> Collisions { get; set; }
        public PageInfo PageInfo { get; set; }
        
    }
}
