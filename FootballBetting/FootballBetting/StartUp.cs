using System;
using Data;

namespace FootballBetting
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            FootballBettingContext dbContext = new FootballBettingContext();

            dbContext.Database.EnsureCreated();

            Console.WriteLine("DB Created Successfully!");
            Console.WriteLine("Do you want to delete DB (Y / N)");

            string result = Console.ReadLine();
            if (result == "Y")
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
