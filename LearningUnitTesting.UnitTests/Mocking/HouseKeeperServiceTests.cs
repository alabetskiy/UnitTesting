using LearningUnitTesting.Mocking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LearningUnitTesting.UnitTests.Mocking
{
    [TestFixture]
   public class HouseKeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate = new DateTime(2018, 06, 17);
        private Housekeeper _houseKeeper;
        private readonly string _statementFileName = "filename";

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            //First dependence for HouseKeeperService class
            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            //Second dependence for HouseKeeperService class
            _statementGenerator = new Mock<IStatementGenerator>();

            //Third dependence for HouseKeeperService class
            _emailSender = new Mock<IEmailSender>();

            //Fourth dependence for HouseKeeperService class
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object,
                _statementGenerator.Object, _emailSender.Object, _messageBox.Object);

        }


        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {

            //ACT
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));

        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatement()
        {
            _houseKeeper.Email = null;
            //ACT
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(
                _houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never); //Times.Never means that SaveStatement should never be called

        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhitespace_ShouldNotGenerateStatement()
        {
            _houseKeeper.Email = " ";
            //ACT
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(
                _houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never); //Times.Never means that SaveStatement should never be called

        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsEmptyString_ShouldNotGenerateStatement()
        {
            _houseKeeper.Email = "";
            //ACT
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(
                _houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never); //Times.Never means that SaveStatement should never be called

        }


        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(_statementFileName); 

            //ACT
            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(
                    _houseKeeper.Oid,
                    _houseKeeper.FullName,
                    _statementDate))
                .Returns(() => null);

            //ACT
            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsEmptyString_ShouldNotEmailTheStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(
                    _houseKeeper.Oid,
                    _houseKeeper.FullName,
                    _statementDate))
                .Returns("");

            //ACT
            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsWhiteSpace_ShouldNotEmailTheStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(
                    _houseKeeper.Oid,
                    _houseKeeper.FullName,
                    _statementDate))
                .Returns(" ");

            //ACT
            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(_statementFileName);

            _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>())).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));

        }
    }
}
