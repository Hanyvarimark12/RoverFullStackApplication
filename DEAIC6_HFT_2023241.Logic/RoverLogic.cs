using DEAIC6_HFT_2023241.Models;
using DEAIC6_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DEAIC6_HFT_2023241.Logic
{
    public class RoverLogic : IRoverLogic
    {
        IRepository<Rover> rover_repo;
        IRepository<RoverBuilder> builder_repo;

        public RoverLogic(IRepository<Rover> rover_repo)
        {
            this.rover_repo = rover_repo;
        }
        public void Create(Rover element)
        {
            if (element.RoverId < 0)
            {
                throw new ArgumentException("RoverId cannot be null!");
            }
            else
            {
                this.rover_repo.Create(element);
            }
        }

        public void Delete(int id)
        {
            this.rover_repo.Delete(id);
        }

        public Rover Read(int id)
        {
            var rover = this.rover_repo.Read(id);
            if (rover == null)
            {
                throw new ArgumentException("RoverBuilder does not exist!");
            }
            return rover;
        }

        public IQueryable<Rover> ReadAll()
        {
            return this.rover_repo.ReadAll();
        }

        public void Update(Rover Element)
        {
            this.rover_repo.Update(Element);
        }

        public IEnumerable<RoverNumber> BuilderRovers()
        {
            return from x in rover_repo.ReadAll()
                               group x by x.BuilderId into g
                               select new RoverNumber()
                               {
                                   BuilderId = g.Key, 
                                   Number = g.Count()
                               };
        }
    }

    public class RoverNumber
    {
        public int BuilderId { get; set; }
        //public string RoverName { get; set; }
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
