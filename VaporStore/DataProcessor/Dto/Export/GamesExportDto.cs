namespace VaporStore.DataProcessor.Dto.Export
{
    public class GamesExportDto
    {
        public int Id { get; set; }      
        public string Genre { get; set; }
        public GameExDto[] Games { get; set; }
        public int TotalPlayers { get; set; }
    }
}
