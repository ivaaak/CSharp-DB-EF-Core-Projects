namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var games = JsonConvert.DeserializeObject<GameDto[]>(jsonString);
			StringBuilder sb = new StringBuilder(); 
			List<Game> dbGames = new List<Game>();
			List<Developer> devs = new List<Developer>();
			List<Genre> genres = new List<Genre>();
			List<Tag> tags = new List<Tag>();

            foreach (var game in games)
            {
                if (!IsValid(game))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

                if (!DateTime
					.TryParse(game.ReleaseDate, CultureInfo.InvariantCulture, 
					DateTimeStyles.None, out DateTime releaseDate))
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				var dbGame = new Game()
                {
					Name = game.Name,
                    Price = game.Price,
                    ReleaseDate = releaseDate
                };

				Developer dev = devs.FirstOrDefault(d => d.Name == game.Developer);
				if (dev == null)
				{
					dev = new Developer() { Name = game.Developer };
					devs.Add(dev);
				}

				dbGame.Developer = dev;
				Genre genre = genres.FirstOrDefault(g => g.Name == game.Genre);
				if (genre == null)
                {
					genre = new Genre() { Name = game.Genre };
					genres.Add(genre);
                }

				dbGame.Genre = genre;

                foreach (var tag in game.Tags)
                {
					Tag dbTag = tags.FirstOrDefault(t => t.Name == tag);
					if (dbTag == null)
                    {
						dbTag = new Tag() { Name = tag };
						tags.Add(dbTag);
                    }

					dbGame.GameTags.Add(new GameTag() { Tag = dbTag });
                }

				dbGames.Add(dbGame);
				sb.AppendLine($"Added {game.Name} ({game.Genre}) with {game.Tags.Length} tags");
            }

			context.Games.AddRange(dbGames);
			context.SaveChanges();

			return sb.ToString();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var users = JsonConvert.DeserializeObject<UserDto[]>(jsonString);
			var sb = new StringBuilder();
			List<User> usersList = new List<User>();

			foreach (var user in users)
            {
				bool hasInvalidCard = false;

                if (!IsValid(user))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				var dbUser = new User()
                {
					Age = user.Age,
					Email = user.Email,
					FullName = user.FullName,
					Username = user.Username
                };

                foreach (var card in user.Cards)
                {
					string[] validTypes = new string[] { "Debit", "Credit" };

                    if (!IsValid(card) || validTypes.Any(t => t == card.Type) == false)
                    {
						hasInvalidCard = true;
						break;
                    }

					Card dbCard = new Card()
					{
						Cvc = card.Cvc,
						Number = card.Number
					};

					dbCard.Type = (CardType)Enum.Parse(typeof(CardType), card.Type);
					dbUser.Cards.Add(dbCard);
				}

                if (hasInvalidCard)
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				usersList.Add(dbUser);
				sb.AppendLine($"Imported {user.Username} with {user.Cards.Length} cards");
            }

			context.Users.AddRange(usersList);
			context.SaveChanges();

			return sb.ToString();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var sb = new StringBuilder();
			XmlRootAttribute root = new XmlRootAttribute("Purchases"); 
			XmlSerializer serializer = new XmlSerializer(typeof(PurchaseDto[]), root);
			PurchaseDto[] purchases;
			List<Purchase> dbPurchases = new List<Purchase>(); 

            using (StringReader sr = new StringReader(xmlString))
            {
				purchases = (PurchaseDto[])serializer.Deserialize(sr);
            }

			var games = context.Games.ToList();
			var cards = context.Cards
				.AsQueryable()
				.Include(c => c.User)
				.ToList();

            foreach (var purchase in purchases)
            {
                if (!IsValid(purchase))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				Purchase dbPurchase = new Purchase()
				{
					ProductKey = purchase.Key
				};

                if (!DateTime
					.TryParseExact(purchase.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
					DateTimeStyles.None, out DateTime purchaseDate))
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				dbPurchase.Date = purchaseDate;

				if (!Enum.TryParse(typeof(PurchaseType), purchase.Type, out object purchaseType))
				{
					sb.AppendLine("Invalid Data");
					continue;
				}

				dbPurchase.Type = (PurchaseType)purchaseType;

				var card = cards.FirstOrDefault(c => c.Number == purchase.Card);

				if (card == null)
				{
					sb.AppendLine("Invalid Data");
					continue;
				}

				dbPurchase.Card = card;

				var game = games.FirstOrDefault(g => g.Name == purchase.Title);

				if (game == null)
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				dbPurchase.Game = game;
				dbPurchases.Add(dbPurchase);
				sb.AppendLine($"Imported {purchase.Title} for {card.User.Username}");
			}

			context.Purchases.AddRange(dbPurchases);
			context.SaveChanges();

			return sb.ToString();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}