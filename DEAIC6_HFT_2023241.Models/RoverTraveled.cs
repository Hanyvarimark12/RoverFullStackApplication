using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class RoverTraveled
    {
        public int VisitedPlaceId { get; set; }

        public DateTime Launching { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            RoverTraveled planet = obj as RoverTraveled;
            if (planet == null)
                return false;
            else
            {
                return this.VisitedPlaceId == planet.VisitedPlaceId
                    && this.Launching == planet.Launching
                    && this.Name == planet.Name;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.VisitedPlaceId, this.Launching, this.Name);
        }
    }
}
