using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyQuizApplication.Infrastructure.Data.Models
{
    public class Quiz
    {
        public Quiz()
        {
            this.Questions = new HashSet<Question>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
