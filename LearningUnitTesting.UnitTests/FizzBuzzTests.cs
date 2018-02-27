using LearningUnitTesting.Fundamentals;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NUnit.Framework;

namespace LearningUnitTesting.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_DevidedByThreeAndFive_ReturnFizzBuzz()
        {

            var result = FizzBuzz.GetOutput(15);
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_DevidedByThree_ReturnFizz()
        {

            var result = FizzBuzz.GetOutput(9);
            Assert.That(result, Is.EqualTo("Fizz"));
        }
        
        [Test]
        public void GetOutput_DevidedByFive_ReturnBuzz()
        {

            var result = FizzBuzz.GetOutput(10);
            Assert.That(result, Is.EqualTo("Buzz"));
        }
        
        [Test]
        public void GetOutput_DevidedAnyNumber_ReturnTheSameNumber()
        {

            var result = FizzBuzz.GetOutput(11);
            Assert.That(result, Is.EqualTo("11"));
        }
        
        

    }
}