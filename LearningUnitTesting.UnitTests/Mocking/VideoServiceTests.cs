using LearningUnitTesting.Mocking;
using Moq;
using NUnit.Framework;

namespace LearningUnitTesting.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _mockFileReader;

        [SetUp]
        public void Setup()
        {
            _mockFileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_mockFileReader.Object);

        }
        

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
                      
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns(""); //settting up mockFileReader object

            var result = _videoService.ReadVideoTitle();
            
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}