using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class Rover
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoverId { get; set; }
        [StringLength(60)]
        [Required]
        public string RoverName { get; set; }
        //public DateTime BuildTime { get; set; }
        [Required]
        public DateTime LaunchDate { get; set; }
        [Required]
        public DateTime LandDate { get; set; }
        [Required]
        public int VisitedPlaceId { get; set; }
        [Required]
        public int BuilderId { get; set; }
        [NotMapped]
        public virtual RoverBuilder RoverBuilder { get; set; }

        //public virtual VisitedPlaces VisitedPlaces { get; private set; }
        //public virtual RoverBuilder RoverBuilders { get; private set; }

        public Rover()
        {
            
        }

        public Rover(string line)
        {
            //RoverId#RoverName#LaunchDate#LandDate#BuilderId
            //1 Active 0 not Active -1 will be active
            string[] roverline = line.Split('#');
            RoverId = int.Parse(roverline[0]);
            RoverName = roverline[1];
            LaunchDate = DateTime.Parse(roverline[2]);
            LandDate = DateTime.Parse(roverline[3]);
            BuilderId = int.Parse(roverline[4]);
        }
    }
}
