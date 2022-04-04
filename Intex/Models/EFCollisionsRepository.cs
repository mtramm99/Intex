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
            throw new NotImplementedException();
        }

        public void CreateCollision(Collision c)
        {
            throw new NotImplementedException();
        }

        public void DeleteCollision(Collision c)
        {
            throw new NotImplementedException();
        }
    }
}
