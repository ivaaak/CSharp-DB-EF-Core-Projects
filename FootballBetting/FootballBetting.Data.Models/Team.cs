using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBetting.Data.Models
{
    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();

            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
        }

        [Key]       
        public int TeamID { get; set; }  
        
        [Required] 
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(2048)]
        public string LogoUrl { get; set; }

        [Required]
        [MaxLength(4)]
        public string Initials { get; set; }

        public decimal Budget { get; set; }
        
        [ForeignKey(nameof(PrimaryKitColor))]
        public int PrimaryKitColorId { get; set; }
        public virtual Color PrimaryKitColor { get; set; }

        [ForeignKey(nameof(SecondaryKitColor))]
        public int SecondaryKitColorId { get; set; }
        public virtual Color SecondaryKitColor { get; set; }

        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        [InverseProperty("HomeTeam")]
        public virtual ICollection<Game> HomeGames { get; set; }
        
        [InverseProperty("AwayTeam")]
        public virtual ICollection<Game> AwayGames { get; set; }

        [ForeignKey(nameof(Players))]
        public ICollection<Player> Players { get; set; }
    }
}
