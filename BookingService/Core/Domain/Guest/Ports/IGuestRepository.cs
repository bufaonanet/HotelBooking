
namespace Domain.Guest.Ports;

public interface IGuestRepository
{
    Task<Entities.Guest> GetAsync(int id);
    Task<int> CreateAsync(Entities.Guest guest);
}