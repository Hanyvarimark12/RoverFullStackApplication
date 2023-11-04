using DEAIC6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Repository
{
    internal class RoverBuilderRepository : Repository<RoverBuilder>, IRepository<RoverBuilder>
    {
        public RoverBuilderRepository(RoverDbContext ctx) : base(ctx)
        { }

        public override RoverBuilder Read(int id)
        {
            return this.ctx.roverBuilders.FirstOrDefault(t => t.BuilderId == id);
        }

        public override void Update(RoverBuilder element)
        {
            var Old = Read(element.BuilderId);
            foreach (var prop in Old.GetType().GetProperties())
            {
                prop.SetValue(Old, prop.GetValue(element));
            }
            ctx.SaveChanges();
        }
    }
}
