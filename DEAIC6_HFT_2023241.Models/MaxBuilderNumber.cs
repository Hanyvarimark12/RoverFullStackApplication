using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class MaxBuilderNumber
    {
        public string Name { get; set; }
        public int BuilderNumber { get; set; }

        public override bool Equals(object obj)
        {
            MaxBuilderNumber planet = obj as MaxBuilderNumber;
            if (planet == null)
                return false;
            else
            {
                return this.BuilderNumber == planet.BuilderNumber
                    && this.Name == planet.Name;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.BuilderNumber, this.Name);
        }
    }
}
