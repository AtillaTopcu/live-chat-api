namespace LiveChat.Api.Models
{
    public class User
    {
        public int ID { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}