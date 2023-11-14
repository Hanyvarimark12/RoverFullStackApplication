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
    internal class VisitedPlacesLogic
    {
        IRepository<VisitedPlaces> visitedplaces_repo;
        IRepository<Rover> rover_repo;
        IRepository<RoverBuilder> builder_repo;

        public VisitedPlacesLogic(IRepository<VisitedPlaces> visitedplaces_repo, IRepository<Rover> rover_repo, IRepository<RoverBuilder> builder_repo)
        {
            this.visitedplaces_repo = visitedplaces_repo;
            this.rover_repo = rover_repo;
            this.builder_repo = builder_repo;
        }
        public void Create(VisitedPlaces element)
        {
            if(element.PlaceId == null || element.Distance < 0)
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
            if(visitedPlace == null)
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

        public IEnumerable<RoverTraveled> VisitedByBuilder()
        {
            //visitedplace how many time by builder
            return (IEnumerable<RoverTraveled>)from visitedplace in this.visitedplaces_repo.ReadAll()
                              group visitedplace by visitedplace.PlaceId into builder
                              select new RoverTraveled()
                              {
                                  VisitedPlaceId = builder.Key,
                                  Builders = visitedplaces_repo.ReadAll().Select(t => t.RoverBuilders).Count()
                              };
        }
    }

    public class RoverTraveled
    {
        public int VisitedPlaceId { get; set; }
        public int Builders { get; set; }
    }
}
