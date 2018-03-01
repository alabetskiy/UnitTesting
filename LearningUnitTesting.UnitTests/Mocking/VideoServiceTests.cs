using LearningUnitTesting.Mocking;
using NUnit.Framework;

namespace LearningUnitTesting.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var service = new VideoService();
            
            service._fileReader = new FakeFileReader();

            var result = service.ReadVideoTitle();
            
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}