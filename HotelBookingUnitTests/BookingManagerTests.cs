using HotelBooking.Core;
using HotelBookingUnitTests.Fakes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingUnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;
        private readonly Mock<IRepository<Booking>> _mockBookingRepo;
        private readonly Mock<IRepository<Room>> _mockRoomRepo;

        public BookingManagerTests()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
            _mockRoomRepo = new Mock<IRepository<Room>>();
            _mockBookingRepo = new Mock<IRepository<Booking>>();
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            // Arrange
            DateTime date = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(date, date);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void FindAvailableRoom_EndDateBeforeStartDate_ThrowsArgumentException()
        {
            //Arrange
            var bookingManager = new BookingManager(_mockBookingRepo.Object, _mockRoomRepo.Object);

            //Act
            Action act = () => bookingManager.FindAvailableRoom(DateTime.Today.AddDays(12), DateTime.Today.AddDays(10));

            //Assert
            Assert.Throws<ArgumentException>(act);


        }


        [Fact]
        public void GetFullyOccupiedDates_StartDayAdd14EndDateAdd18_ReturnsEmptyList()
        {
            //Arrange
            var bookingManager = new BookingManager(_mockBookingRepo.Object, _mockRoomRepo.Object);
            DateTime startDate = DateTime.Today.AddDays(14);
            DateTime endDate = DateTime.Today.AddDays(18);

            //Act
            List<DateTime> fullyOccupiedDates = bookingManager.GetFullyOccupiedDates(startDate, endDate);

            //Asert
            Assert.Empty(fullyOccupiedDates);


        }


        [Fact]
        public void FullyOccupiedDates_StartDateIsLaterThanEndDate_ThrowArgumentException()
        {
            //Arrange
            var bookingManager = new BookingManager(_mockBookingRepo.Object, _mockRoomRepo.Object);
            DateTime startDate = DateTime.Today.AddDays(5);
            DateTime endDate = DateTime.Today.AddDays(2);
            Action act = () => bookingManager.GetFullyOccupiedDates(startDate, endDate);
            //Act
            var rec = Record.Exception(act);
            //Assert
            Assert.IsType<ArgumentException>(rec);
        }


        [Fact]
        public void CreateBooking_IncorrectStartDate_ReturnFalse()
        {
            //Arrange
            Booking newBooking = new()
            {
                RoomId = 3,
                StartDate = DateTime.Today.AddDays(14),
                EndDate = DateTime.Today.AddDays(14),
                IsActive = false,
                CustomerId = 2,
                Customer =
                new Customer() { Email = "dggg@mail", Id = 1, Name = "Everett" },
                Room = new Room() { Description = "Comfrtoab le" }
            };

            //Act
            bool isCreated = bookingManager.CreateBooking(newBooking);

            //Assert
            Assert.False(isCreated);

        }
        [Fact]
        public void CreateBooking_CorrectStartDate_ReturnTrue()
        {
            //Arrange
            Booking newBooking = new()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(2)
            };

            //Act
            bool isCreated = bookingManager.CreateBooking(newBooking);

            //assert
            Assert.True(isCreated);



        }
        [Theory]
        [InlineData(2, 1)]
        [InlineData(-1, 3)]
        public void FindAvailableRoom_RoomNotAvailable_InvalidInputData(int startDate, int endDate)
        {
            // Arrange
            var bookingManager = new BookingManager(_mockBookingRepo.Object, _mockRoomRepo.Object);
            // Act
            Action act = () => bookingManager.FindAvailableRoom(DateTime.Today.AddDays(startDate), DateTime.Today.AddDays(endDate));
            // Assert
            Assert.Throws<ArgumentException>(act);
        }

    }
}
