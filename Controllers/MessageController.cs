using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LiveChat.Api.Data;
using Microsoft.EntityFrameworkCore;
using LiveChat.Api.DTOs;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly LiveChatDbContext _context;

    public MessageController(LiveChatDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetMessages(int roomID)
    {
        return Ok(await _context.Messages.Where(m => m.RoomID == roomID)
        .Join(_context.Users, message => message.AuthorID, user => user.ID, (message, user) => new MessageResponseDto
        {
            AuthorID = message.AuthorID,
            Username = user.Username,
            Content = message.Content,
            SentAt = message.SentAt
        }).OrderBy(m => m.SentAt).ToListAsync());
    }
}