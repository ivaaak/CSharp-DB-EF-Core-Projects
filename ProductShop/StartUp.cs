using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
   public class StartUp
   {
      public static void Main(string[] args)
      {
		var context = new ProductShopContext();
		
		//Initialize/Reset DB when starting main
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
		
		string usersJsonAsString = File.ReadAllText("../../../Datasets/users.json");
		string productsJsonAsString = File.ReadAllText("../../../Datasets/products.json");
		//find directory -> ../../../ = 3 folders back form the dll/compiled file
		
		Console.WriteLine(ImportUsers(context, usersJsonAsString));
      }
	  
	public static string ImportUsers(ProductShopContext context, string inputJson) 
	{ 
		IEnumerable<UserInputDto> users = JsonConvert.DeserializeObject<IEnumerable<UserInputDto>>(inputJson); 
		
		var mapperConfiguration = new MapperConfiguration(cfg => 
		{ 
			cfg.AddProfile<ProductShopProfile>(); 
		});

		IMapper mapper = new Mapper(mapperConfiguration);
		
		IEnumerable<User> mappedUsers = mapper.Map<IEnumerable<User>>(users); 

		context.Users.AddRange(mappedUsers); 
		context.SaveChanges(); 
		
		return $"Successfully imported {mappedUsers.Count()}"; 
	} 

	public static string ImportProducts(ProductShopContext context, string inputJson)
	{
		IEnumerable<ProductInputDTO> products = 
			JsonConvert.DeserializeObject<IEnumerable<ProductInputDTO>>(inputJson);
		
		InitializeMapper();
		
		var mappedProducts = mapper.Map<IEnumerable<Product>>(products);
		context.Products.AddRange(mappedProducts); 
		context.SaveChanges(); 
		
		return "Successfully imported {}"
		
	}

	//static mapping
	public static class UserMappings
	{
		public static User MapToDomainUser(this UserInputDto userDto)
		{
			return new User
			{
				Age = userDto.Age,
				FirstName = userDto.FirstName,
				LastName = userDto.LastName
			};
		}
	}
   }
}