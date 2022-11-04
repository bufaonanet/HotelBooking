namespace Domain.Booking.Ports;

public interface IBookingRepository
{
    Task<Entities.Booking> GetAsync(int id);
    Task<Entities.Booking> CreateBookingAsync(Entities.Booking booking);
}
