using Application;
using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestsController : ControllerBase
{
    private readonly ILogger<GuestsController> _logger;
    private readonly IGuestManager _guestManager;

    public GuestsController(ILogger<GuestsController> logger,
                            IGuestManager guestManager)
    {
        _logger = logger;
        _guestManager = guestManager;
    }

    [HttpPost]
    public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
    {
        var request = new CreateGuestRequest
        {
            Data = guest,
        };

        var res = await _guestManager.CreateGuest(request);

        if (res.Success)
            return Created("", res.Data);

        if (res.ErrorCode == ErrorCodes.NOT_FOUND)
            return BadRequest();

        if (res.ErrorCode == ErrorCodes.NOT_FOUND)
        {
            return NotFound(res);
        }
        else if (res.ErrorCode == ErrorCodes.INVALID_PERSON_ID)
        {
            return BadRequest(res);
        }
        else if (res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION)
        {
            return BadRequest(res);
        }
        else if (res.ErrorCode == ErrorCodes.INVALID_EMAIL)
        {
            return BadRequest(res);
        }
        else if (res.ErrorCode == ErrorCodes.COULD_NOT_STORE_DATA)
        {
            return BadRequest(res);
        }      

        _logger.LogError("Response with unkonw ErrorCode Returned", res);
        return BadRequest(StatusCodes.Status500InternalServerError);
    }

    [HttpGet]
    public async Task<ActionResult<GuestDTO>> Get(int guestId)
    {
        var res = await _guestManager.GetGuest(guestId);

        if (res.Success) return Ok(res.Data);

        return NotFound(res);
    }
}
