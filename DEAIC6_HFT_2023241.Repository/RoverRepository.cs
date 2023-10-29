using DEAIC6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Repository
{
    public class RoverRepository : Repository<Rover>, IRepository<Rover>
    {
        public RoverRepository(RoverDbContext ctx) : base(ctx)
        { }

        public override Rover Read(int id)
        {
            return this.ctx.Rovers.FirstOrDefault(t => t.RoverId == id);
        }

        public override void Update(Rover element)
        {
            var old = Read(element.RoverId);
            foreach(var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(element));
            }
            ctx.SaveChanges();
        }
    }
}
