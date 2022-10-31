using BoDi;
using HotelBooking.Core;
using Moq;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Hooks
{
    [Binding]
    public sealed class Hooks1
    {
        private readonly IObjectContainer container;
        private readonly Mock<IRepository<Booking>> _mockBookingRepo;
        private readonly Mock<IRepository<Room>> _mockRoomRepo;

        public Hooks1(IObjectContainer container)
        {
            this.container = container;
            _mockRoomRepo = new Mock<IRepository<Room>>();
            _mockBookingRepo = new Mock<IRepository<Booking>>();
        }

        [BeforeScenario]
        public void InitializeBookingManager()
        {
            var bookingManager = new BookingManager(_mockBookingRepo.Object, _mockRoomRepo.Object);
            container.RegisterInstanceAs<IBookingManager>(bookingManager);
        }
    }
}