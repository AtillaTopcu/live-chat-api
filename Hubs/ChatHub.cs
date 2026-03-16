using System.Security.Claims;
using LiveChat.Api.Data;
using LiveChat.Api.DTOs;
using LiveChat.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LiveChat.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly LiveChatDbContext _context;

        public ChatHub(LiveChatDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(SendMessageDto sendMessageDto, int roomID)
        {
            var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonim";

            Message message = new Message
            {
                AuthorID = int.Parse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"),
                Content = sendMessageDto.Content,
                RoomID = roomID,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            await Clients.Group(roomID.ToString()).SendAsync("receivedMessage", username, sendMessageDto.Content);
        }

        public async Task JoinRoom(int roomID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomID.ToString());
        }

        public async Task LeaveRoom(int roomID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomID.ToString());
        }
    }
}