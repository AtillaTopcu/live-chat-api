using LiveChat.Api.DTOs;
using LiveChat.Api.Models;

public interface IRoomService {
    Task<bool> CreateRoom(CreateRoomDto createRoomDto, int creatorID);
    Task<List<Room>> GetAllRooms();
}