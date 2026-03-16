namespace LiveChat.Api.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public required string Content { get; set; }
        public int RoomID { get; set; }
        public DateTime SentAt { get; set; }
    }
}