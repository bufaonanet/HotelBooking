using Application;
using Application.Room.DTOs;
using Application.Room.Ports;
using Application.Room.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly ILogger<GuestsController> _logger;
    private readonly IRoomManager _roomManager;

    public RoomController(ILogger<GuestsController> logger,
                          IRoomManager roomManager)
    {
        _logger = logger;
        _roomManager = roomManager;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Post(RoomDto room)
    {
        var request = new CreateRoomRequest
        {
            Data = room
        };

        var res = await _roomManager.CreateRoomAsync(request);

        if (res.Success) return Created("", res.Data);

        else if (res.ErrorCode == ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION)
        {
            return BadRequest(res);
        }
        else if (res.ErrorCode == ErrorCodes.ROOM_COULD_NOT_STORE_DATA)
        {
            return BadRequest(res);
        }

        _logger.LogError("Response with unknown ErrorCode Returned", res);
        return BadRequest(500);
    }

    [HttpGet]
    public async Task<ActionResult<RoomDto>> Get(int rooomId)
    {
        var res = await _roomManager.GetRoomAsync(rooomId);

        if (res.Success) return Ok(res.Data);

        return NotFound(res);
    }
}
