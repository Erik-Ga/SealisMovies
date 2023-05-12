namespace SealisMovies.Models
{
    public class Inbox
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Message> MessageList { get; set; }
    }
}
