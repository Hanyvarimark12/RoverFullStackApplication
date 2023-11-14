using System;
using DEAIC6_HFT_2023241.Repository;
using DEAIC6_HFT_2023241.Models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace DEAIC6_HFT_2023241.Logic
{
    public class RoverBuilderLogic 
    {
        IRepository<RoverBuilder> builder_repo;
        IRepository<Rover> rover_repo;

        public RoverBuilderLogic(IRepository<RoverBuilder> builder_repo, IRepository<Rover> rover_repo)
        {
            this.builder_repo = builder_repo;
            this.rover_repo = rover_repo;
        }

        public void Create(RoverBuilder element)
        {
            if(element.BuilderId < 0 ||  element.BuilderName == null) 
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
            if(builder == null)
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

        public IEnumerable<BuilderTime> AvgTimeTravel(int BuilderId)
        {
            var builderTime = rover_repo.ReadAll().Where(t => t.BuilderId == BuilderId);
            var timetraveled = (IEnumerable<BuilderTime>)from builder in builder_repo.ReadAll()
                               where builder.BuilderId == BuilderId
                               group builder by builder.BuilderId into roverTimeTraveled
                               select new BuilderTime
                               {
                                   Id = roverTimeTraveled.Key,
                                   AvgTraveled = rover_repo.ReadAll().Where(t => t.BuilderId == BuilderId)
                                   .Average(t => t.LandDate.Hour - t.LaunchDate.Hour)
                               };

            return timetraveled;
        }

        public IEnumerable<Builder> MoreVisitedPlaces()
        {
            //roverbuilder with more than one visited places
            return (IEnumerable<Builder>)from builder in builder_repo.ReadAll()
                   where builder.VisitedPlaces.Count() > 1
                   group builder by builder.BuilderId into rover_builder
                   select new Builder()
                   {
                       Id = rover_builder.Key,
                       VisitedPlaceNumber = rover_builder.Count()
                   };
                
        } 
    }

    public class Builder
    {
        public int Id { get; set; }
        public int VisitedPlaceNumber { get; set; }
    }

    public class BuilderTime
    {
        public int Id { get; set; }
        public double AvgTraveled { get; set; }
    }
}
