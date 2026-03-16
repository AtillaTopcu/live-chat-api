using LiveChat.Api.Data;
using LiveChat.Api.DTOs;
using LiveChat.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveChat.Api.Services
{
    public class RoomService : IRoomService
    {
        private readonly LiveChatDbContext _context;

        public RoomService(LiveChatDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoom(CreateRoomDto createRoomDto, int CreatorID)
        {
            Room room = new Room
            {
                CreatorID = CreatorID,
                RoomName = createRoomDto.RoomName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return rooms;
        }
    }
}