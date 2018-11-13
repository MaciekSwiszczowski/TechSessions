using System.IO;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;
using Shouldly;

namespace Io.Abstractions
{
    [TestFixture]
    public class FileOperationsAbstractedTests
    {
        [Test]
        public void TestMethod()
        {
            var mockedFileSystem = new MockFileSystem();
            //var operations = new FileOperationsAbstracted(mockedFileSystem);

            //var fullPath = operations.Save();
            //var textReadFromFile = operations.Read();


            //mockedFileSystem.AllPaths.ShouldContain(fullPath);

            //var mockedFile = mockedFileSystem.GetFile(fullPath);
            //textReadFromFile.ShouldBe(mockedFile.TextContents);

        }
    }
}
