namespace SealisMovies.Models
{
    public class Message
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? RecieverId { get; set; }
        public string? ReceiverName { get; set; }
        public bool? Sent { get; set; }
    }
}
