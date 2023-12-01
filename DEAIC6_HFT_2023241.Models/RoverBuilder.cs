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
    public class RoverBuilder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuilderId { get; set; }
        [StringLength(60)]
        public string BuilderName { get; set; }
        [Required]
        public int VisitedPlaceId { get; set; }
        [NotMapped]
        public virtual VisitedPlaces VisitedPlaces { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Rover> Rovers { get; set; }

        public RoverBuilder()
        {
            Rovers = new HashSet<Rover>();
        }

        public RoverBuilder(string line)
        {
            string[] builderLine = line.Split('#');
            BuilderId = int.Parse(builderLine[0]);
            BuilderName = builderLine[1];
            VisitedPlaceId = int.Parse(builderLine[2]);
            Rovers = new HashSet<Rover>();
        }

        //BuilderId#BuilderName

    }
}
