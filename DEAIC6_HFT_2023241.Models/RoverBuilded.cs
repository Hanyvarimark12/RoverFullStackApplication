using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class RoverBuilded
    {
        public int Id { get; set; }
        public double Distance { get; set; }

        public override bool Equals(object obj)
        {
            RoverBuilded builded = obj as RoverBuilded;
            if (builded == null)
                return false;
            else
            {
                return this.Id == builded.Id
                    && this.Distance == builded.Distance;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Distance);
        }
    }
}
