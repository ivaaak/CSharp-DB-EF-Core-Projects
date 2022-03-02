using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FootballBetting.Data.Models
{
    public class Bet
    {
        [Key]
        public int BetId { get; set; }
        public decimal Amount { get; set; }
        public Prediction Prediction { get; set; }
        public DateTime DateTime { get; set; }

        //nav prop
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }


        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

    }
}
