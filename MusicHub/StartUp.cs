using System;
using System.Linq;
using System.Text;
using Data;
using Initializer;
    
namespace MusicHub
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var sb = new StringBuilder();

            var albumInfo = context.Producers
                            .FirstOrDefault(x => x.Id == producerId)
                            .Albums
                            .Select(x => new
                            {
                                AlbumName = x.Name,
                                ReleaseDate = x.ReleaseDate,
                                ProducerName = x.Producer.Name,
                                Songs = x.Songs.Select(x => new
                                {
                                    SongName = x.Name,
                                    Price = x.Price,
                                    Writer = x.Writer.Name
                                })
                                .OrderByDescending(x => x.SongName)
                                .ThenBy(x => x.Writer)
                                .ToList(),
                                AlbumPrice = x.Price
                            })
                            .OrderByDescending(x => x.AlbumPrice)
                            .ToList();

            foreach (var album in albumInfo)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}")
                .AppendLine($"-ReleaseDate: {album.ReleaseDate:MM/dd/yyyy}")
                .AppendLine($"-ProducerName: {album.ProducerName}")
                .AppendLine("-Songs:");

                int counter = 1;

                foreach (var song in album.Songs)
                {
                    sb.AppendLine($"---#{counter++}")
                        .AppendLine($"---SongName: {song.SongName}")
                        .AppendLine($"---Price: {song.Price:F2}")
                        .AppendLine($"---Writer: {song.Writer}");
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:F2}");
            }

            return sb.ToString().TrimEnd();
        }
	  

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var sb = new StringBuilder();

            var allSongs = context.Songs
                        .ToList() //load everything locally (materialize)
                        .Where(x => x.Duration.TotalSeconds > duration)
                        .Select(x => new
                        {
                            SongName = x.Name,
                            Writer = x.Writer.Name,
                            PerformerFullName = x.SongPerformers
                                                .Select(x => x.Performer.FirstName + " "
                                                + x.Performer.LastName).FirstOrDefault(),
                            AlbumProducer = x.Album.Producer.Name,
                            Duration = x.Duration
                        })
                        .OrderBy(x => x.SongName)
                        .ThenBy(x => x.Writer)
                        .ThenBy(x => x.PerformerFullName)
                        .ToList();

            int count = 1;

            foreach (var song in allSongs)
            {
                sb.AppendLine($"-Song #{count++}")
                    .AppendLine($"---SongName: {song.SongName}")
                    .AppendLine($"---Writer: {song.Writer}")
                    .AppendLine($"---Performer: {song.PerformerFullName}")
                    .AppendLine($"---AlbumProducer: {song.AlbumProducer}")
                    .AppendLine($"---Duration: {song.Duration:c}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}