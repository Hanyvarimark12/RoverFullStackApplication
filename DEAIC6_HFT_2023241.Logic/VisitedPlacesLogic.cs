using DEAIC6_HFT_2023241.Models;
using DEAIC6_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Logic
{
    public class VisitedPlacesLogic : IVisitedPlacesLogic
    {
        IRepository<VisitedPlaces> visitedplaces_repo;

        public VisitedPlacesLogic(IRepository<VisitedPlaces> visitedplaces_repo)
        {
            this.visitedplaces_repo = visitedplaces_repo;
        }
        public void Create(VisitedPlaces element)
        {
            if (element.PlaceId == null || element.Distance < 0)
            {
                throw new ArgumentException("PlaceId cannot be null!/Distance cannot be negative!");
            }
            else
            {
                this.visitedplaces_repo.Create(element);
            }
        }

        public void Delete(int id)
        {
            this.visitedplaces_repo.Delete(id);
        }

        public VisitedPlaces Read(int id)
        {
            var visitedPlace = this.visitedplaces_repo.Read(id);
            if (visitedPlace == null)
            {
                throw new ArgumentException("Visited Place cannot be found!");
            }
            return visitedPlace;
        }

        public IQueryable<VisitedPlaces> ReadAll()
        {
            return this.visitedplaces_repo.ReadAll();
        }

        public void Update(VisitedPlaces Element)
        {
            this.visitedplaces_repo.Update(Element);
        }

        public IEnumerable<RoverTraveled> VisitedByLaunchDate()
        {
            //avg time to planet
            var builder = from x in visitedplaces_repo.ReadAll()
                          orderby x.RoverBuilders.Select(t => t.Rovers.Min(t => t.LaunchDate)).First()
                          select new RoverTraveled()
                          {
                              VisitedPlaceId = x.PlaceId,
                              Launching = x.RoverBuilders.Select(t => t.Rovers.Min(t => t.LaunchDate)).FirstOrDefault(),
                              Name = x.PlanetName
                          };

                          

            return builder;
        }

        public RoverNumberByPlanet MostRoverNumber()
        {
            var builder = from x in this.visitedplaces_repo.ReadAll()
                          orderby x.RoverBuilders.Count() descending
                          select new RoverNumberByPlanet()
                          {
                              Id = x.PlaceId,
                              RoverNumber = x.RoverBuilders.Count()
                          };
            return builder.FirstOrDefault();
        }
    }

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
