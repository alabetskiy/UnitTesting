using LearningUnitTesting.Mocking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LearningUnitTesting.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
       public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            _fileDownloader.Setup(fd => fd.DownloadFile("http://example.com/customer/installer", null)).Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);

        }

        [Test]
        public void DownloadInstaller_DownloadComplete_ReturnTrue()
        {

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);

        }

    }
}
