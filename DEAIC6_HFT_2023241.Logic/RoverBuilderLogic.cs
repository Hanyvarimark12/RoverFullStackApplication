using System;
using DEAIC6_HFT_2023241.Repository;
using DEAIC6_HFT_2023241.Models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace DEAIC6_HFT_2023241.Logic
{
    public class RoverBuilderLogic : IRoverBuilderLogic
    {
        IRepository<RoverBuilder> builder_repo;
        //IRepository<Rover> rover_repo;
        //IRepository<VisitedPlaces> visitedplaces_repo;

        public RoverBuilderLogic(IRepository<RoverBuilder> builder_repo)
        {
            this.builder_repo = builder_repo;
            //IRepository<Rover> rover_repo
            //this.rover_repo = rover_repo;
            //this.visitedplaces_repo = visitedplaces_repo;
        }

        public void Create(RoverBuilder element)
        {
            if (element.BuilderId < 0 || element.BuilderName == null)
            {
                throw new ArgumentException("Cannot create RoverBuilder without name!/RoverBuilderId cannot be null!");
            }
            else
            {
                this.builder_repo.Create(element);
            }
        }

        public void Delete(int id)
        {
            this.builder_repo.Delete(id);
        }

        public RoverBuilder Read(int id)
        {
            var builder = this.builder_repo.Read(id);
            if (builder == null)
            {
                throw new ArgumentException("RoverBuilder does not exist!");
            }
            return builder;
        }

        public IQueryable<RoverBuilder> ReadAll()
        {
            return this.builder_repo.ReadAll();
        }

        public void Update(RoverBuilder Element)
        {
            this.builder_repo.Update(Element);
        }

        public IEnumerable<RoverBuilded> RoverBuild()
        {
            //builder order by distance
            return from x in this.builder_repo.ReadAll()
                                orderby x.VisitedPlaces.Distance descending
                                select new RoverBuilded()
                                {
                                    Id = x.BuilderId,
                                    Distance = x.VisitedPlaces.Distance
                                };
        }

        public MaxRoverNumber MoreVisitedPlaces()
        {
            //visitedplace with most builders
            var builderOnPlanet = from x in this.builder_repo.ReadAll()
                                  group x by x.VisitedPlaceId into g
                                  orderby g.Count() descending
                                  select new MaxRoverNumber()
                                  {
                                      Id = g.Key,
                                      BuilderNumber = g.Count(),
                                      Name = g.Select(t => t.VisitedPlaces.PlanetName).FirstOrDefault()
                                  };

            return builderOnPlanet.FirstOrDefault();
        }
    }

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

    public class MaxRoverNumber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BuilderNumber { get; set; }

        public override bool Equals(object obj)
        {
            MaxRoverNumber planet = obj as MaxRoverNumber;
            if (planet == null)
                return false;
            else
            {
                return this.Id == planet.Id
                    && this.BuilderNumber == planet.BuilderNumber
                    && this.Name == planet.Name;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.BuilderNumber, this.Name);
        }
    }
}
