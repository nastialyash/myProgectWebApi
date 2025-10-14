namespace myProgectWebApi.DAL.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } 
        public string Genre { get; set; } = string.Empty;   
        public DateTime ReleaseDate { get; set; }   
        public double Price { get; set; }

        public List<string> ImagePaths { get; set; } 
    }
}
