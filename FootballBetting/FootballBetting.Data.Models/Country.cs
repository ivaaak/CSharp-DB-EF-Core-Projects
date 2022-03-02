using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
        }
	  
        [Key]
        public int CountryId { get; set; }
	  
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
