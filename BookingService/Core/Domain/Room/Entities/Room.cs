using Domain.Room.Exceptions;
using Domain.Room.Ports;
using Domain.Room.ValueObjects;

namespace Domain.Room.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public Price Price { get; set; }
    public bool IsAvailabel
    {
        get
        {
            if (InMaintenance || HasGest)
            {
                return false;
            }
            return true;
        }
    }

    public bool HasGest
    {
        //Verificar se existem bookins abertos para este room
        get { return true; }
    }

    private void ValidateState()
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new InvalidRoomDataException();
        }

        if (Price == null || Price.Value < 10)
        {
            throw new InvalidRoomPriceException();
        }
    }

    public async Task Save(IRoomRepository roomRepository)
    {
        ValidateState();

        if (Id == 0)
        {
            Id = await roomRepository.CreateAsync(this);
        }
        else
        {
        }
    }
}
