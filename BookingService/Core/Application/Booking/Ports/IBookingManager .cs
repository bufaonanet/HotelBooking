using Application.Booking.DTOs;
using Application.Booking.Responses;
using Application.Payment.DTOs;
using Application.Payment.Responses;

namespace Application.Booking.Ports;

public interface IBookingManager
{
    Task<BookingResponse> CreateBookingAsync(BookingDto booking);
    Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequestDto);
    Task<BookingDto> GetBooking(int id);
}


