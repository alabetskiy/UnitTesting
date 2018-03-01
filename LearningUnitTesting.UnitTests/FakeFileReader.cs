using LearningUnitTesting.Mocking;

namespace LearningUnitTesting.UnitTests
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return ""; //returning fake result instead of accessing file system
        }
    }
}