using DEAIC6_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace DEAIC6_HFT_2023241.Logic
{
    public interface IRoverBuilderLogic
    {
        IEnumerable<RoverBuilded> RoverBuild();
        void Create(RoverBuilder element);
        void Delete(int id);
        MaxRoverNumber MoreVisitedPlaces();
        RoverBuilder Read(int id);
        IQueryable<RoverBuilder> ReadAll();
        void Update(RoverBuilder Element);
    }
}