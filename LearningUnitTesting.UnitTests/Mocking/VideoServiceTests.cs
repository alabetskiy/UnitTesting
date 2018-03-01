using LearningUnitTesting.Mocking;
using NUnit.Framework;

namespace LearningUnitTesting.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var service = new VideoService();

            var result = service.ReadVideoTitle(new FakeFileReader());
            
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}