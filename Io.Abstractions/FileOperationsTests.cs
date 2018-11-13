using NUnit.Framework;

namespace Io.Abstractions
{
    [TestFixture]
    public class FileOperationsTests
    {
        /// <summary>
        ///
        /// - slow to run
        /// - complicated setup or additional cleanup code needed
        /// - can it be run in parallel with other file related tests?
        /// - are paths on CI server the same as on local PC? 
        /// 
        /// </summary>
        [Test]
        public void TestMethod()
        {
            var operations = new FileOperations();

            var fullPath = operations.Save();
            var text = operations.Read();

            //???.ShouldBe(); //??? what should I test here???
        }
    }
}
