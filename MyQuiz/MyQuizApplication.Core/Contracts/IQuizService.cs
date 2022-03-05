using MyQuizApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizApplication.Core.Contracts
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizModel>> GetQuizzes();

        Task<bool> CreateQuiz(QuizModel model);

        Task<QuizModel> GetById(int id);

        Task<bool> Edit(QuizModel model);

        Task Delete(int id);
    }
}
