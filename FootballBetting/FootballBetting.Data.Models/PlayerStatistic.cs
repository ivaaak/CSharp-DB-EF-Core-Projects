using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        //Fluent API composite PK
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public int ScoredGoals { get; set; }
        public int Assists { get; set; }
        public int MinutesPlayed { get; set; }
    }
}
