using NUnit.Framework;

namespace Io.Abstractions
{
    [TestFixture]
    public class FileOperationsTests
    {
        [Test]
        public void TestMethod()
        {
            var operations = new FileOperations();

            operations.Save();
            var text = operations.Read();

        
            //???.ShouldBe(); //???

        }
    }
}
