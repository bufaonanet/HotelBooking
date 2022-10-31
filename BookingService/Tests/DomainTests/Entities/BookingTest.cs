using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Entities
{
    public class BookingTest
    {
        [Fact]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.Equal(Status.Created, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToPaidWhenCurrentStatusIsCreated()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);

            Assert.Equal(Status.Paid, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToCanceledWhenCurrentStatusIsCreated()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);

            Assert.Equal(Status.Canceled, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToFinishedWhenCurrentStatusIsPaid()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);

            Assert.Equal(Status.Finished, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToRefoundedWhenCurrentStatusIsPaid()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);

            Assert.Equal(Status.Refounded, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToCreatedWhenCurrentStatusIsCanceled()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);

            Assert.Equal(Status.Created, booking.CurrentStatus);
        }
    }
}