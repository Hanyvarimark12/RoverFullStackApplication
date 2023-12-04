using DEAIC6_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace DEAIC6_HFT_2023241.Logic
{
    public interface IVisitedPlacesLogic
    {
        void Create(VisitedPlaces element);
        void Delete(int id);
        RoverNumberByPlanet MostRoverNumber();
        VisitedPlaces Read(int id);
        IQueryable<VisitedPlaces> ReadAll();
        void Update(VisitedPlaces Element);
        IEnumerable<RoverTraveled> VisitedByLaunchDate();
    }
}