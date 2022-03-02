using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)] 
        public string Password { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
