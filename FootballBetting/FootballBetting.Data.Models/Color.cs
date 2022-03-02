using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FootballBetting.Data.Models
{
    public class Color
    {
        //initialize collections 
        public Color()
        {
            this.PrimaryKitTeams = new HashSet<Team>();
            this.SecondaryKitTeams = new HashSet<Team>();
        }

        [Key]
        public int ColorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //2 relations with the same direction -> use InverseProperty
        [InverseProperty("PrimaryKitColor")]
        public virtual ICollection<Team> PrimaryKitTeams { get; set; }
        
        [InverseProperty("SecondaryKitColor")]
        public virtual ICollection<Team> SecondaryKitTeams { get; set; }
    }
}
