namespace SealisMovies.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Imgage { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
