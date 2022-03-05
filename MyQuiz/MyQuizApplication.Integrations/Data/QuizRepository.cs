using MyQuizApplication.Infrastructure.Data.Common;

namespace MyQuizApplication.Infrastructure.Data
{
    public class QuizRepository : Repository, IQuizRepository
    {
        public QuizRepository(QuizContext context)
        {
            this.Context = context;
        }
    }
}
