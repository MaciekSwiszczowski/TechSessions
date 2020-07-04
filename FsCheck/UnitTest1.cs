using System.Globalization;
using System.Threading;
using FsCheck;
using NUnit.Framework;

namespace FsCheckExamples
{
    [TestFixture]
    public class UnitTest1
    {

        [SetUp]
        public void RunBeforeAnyTests()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("En-Us");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("En-Us");

            Gen.Choose(1, 44);
        }


        //[FsCheck.NUnit.Property(Verbose = true)]
        //[Test]
        //public void TestMethod1()
        //{
        //    Func<int[], bool> revRevIsOrig = xs => true;
        //    Prop.ForAll(revRevIsOrig).QuickCheckThrowOnFailure();
        //}


        [Test]
        public void Test()
        {

        }
    }



    //[SetUpFixture]
    //public class MySetUpClass
    //{
    //    public MySetUpClass()
    //    {
    //        Thread.CurrentThread.CurrentCulture = new CultureInfo("En-Us");
    //        Thread.CurrentThread.CurrentUICulture = new CultureInfo("En-Us");
    //    }
    //}
}
