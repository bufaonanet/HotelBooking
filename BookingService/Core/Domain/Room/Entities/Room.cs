using Domain.Booking.Enums;
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
    public ICollection<Booking.Entities.Booking> Bookings { get; set; }
    public bool IsAvailable
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
        get
        {
            var notAvailableStatus = new List<Status>
            {
                Status.Created,
                Status.Paid,
            };
            return Bookings.Where(b => b.Room.Id == Id && notAvailableStatus.Contains(b.CurrentStatus)).Any();
        }
    }

    public bool IsValid()
    {
        try
        {
            ValidateState();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
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

    public bool CanBeBooked()
    {
        try
        {
            ValidateState();
        }
        catch (Exception)
        {

            return false;
        }

        if (!IsAvailable)
        {
            return false;
        }

        return true;
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
