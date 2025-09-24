namespace myProgectWebApi.DAL.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;   
        public DateTime ReleaseDate { get; set; }   
        public double Price { get; set; }
    }
}
