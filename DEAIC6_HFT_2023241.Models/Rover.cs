using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class Rover
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoverId { get; set; }
        [StringLength(60)]
        public string RoverName { get; set; }

        public DateTime BuildTime { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime LandDate { get; set; }

        public int VisitedPlaceId { get; set; }
        public int BuilderId { get; set; }

        public virtual VisitedPlaces VisitedPlaces { get; private set; }
        public virtual RoverBuilder RoverBuilders { get; private set; }


        public Rover(string line)
        {
            //RoverId#RoverName#LaunchDate#LandDate#VisitedPlaceId#BuilderId
            //1 Active 0 not Active -1 will be active
            string[] roverline = line.Split('#');
            RoverId = int.Parse(roverline[0]);
            RoverName = roverline[1];
            LaunchDate = DateTime.Parse(roverline[2]);
            LandDate = DateTime.Parse(roverline[3]);
            VisitedPlaceId = int.Parse(roverline[6]);
            BuilderId = int.Parse(roverline[7]);
        }
    }
}
