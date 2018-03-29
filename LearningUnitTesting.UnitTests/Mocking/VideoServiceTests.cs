using LearningUnitTesting.Mocking;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace LearningUnitTesting.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _mockFileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_mockFileReader.Object, _repository.Object);

        }
        

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
                      
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns(""); //settting up mockFileReader object

            var result = _videoService.ReadVideoTitle();
            
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_NoUnprocessedVideos_ReturnEmpyString()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideos_ReturnStringWithIdOfUnprocessedVideos()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
              new Video { Id = 1 },
              new Video { Id = 2 },
              new Video { Id = 3 }
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}