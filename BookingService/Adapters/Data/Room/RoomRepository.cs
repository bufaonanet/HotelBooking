using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Room;

public class RoomRepository : IRoomRepository
{
    private readonly HotelDbContext _hotelDbContext;

    public RoomRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }

    public async Task<int> CreateAsync(Domain.Room.Entities.Room room)
    {
        _hotelDbContext.Rooms.Add(room);
        await _hotelDbContext.SaveChangesAsync();
        return room.Id;
    }

    public async  Task<Domain.Room.Entities.Room> GetAsync(int Id)
    {
        return await _hotelDbContext.Rooms
               .Where(g => g.Id == Id)
               .FirstAsync();
    }
}