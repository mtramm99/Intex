using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    public interface ICollisionsRepository
    {
        IQueryable<Collision> Collisions { get;  }

        public void SaveCollision(Collision c);
        public void CreateCollision(Collision c);
        public void DeleteCollision(Collision c);
    }
}
