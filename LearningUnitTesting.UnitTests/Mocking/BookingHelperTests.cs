using LearningUnitTesting.Mocking;
using Moq;
using System.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningUnitTesting.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking()
        {
            //Arrange
            var repository = new Mock<IBookingRepository>();
            repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking> {
                new Booking
                {
                    Id = 2,
                    ArrivalDate =  new DateTime(2017, 1, 15, 14, 0, 0 ),
                    DepartureDate = new DateTime(2017, 1, 20, 10, 0, 0),
                    Reference = "a"
                }

            }.AsQueryable());

            //Act
           var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = new DateTime(2017, 1, 10, 14, 0, 0),
                    DepartureDate = new DateTime(2017, 1, 14, 10, 0, 0),

                }, repository.Object);

            Assert.That(result, Is.Empty);
        }
    }
}
