using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest;

public class GuestRepository : IGuestRepository
{
    private readonly HotelDbContext _hotelDbContext;

    public GuestRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }

    public async Task<int> CreateAsync(Domain.Entities.Guest guest)
    {
        _hotelDbContext.Guests.Add(guest);
        await _hotelDbContext
            .SaveChangesAsync();

        return guest.Id;
    }

    public async Task<Domain.Entities.Guest> GetAsync(int id)
    {
        return await _hotelDbContext.Guests
            .Where(g => g.Id == id)
            .FirstOrDefaultAsync();
    }
}
