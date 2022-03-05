using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyQuizApplication.Core.Models
{
    public class QuestionModel
    {
        [UIHint("hidden")]
        public int Id { get; set; }

        [Display(Name = "Въпрос")]
        [Required(ErrorMessage = "Полето {0} е задължително")]
        [StringLength(200, ErrorMessage = "Полето {0} не може да е по-дълго от {1} символи")]
        public string Title { get; set; }

        [Display(Name = "Въпросник")]
        public int QuizId { get; set; }

    }
}
