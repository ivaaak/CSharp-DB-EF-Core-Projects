using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyQuizApplication.Core.Models
{
    public class QuizModel
    {
        public QuizModel()
        {
            this.Questions = new HashSet<QuestionModel>();
        }

        [UIHint("hidden")]
        public int Id { get; set; }

        [Display(Name = "Име на Въпросника")]
        [Required(ErrorMessage = "Полето {0} е задължително")]
        [StringLength(150, ErrorMessage = "Полето {0} не може да е по-дълго от {1} символи")]
        [RegularExpression("[A-Z,А-Я].+", ErrorMessage = "Името трябва да започва с главна буква")]
        public string Title { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }
    }
}
