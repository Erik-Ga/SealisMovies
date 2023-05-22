namespace SealisMovies.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Text { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public int DiscussionId { get; set; }
    }
}
