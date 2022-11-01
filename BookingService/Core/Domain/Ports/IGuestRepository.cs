using Domain.Entities;

namespace Domain.Ports;

public interface IGuestRepository
{
    Task<Guest> GetAsync(int id);
    Task<int> CreateAsync(Guest guest);
}
