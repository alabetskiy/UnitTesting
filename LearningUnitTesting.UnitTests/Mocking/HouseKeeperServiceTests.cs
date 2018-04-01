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
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            //First dependence for HouseKeeperService class
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" }
            }.AsQueryable());

            //Second dependence for HouseKeeperService class
            var statementGenerator = new Mock<IStatementGenerator>();

            //Third dependence for HouseKeeperService class
            var emailSender = new Mock<IEmailSender>();

            //Fourth dependence for HouseKeeperService class
            var messageBox = new Mock<IXtraMessageBox>();

            var service = new HousekeeperService(unitOfWork.Object,
                statementGenerator.Object, emailSender.Object, messageBox.Object);

            //ACT
            service.SendStatementEmails(new DateTime(2017, 1, 1));

            statementGenerator.Verify(sg => sg.SaveStatement(1, "b", (new DateTime(2017, 1, 1))));

        }
    }
}
