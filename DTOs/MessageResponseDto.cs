namespace LiveChat.Api.DTOs
{
    public class MessageResponseDto
    {
        public required int AuthorID { get; set; }
        public required string Username { get; set; }
        public required string Content { get; set; }
        public DateTime SentAt { get; set; }
    }   
}