using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    public class CollisionsDbContext : DbContext
    {
        public CollisionsDbContext(DbContextOptions<CollisionsDbContext> options) : base(options)
        {

        }

        public DbSet<Collision> Collisions { get; set; }
    }
}
