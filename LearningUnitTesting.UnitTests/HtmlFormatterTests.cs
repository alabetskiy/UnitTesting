using System.Runtime.Serialization;
using LearningUnitTesting.Fundamentals;
using NUnit.Framework;

namespace LearningUnitTesting.UnitTests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ReturnsEnclosedStringWithStrongTag()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("abc");
            
            //Specific 
            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);
            
            //More general. For testing stings it is better to have more general assertions. 
            Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
            Assert.That(result, Does.Contain("abc"));
            
        }
    }
}