using Microsoft.EntityFrameworkCore;
using MyQuizApplication.Infrastructure.Data.Models;

namespace MyQuizApplication.Infrastructure.Data
{
    public partial class QuizContext : DbContext
    {
        public QuizContext()
        {
        }

        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.;Database=SoftUni;User Id=sa;Password=SoftUn!2021;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
