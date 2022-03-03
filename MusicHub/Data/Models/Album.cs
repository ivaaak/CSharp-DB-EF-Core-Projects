using MusicHub.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Album
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ALBUM_NAME_MAX_LENGTH)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }

        [ForeignKey(nameof(Producer))]
        public int? ProducerId { get; set; }   
        public virtual Producer Producer { get; set; }

        public decimal Price => this.Songs.Sum(s => s.Price);

        public virtual ICollection<Song> Songs { get; set; }
    }
}
