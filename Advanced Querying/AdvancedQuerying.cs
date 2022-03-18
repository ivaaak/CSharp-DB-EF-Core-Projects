using System;
using System.Linq;
using System.Text;
using Data;
using Initializer;

//This project isnt complete and doesnt run
//These are just the queries/answers to lab problems - a showcase of the LINQ queries
namespace AdvancedQuerying
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
		//Explicit loading:
		var employee = context.Employees.First().Load();
		//ToList ToArray FirstOrDefault SingleOrDefault
	
		var explicitlyLoadedEmployee = context.Employees
			.Reference(e=>e.Deparetments)
			.Load(); 
			//-> explicitly load an obj
			
		//Eager loading			
		var employee = context.Employees.First().Include();
	
		//Lazy loading	
		virtual var employee = context.Employees.First().Load();
	}
	
	//Problem 01
	public static string GetBooksByAgeRestriction(BookShopContext context, string command) 
	{
		StringBuilder sb = new StringBuilder(); 
		AgeRestriction ageRestriction = 
			Enum.Parse<AgeRestriction>(command, true); 
	}
	  
	//Problem 02
	public static string GetBooksByAgeRestriction(BookShopContext context, string command) 
	{
		StringBuilder sb = new StringBuilder(); 
			
		//Cannot be null 
		AgeRestriction ageRestriction = 
			Enum.Parse<AgeRestriction> (command, true); 

		string[] bookTitles = context 									// BookShopContext 
			.Books											// DbSet<Book>
			.Where(b => b.AgeRestriction == ageRestriction) 					// IQueryable<Book> 
			.OrderBy(b => b. Title) 								// IOrderedQueryable<Book> 
			.Select(b => b.Title)									// IQueryable<string>
			.ToArray(); 
		
		foreach (string title it bookTitles) 
		{ 
			sb.AppendLine(title); 
		}
			
		return sb.ToString().TrimEnd(); 
	} 

	//Problem 03 
	public static string GetGoldenBooks(BookShopContext context) 
	{ 
		StringBuilder sb = new StringBuilder(); 
		
		string[] goldenBooksTitles = context 								// BookShopContext 
			.Books   										// DbSet<Book>
			.Where(b => b.EditionType == EditionType.Gold && 
						 b.Copies < 5000) 						// IQueryable<Book> 
			.OrderBy(b => b.BookId) 								// IOrderedQueryable<Book> 
			.Select(b => b.Title) 									// IQueryable<string> 
			.ToArray(); 
		
		foreach (string title in goldenBooksTitles) 
		{
			sb.AppendLine(title); 
		} 
		
		return sb.ToString().TrimEnd();
	}
	
	//Problem 04 
	public static string GetBooksNotReleasedIn(BookShopContext context, year) 
	{ 
		StringBuilder sb = new StringBuilder(); 
	
		string[] booksNotReleasedInTitles = context							// BookShopContext 
			.Books 											// DbSet<Book> 
			.Where(b => b.ReleaseDate.HasValue &&
						 b.ReleaseDate.Value.Year != year)				// IQueryable<Book> 
			.OrderBy(b => b . BookId) 								// IOrderedQueryable<Boolc 
			.Select (b => b.Titie)  								// IQueryable<string> 
			.ToArray(); 
		
		foreach (string title in booksNotReleasedInTitles)
		{
			sb.AppendLine(title);
		}
		
		return sb.ToString().TrimEnd();
	} 
	
	//Problem 05
	public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
	{ 
		StringBuilder sb = new StringBuilder(); 
		
		string[] authorNames = context    								// BookShopContext 
			.Authors 										// DbSet<Author> 
			.ToArray()                             							// Author[]      used to bypass EF bug (cant translate query)
			.Where(a => 
					a.FirstName
					.ToLower()
					.EndsWith(input.ToLower()))						// IQueryable<Author> 
			.Select (a => $''{a.FirstName} {a.LastName}'') 						// IQueryable<string> 
			.OrderBy(n => n)									// IOrderedQueryabie<string> 
			.ToArray();
			
		foreach (string authorName in authorNames)
		{
			sb.AppendLine(authorName);
		}
			
		return sb.ToString().TrimEnd();
	}
	
	//Problem 06
	public static string CountCopiesByAuthor(BookShopContext context) 
	{ 
		StringBuilder sb = new StringBuilder(); 
		
		var authorsWithBookCopies = context 							// BookShopContext 
			.Authors 									// DbSet<Author> 
			.Select(a => new 
			{ 
				FullName = a.FirstName + " " + a.LastName, 
				TotalBookCopies = a  							// Author
					.Books 								// ICollection<Book>
					.Sum(b => b.Copies) 						// int 
			}) 										// IQueryable<{FullName, TotalBookCopies}> 
			.OrderByDescending(a => a .TotalBookCopies) 					//OrderedQueryable<(FullName, TotalBookCopies)> 
			.ToArray(); 
			
		foreach (var author in authorsWithBookCopies) 
		{ 
			sb.AppendLine($"{author.FullName} - fauthor.TotalBookCopiesl"); 
		}
		
		return sb.ToString().TrimEnd();
	}
	
	//Problem 07
	public static string GetTotalPriceByCategory(BookShopContaxt contaxt)
	{
		StringBuilder sb = new StringBuilder(); 
		
		var categoriesByProfit = context						// BookShopContext 
			.Categories 								// DbSet<Category> 
			.Select(c => new 
			{ 
				CategoryName = c.Name, 
				TotalProfit = c.CategoryBooks 					// ICollection<BookCategory> 
					.Sum(cb => cb.Book.Copies * cb.Book.Price)  		//decimal
			}									// IQueryable<CategoryName, TotalProfit> 
			.OrderByDescending(c => c.TotalProfit) 	
			.ThenBy(c => c.CategoryName)   		 				// IOrderedQueryable<CategoryName,TotalProfit> 
			.ToArray(); 
		
		foreach (var category categoriesByProfit) 
		{
			sb.AppendLine($"{category.CategoryName} ${category.TotalProfit:f2}"); 
		} 
		
		return sb.ToString().TrimEnd();
	} 
				
	// Problem 08
	public static string GetMostRecentBooks(BookShopContext context) 
	{ 
		StringBuilder sb = new StringBuilder(); 
		var categoriesWithMostRecentBooks = context 					// BookShopContext 
			.Categories								// DbSet<Category> 
			.Select(c => new 
			{ 
				CategoryName = c.Name, 
				MostRecentBooks = c.CategoryBooks				// ICollection<BookCategory> 
					.Select (cb => cb.Book) 				// IEnumerable<Book> 
					.OrderByDescending(b => b.ReleaseDate)			// IOrderedEnumerable<Book> 
					.Select(b => new
					{
						BookTitle = b.Title, 
						ReleaseYear = b.ReleaseDate.Value.Year
					})
					.Take (3) 						// IEnumerable“BookTitle,ReleaseYearl> 
					.ToArray() 						// {BookTitle,ReleaseYear}  
			})									// IQueryable<{CategoryName, MostRecentBooks}> 
			.OrderBy(c => c.CategoryName) 						// IOrderedQueryable<CategoryName,MostRecentBooks> 
			.ToArray(); 
			
		foreach (var category categoriesByProfit) 
		{
			sb.AppendLine($"{book.BookTitle} ${book.ReleaseYear:f2}"); 
		} 
		return sb.ToString().TrimEnd();
	}

	//Problem 09
	public static void IncreasePrices(BookShopContext context)
	{
		IQueryable<Book> allBooksBefore2010 = context	
			.Books
			.Where(b => b.ReleaseDate.HasValue &&
						b.ReleaseDate.Value.Year < 2010);
						
		foreach(Book book in allBooksBefore2010)
		{
			book.Price += 5;
		}
		context.SaveChanges();
	}
   }
}
