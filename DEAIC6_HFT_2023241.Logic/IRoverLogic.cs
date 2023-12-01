using DEAIC6_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace DEAIC6_HFT_2023241.Logic
{
    public interface IRoverLogic
    {
        void Create(Rover element);
        void Delete(int id);
        IEnumerable<RoverNumber> BuilderRovers();
        Rover Read(int id);
        IQueryable<Rover> ReadAll();
        void Update(Rover Element);
    }
}