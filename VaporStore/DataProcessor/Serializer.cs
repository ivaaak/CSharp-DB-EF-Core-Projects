namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			List<GamesExportDto> games = new List<GamesExportDto>();
			var gamesToProcess = context.Games
				.AsQueryable()
				.Where(g => genreNames.Contains(g.Genre.Name))
				.Where(g => g.Purchases.Any())
				.Include(g => g.GameTags)
				.ThenInclude(gt => gt.Tag)
				.Include(g => g.Purchases)
				.Include(g => g.Genre)
				.ToList();

			foreach (string genre in genreNames) 
			{
				var genreGames = gamesToProcess
					.Where(g => g.Genre.Name == genre)
					.ToList();

				if (genreGames.Count == 0)
                {
					continue;
                }

				var result = new GamesExportDto()
				{
					Id = genreGames.First().Genre.Id,
					Genre = genreGames.First().Genre.Name,
					Games = genreGames
					.Select(g => new GameExDto()
                    {
						Id = g.Id,
						Developer = g.Developer.Name,
						Title = g.Name,
						Tags = string.Join(", ", g.GameTags.Select(t => t.Tag.Name)),
						Players = g.Purchases.Count
                    })
					.OrderByDescending(g => g.Players)
					.ThenBy(g => g.Id)
					.ToArray()
				};

				result.TotalPlayers = result.Games.Sum(g => g.Players);

				games.Add(result);
			}

			games = games
				.OrderByDescending(g => g.TotalPlayers)
				.ThenBy(g => g.Id)
				.ToList();

			return JsonConvert.SerializeObject(games, Formatting.Indented);
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			List<UserExportDto> users = new List<UserExportDto>();
			PurchaseType purcahseType = (PurchaseType)Enum.Parse(typeof(PurchaseType), storeType);
			var usersToProcess = context.Purchases
				.AsQueryable()
				.Where(p => p.Type == purcahseType)
				.Include(p => p.Game.Genre)
				.Include(p => p.Card.User)
				.ToList()
				.GroupBy(p => p.Card.User.Username)
				.ToList();

			foreach (var user in usersToProcess)
            {
				var result = new UserExportDto()
				{
					username = user.Key,
					Purchases = user
					.OrderBy(p => p.Date)
					.Select(p => new UserPurchase()
					{
						Card = p.Card.Number,
						Cvc = p.Card.Cvc,
						Date = p.Date.ToString("yyyy-MM-dd HH:mm"),
						Game = new UserPurchaseGame()
						{
							Genre = p.Game.Genre.Name,
							Price = p.Game.Price,
							title = p.Game.Name
						}
					}).ToArray()
				};

				result.TotalSpent = result.Purchases
					.Select(p => p.Game.Price)
					.Sum();
				users.Add(result);
            }

			users = users
				.OrderByDescending(u => u.TotalSpent)
				.ThenBy(u => u.username)
				.ToList();

			XmlRootAttribute root = new XmlRootAttribute("Users");
			XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
			xmlns.Add(string.Empty, string.Empty);
			XmlSerializer serializer = new XmlSerializer(typeof(UserExportDto[]),  root);
			StringBuilder sb = new StringBuilder();

			using (StringWriter sw = new StringWriter(sb))
			{
				serializer.Serialize(sw, users.ToArray(), xmlns);
			}

			return sb.ToString();
		}
	}
}