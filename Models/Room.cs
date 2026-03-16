namespace LiveChat.Api.Models
{
    public class Room
    {
        public int ID { get; set; }
        public required int CreatorID { get; set; }
        public required string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}