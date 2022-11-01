using HotelBooking.Core;
using Moq;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class CreateBookingStepDefinitions
    {
        DateTime startDateVar, endDateVar;
        bool Available;

        private readonly Mock<IRepository<Booking>> _bookingRepository = new();
        private readonly Mock<IRepository<Room>> _roomRepository = new();
        private readonly IBookingManager _bookingManager;
        public static int _startOccupiedDay = 5;
        public static int _endOccupiedDay = 10;

        public CreateBookingStepDefinitions()
        {
            DateTime startOccupiedDate = DateTime.Today.AddDays(_startOccupiedDay);
            DateTime endOccupiedDate = DateTime.Today.AddDays(_endOccupiedDay);

            //Create Mock data
            Booking[] activeBookings = new Booking[3]
            {
                new Booking { StartDate = startOccupiedDate, EndDate = endOccupiedDate, IsActive = true, RoomId = 1 },
                new Booking { StartDate = startOccupiedDate, EndDate = endOccupiedDate, IsActive = true, RoomId = 2 },
                new Booking { StartDate = startOccupiedDate, EndDate = endOccupiedDate, IsActive = true, RoomId = 3 }
            };
            Room[] rooms = new Room[3]
            {
                new Room { Id = 1, Description = "Room A" },
                new Room { Id = 2, Description = "Room B" },
                new Room { Id = 3, Description = "Room C" }
            };

            //Add Mock data to Mock Repos
            _bookingRepository.Setup(x => x.Add(It.IsAny<Booking>()));
            _bookingRepository.Setup(x => x.GetAll()).Returns(activeBookings);
            _roomRepository.Setup(x => x.GetAll()).Returns(rooms);
            _bookingManager = new BookingManager(_bookingRepository.Object, _roomRepository.Object);

        }


        [Given(@"i have entered a (.*)")]
        public void GivenIHaveEnteredA(int startDate)
        {
            startDateVar = DateTime.Today.AddDays(startDate);
        }

        [Given(@"i have also entered a (.*)")]
        public void GivenIHaveAlsoEnteredA(int endDate)
        {
            endDateVar = DateTime.Today.AddDays(endDate);
        }

        
        [When(@"i press book")]
        public void WhenIPressBook()
        {
            
            Available = _bookingManager.CreateBooking(new Booking()
            {
                CustomerId = 2,
                StartDate = startDateVar,
                EndDate = endDateVar
            });
        }

        [Then(@"the booking should succeed or fail (.*)")]
        public void ThenTheBookingShouldSucceedOrFail(bool available)
        {
            Assert.Equal(Available, available);
        }
       
    }
}