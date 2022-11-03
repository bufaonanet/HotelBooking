namespace Domain.Room.Ports;

public interface IRoomRepository
{
    Task<Entities.Room> GetAsync(int Id);
    Task<int> CreateAsync(Entities.Room room);
    //Task<Entities.Room> GetAggregate(int Id);
}
