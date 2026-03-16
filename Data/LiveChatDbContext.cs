using Microsoft.EntityFrameworkCore;
using LiveChat.Api.Models;

namespace LiveChat.Api.Data
{
    public class LiveChatDbContext : DbContext
    {
        public LiveChatDbContext(DbContextOptions<LiveChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}