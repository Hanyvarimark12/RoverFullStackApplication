using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Models
{
    public class VisitedPlaces
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlaceId { get; set; }
        [StringLength(60)]
        public string PlanetName { get; set; }
        [StringLength(60)]
        public string PlanetType { get; set;}
        [Required]
        public double Distance { get; set; }

        public virtual ICollection<RoverBuilder> RoverBuilders { get; set; }

        public VisitedPlaces(string line)
        {
            string[] visitedPlacesLine = line.Split('#');
            PlaceId = int.Parse(visitedPlacesLine[0]);
            PlanetName = visitedPlacesLine[1];
            PlanetType = visitedPlacesLine[2];
            Distance = double.Parse(visitedPlacesLine[3]);
        }

        //PlaceId#PlanetName#PlanetType#Distance
    }
}
