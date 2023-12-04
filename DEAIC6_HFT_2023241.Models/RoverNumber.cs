using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class RoverNumber
    {
        public int BuilderId { get; set; }
        public int Number { get; set; }

        public override bool Equals(object obj)
        {
            RoverNumber distance = obj as RoverNumber;
            if (distance == null)
                return false;
            else
            {
                return this.BuilderId == distance.BuilderId
                    && this.Number == distance.Number;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.BuilderId, this.Number);
        }
    }
}
