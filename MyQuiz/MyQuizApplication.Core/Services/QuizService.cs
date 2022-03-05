using Microsoft.EntityFrameworkCore;
using MyQuizApplication.Core.Contracts;
using MyQuizApplication.Core.Models;
using MyQuizApplication.Infrastructure.Data;
using MyQuizApplication.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizApplication.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository repo;

        public QuizService(IQuizRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> CreateQuiz(QuizModel model)
        {
            bool result = false;
            var entity = new Quiz() { Title = model.Title };
            await repo.AddAsync(entity);

            try
            {
                await repo.SaveChangesAsync();
                result = true;
            }
            catch (Exception)
            {
            }

            return result;
        }

        public async Task Delete(int id)
        {
            await repo.DeleteAsync<Quiz>(id);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> Edit(QuizModel model)
        {
            bool result = false;
            var entity = await repo.GetByIdAsync<Quiz>(model.Id);

            if (entity != null)
            {
                entity.Title = model.Title;

                try
                {
                    await repo.SaveChangesAsync();
                    result = true;
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        public async Task<QuizModel> GetById(int id)
        {
            var entity = await repo.GetByIdAsync<Quiz>(id);
            QuizModel quizModel = null;

            if (entity != null)
            {
                quizModel = new QuizModel() 
                { 
                    Id = entity.Id,
                    Title = entity.Title
                };
            }

            return quizModel;
        }

        public async Task<IEnumerable<QuizModel>> GetQuizzes()
        {
            return await repo.AllReadonly<Quiz>()
                .Select(q => new QuizModel()
                {
                    Id = q.Id,
                    Title = q.Title
                })
                .ToListAsync();
        }
    }
}
