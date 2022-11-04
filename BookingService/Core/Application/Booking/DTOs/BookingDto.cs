using Domain.Booking.Enums;

namespace Application.Booking.DTOs;

public class BookingDto
{
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    private Status Status { get; set; }

    public BookingDto()
    {
        PlacedAt = DateTime.UtcNow;
    }

    public static Domain.Booking.Entities.Booking MapToEntity(BookingDto bookingDto)
    {
        return new Domain.Booking.Entities.Booking
        {
            Id = bookingDto.Id,
            Start = bookingDto.Start,
            Guest = new Domain.Guest.Entities.Guest { Id = bookingDto.GuestId },
            Room = new Domain.Room.Entities.Room { Id = bookingDto.RoomId },
            End = bookingDto.End,
            PlacedAt = bookingDto.PlacedAt,
        };
    }
}
