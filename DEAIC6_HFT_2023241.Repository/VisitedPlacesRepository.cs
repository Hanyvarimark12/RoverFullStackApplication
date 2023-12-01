using DEAIC6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Repository
{
    public class VisitedPlacesRepository : Repository<VisitedPlaces>, IRepository<VisitedPlaces>
    {
        public VisitedPlacesRepository(RoverDbContext ctx) : base(ctx)
        { }

        public override VisitedPlaces Read(int id)
        {
            return this.ctx.visitedPlaces.FirstOrDefault(t => t.PlaceId == id);
        }

        public override void Update(VisitedPlaces element)
        {
            var Old = Read(element.PlaceId);
            foreach (var prop in Old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(Old, prop.GetValue(element));
                }
            }
            ctx.SaveChanges();
        }
    }
}
