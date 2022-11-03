using Domain.Guest.Enums;
using Action = Domain.Guest.Enums.Action;

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
}
