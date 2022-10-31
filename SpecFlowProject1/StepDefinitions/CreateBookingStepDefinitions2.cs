using HotelBooking.Core;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class CreateBookingStepDefinitions2
    {
        DateTime StartDate, EndDate;
        bool Available;
        private readonly IBookingManager _bookingManager;

        public CreateBookingStepDefinitions2(IBookingManager manager)
        {
            _bookingManager = manager;

        }

        [Given(@"i have entered a (.*)")]
        public void GivenIHaveEnteredA(string startDate)
        {
            
            //StartDate = startDate;
            StartDate = DateTime.Parse(startDate);

        }

        [Given(@"i have also entered a (.*)")]
        public void GivenIHaveAlsoEnteredA(string endDate)
        {

            //EndDate = endDate;
            EndDate = DateTime.Parse(endDate);

        }

        
        [When(@"i press book")]
        public void WhenIPressBook()
        {

           Available = _bookingManager.CreateBooking(new Booking() { Id = 1, StartDate = StartDate , EndDate = EndDate, IsActive = true });
        }

        [Then(@"the booking should succeed or fail(.*)")]
        public void ThenTheBookingShouldSucceedOrFail(bool available)
        {
            Assert.Equal(Available, available);
        }
       
    }
}