namespace myProgectWebApi.DAL.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }

        public string ImagePath { get; set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }
    }
}
