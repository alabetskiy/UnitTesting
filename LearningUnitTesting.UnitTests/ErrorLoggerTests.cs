using System;
using LearningUnitTesting.Fundamentals;
using NUnit.Framework;
using Remotion.Linq.Clauses.ResultOperators;

namespace LearningUnitTesting.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();
            logger.Log("test");
            
            Assert.That(logger.LastError, Is.EqualTo("test"));
            
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullExeption(string error)
        {
            var logger = new ErrorLogger();
            
//       logger.Log(error); - this will throw me an exeption in test, so I need to use delegate. 
//        So I warp this call inside delegate
          
            Assert.That(()=> logger.Log(error), Throws.ArgumentNullException);
//          Assert.That(()=> logger.Log(error), Throws.TypeOf<>()); //I can use custom exeptions
        }

        [Test]
        public void Log_ValidError_RaiseErrorLogEvent()
        {
            var logger = new ErrorLogger();

            var id = Guid.Empty;

            logger.ErrorLogged += (sender, args) => { id = args; };
            
            logger.Log("a");
            
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}