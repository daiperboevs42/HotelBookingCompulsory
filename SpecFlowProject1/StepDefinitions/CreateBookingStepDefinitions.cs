using HotelBooking.Core;
using Moq;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class CreateBookingStepDefinitions
    {
        DateTime startDate, endDate;
        bool Available;
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IRepository<Booking>> _bookingRepository = new();
        private readonly Mock<IRepository<Room>> _roomRepository = new();
        private readonly IBookingManager _bookingManager;
        public static int _startOccupiedDay = 5;
        public static int _endOccupiedDay = 10;

        public CreateBookingStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            DateTime startOccupiedDate = DateTime.Now.AddDays(_startOccupiedDay);
            DateTime endOccupiedDate = DateTime.Now.AddDays(_endOccupiedDay).AddHours(1);

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


        [Given(@"i have entered a start Date")]
        public void GivenIHaveEnteredA()
        {
            //StartDate = startDate;
            //StartDate = DateTime.Parse(startDate);
            startDate = DateTime.Today.AddDays(25);
        }

        [Given(@"i have also entered a end Date")]
        public void GivenIHaveAlsoEnteredA()
        {
            //EndDate = endDate;
            //EndDate = DateTime.Parse(endDate);
            endDate = DateTime.Today.AddDays(30);
        }

        
        [When(@"i press book")]
        public void WhenIPressBook()
        {
            
            Available = _bookingManager.CreateBooking(new Booking()
            {
                CustomerId = 2,
                StartDate = startDate,
                EndDate = endDate
            });
        }

        [Then(@"the booking should succeed or fail")]
        public void ThenTheBookingShouldSucceedOrFail()
        {
            //Assert.Equal(Available, true);
            Assert.True(Available);
        }
       
    }
}