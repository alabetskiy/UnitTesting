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
    }
}
