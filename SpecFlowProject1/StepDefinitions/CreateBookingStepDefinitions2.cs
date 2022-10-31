using HotelBooking.Core;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class CreateBookingStepDefinitions2
    {
        DateTime startDate, endDate;
        bool Available;
        private readonly IBookingManager _bookingManager;

        public CreateBookingStepDefinitions2(IBookingManager manager)
        {
            _bookingManager = manager;

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