using DEAIC6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Repository
{
    internal class VisitedPlacesRepository : Repository<VisitedPlaces>, IRepository<VisitedPlaces>
    {
        public VisitedPlacesRepository(RoverDbContext ctx) : base(ctx)
        { }

        public override VisitedPlaces Read(int id)
        {
            return this.ctx.visitedPlaces.FirstOrDefault(t => t.PlaceId == id);
        }

        public override void Update(VisitedPlaces element)
        {
            var old = Read(element.PlaceId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(element));
            }
            ctx.SaveChanges();
        }
    }
}
