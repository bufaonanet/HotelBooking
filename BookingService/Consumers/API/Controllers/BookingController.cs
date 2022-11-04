using Application;
using Application.Booking.DTOs;
using Application.Booking.Ports;
using Application.Booking.Responses;
using Application.Payment.DTOs;
using Application.Payment.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IBookingManager _bookingManager;

    public BookingController(
        ILogger<BookingController> logger,
        IBookingManager bookingManager)
    {
        _logger = logger;
        _bookingManager = bookingManager;
    }

    [HttpPost]
    public async Task<ActionResult<BookingResponse>> Post(BookingDto booking)
    {
        var res = await _bookingManager.CreateBookingAsync(booking);

        if (res.Success) return Created("", res.Data);

        else if (res.ErrorCode == ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION)
        {
            return BadRequest(res);
        }
        else if (res.ErrorCode == ErrorCodes.BOOKING_COULD_NOT_STORE_DATA)
        {
            return BadRequest(res);
        }
        else if (res.ErrorCode == ErrorCodes.BOOKING_ROOM_CANNOT_BE_BOOKED)
        {
            return BadRequest(res);
        }

        _logger.LogError("Response with unknown ErrorCode Returned", res);
        return BadRequest(500);
    }

    [HttpPost]
    [Route("{bookingId}/Pay")]
    public async Task<ActionResult<PaymentResponse>> Pay(
            PaymentRequestDto paymentRequestDto, 
            int bookingId)
    {
        paymentRequestDto.BookingId = bookingId;
        var res = await _bookingManager.PayForABooking(paymentRequestDto);

        if (res.Success) return Ok(res.Data);

        return BadRequest(res);
    }

}
