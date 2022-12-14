using Domain.Booking.Enums;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Action = Domain.Booking.Enums.Action;

namespace Domain.Booking.Entities;

public class Booking
{
    public Booking()
    {
        _status = Status.Created;
    }

    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    private Status _status;
    public Status CurrentStatus { get => _status; }

    public Guest.Entities.Guest Guest { get; set; }
    public Room.Entities.Room Room { get; set; }

    public void ChangeState(Action action)
    {
        _status = (_status, action) switch
        {
            (Status.Created, Action.Pay) => Status.Paid,
            (Status.Created, Action.Cancel) => Status.Canceled,
            (Status.Paid, Action.Finish) => Status.Finished,
            (Status.Paid, Action.Refound) => Status.Refounded,
            (Status.Canceled, Action.Reopen) => Status.Created,
            _ => _status
        };
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
        if (PlacedAt == default)
        {
            throw new PlacedAtIsARequiredInformationException();
        }

        if (Start == default(DateTime))
        {
            throw new StartDateTimeIsRequiredException();
        }

        if (End == default(DateTime))
        {
            throw new EndDateTimeIsRequiredException();
        }

        if (Room == null)
        {
            throw new RoomIsRequiredException();
        }

        if (Guest == null)
        {
            throw new GuestIsRequiredException();
        }
    }

    public async Task Save(IBookingRepository bookingRepository)
    {
        ValidateState();

        Guest.IsValid();

        if (!Room.CanBeBooked())
        {
            throw new RoomCannotBeBookedException();
        };

        if (Id == 0)
        {
            var resp = await bookingRepository.CreateBookingAsync(this);
            Id = resp.Id;
        }
        else
        {

        }
    }
}
