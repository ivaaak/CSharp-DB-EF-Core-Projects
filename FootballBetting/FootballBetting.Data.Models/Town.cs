using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FootballBetting.Data.Models
{
    public class Town
    {
        public Town()
        {
            this.Teams = new HashSet<Team>();
        }

        [Key]
        public int TownId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public int CountryId { get; set; }
        [ForeignKey(nameof(Country))]
        public virtual Country Country { get; set; }


        public ICollection<Team> Teams { get; set; }
    }
}
