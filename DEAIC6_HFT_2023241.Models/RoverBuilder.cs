using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
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

        public virtual ICollection<Rover> Rovers { get; set; }

    }
}
