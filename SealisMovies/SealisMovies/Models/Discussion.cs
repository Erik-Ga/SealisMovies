namespace SealisMovies.Models
{
    public class Discussion
    {        
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? Category { get; set; }
        public string? ProfilePicture { get; set; }
        public bool? Reported { get; set; }
        public List<Comment>? CommentList { get; set; }
    }
}
