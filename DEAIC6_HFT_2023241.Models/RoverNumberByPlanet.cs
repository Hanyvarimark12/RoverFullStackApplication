using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class RoverNumberByPlanet
    {
        public int Id { get; set; }
        public int RoverNumber { get; set; }

        public override bool Equals(object obj)
        {
            RoverNumberByPlanet planet = obj as RoverNumberByPlanet;
            if (planet == null)
                return false;
            else
            {
                return this.Id == planet.Id
                    && this.RoverNumber == planet.RoverNumber;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.RoverNumber);
        }
    }
}
