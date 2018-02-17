using LearningUnitTesting.Fundamentals;
using NUnit.Framework;

namespace LearningUnitTesting.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        
        //SetUp - Test will run this method every time before execiting a new test method
        //TearDown - Test will run this method every time after execiting a new test method. Often use with integration test.
        private Math _math;
        
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
           
        }
        
        [Test]
        public void Add_WhenCalled_ReturnsTheSumofAgruments()
        {

            var result = _math.Add(1, 2);
            
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(1,2,2)]
        [TestCase(2,1,2)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnsGreaterArgument(int a, int b, int expectedResult)
        {

            var result = _math.Max(a, b);
            
            Assert.That(result,Is.EqualTo(expectedResult));
        }
        
//        [Test]
//        public void Max_SecondArgumentIsGeater_ReturnsSecondArgument()
//        {
//
//            var result = _math.Max(1, 2);
//            
//            Assert.That(result,Is.EqualTo(2));
//        }
//        
//        [Test]
//        public void Max_ArgumentsAreEqual_ReturnsSameArgument()
//        {
//
//            var result = _math.Max(1, 1);
//            
//            Assert.That(result,Is.EqualTo(1));
//        }
    }
}