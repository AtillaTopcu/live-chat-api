using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LiveChat.Api.DTOs;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoomService roomService;

    public RoomController(IRoomService roomService)
    {
        this.roomService = roomService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto createRoomDto)
    {
        var creatorID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        await roomService.CreateRoom(createRoomDto, creatorID);

        return Ok("Yeni oda oluşturuldu!");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var result = await roomService.GetAllRooms();
        if (result == null) return NotFound();

        return Ok(result);
    }
}