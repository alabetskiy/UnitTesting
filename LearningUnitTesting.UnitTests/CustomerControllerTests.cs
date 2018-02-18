using LearningUnitTesting.Fundamentals;
using NUnit.Framework;

namespace LearningUnitTesting.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {

        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(0); 
            
            //NotFound object
            Assert.That(result, Is.TypeOf<NotFound>()); //Use this most of the time. TypeOf<Object> ensure that it is exactly this object. 
            
            //NotFound object or one of its derivatives
            // Assert.That(result, Is.InstanceOf<NotFound>()); //Result can be NotFound object or on of its derivatives
        }
        
        [Test]
        public void GetCustomer_IdIsNotZero_ReturnsOk()
        {
            
        }
    
    }
}