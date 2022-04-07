using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    public class EFCollisionsRepository : ICollisionsRepository 
    {
        private CollisionsDbContext context { get; set; }
        
        public EFCollisionsRepository (CollisionsDbContext temp)
        {
            context = temp;
        }

        public IQueryable<Collision> Collisions => context.Collisions;

        public void SaveCollision(Collision c)
        {
            context.SaveChanges();
        }

        public void CreateCollision(Collision c)
        {
            context.Add(c);
            context.SaveChanges();
        }

        public void DeleteCollision(Collision c)
        {
            context.Remove(c);
            context.SaveChanges();
        }
    }
}
