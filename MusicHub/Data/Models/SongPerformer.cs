using MusicHub.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicHub.Data.Models
{
    public class SongPerformer
    {
        // Mapping Table - Many to many
        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }
        public virtual Song Song { get; set; }

        [ForeignKey(nameof(Producer))]
        public int PerformerId { get; set; }
        public Producer Producer { get; set; }
    }
}
