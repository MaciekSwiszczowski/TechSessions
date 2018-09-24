using System;
using System.Collections;
using NUnit.Framework;

namespace NUnitAndMoq
{
    /**********************************************************************************
     * 
     * 
     *           https://github.com/nunit/docs/wiki/NUnit-Documentation
     *           https://github.com/nunit/docs/wiki/Attributes
     * 
     * 
     **********************************************************************************/


    [TestFixture]
    public class ExampleTests
    {
        #region Values attribute examples

        [Test]
        public void ValuesExampleTest([Values(Switch.On, Switch.Off)] Switch isTrue)
        {
        }

        [Repeat(3)]
        [Timeout(1000 /*in milliseconds*/)]
        [Test, Sequential]
        public void SequentialExampleTest(
            [Values(1, 2, 3)] int x,
            [Values("A", "B")] string s)
        {
        }

        #endregion

        
        #region Range attribute example

        [Test]
        public void RangeExampleTest([Range(0.2, 0.6, 0.2)] double d)
        {
        }
        
        #endregion

        
        #region TestCase attribute examples

        [TestCase(12, 3, 4)]
        [TestCase(12, 2, 6)]
        [TestCase(12, 4, 3)]
        public void DivideTest(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }

        [TestCase(12, 3, 4, TestName = "That's my favorite test")]
        [TestCase(12, 2, 6, Description = "A test description you can ignore")]
        [TestCase(12, 4, 3, Explicit = true, Reason = "It takes too long to run it every time")]
        [TestCase(12, 5, 3, Ignore = "I'm not sure", IgnoreReason = "Dividing numbers is so difficult")]
        public void DivideTestExtended(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }


        // Not recommending, everybody expects a test to have an assertion
        [TestCase(12, 3, ExpectedResult = 4)]
        [TestCase(12, 2, ExpectedResult = 6)]
        [TestCase(12, 4, ExpectedResult = 3)]
        public int DivideTestWithExpectedResult(int n, int d)
        {
            return n / d;
        }

        #endregion


        #region TestCaseSource and TestCaseData

        // https://github.com/nunit/docs/wiki/TestCaseSource-Attribute
        // https://github.com/nunit/docs/wiki/TestCaseData

        [TestCaseSource(nameof(_divideCases))]
        public void DivideTestWithTestCaseSource(int n, int d, int q)
        {
            Assert.AreEqual(q, n / d);
        }

        private static object[] _divideCases =
        {
            new object[] { 12, 3, 4 },
            new object[] { 12, 2, 6 },
            new object[] { 12, 4, 3 }
        };



        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.TestCases))]
        public void DivideTestWithTestCaseData(int n, int d, int expected)
        {
            Assert.AreEqual(expected, n / d);
        }
        public class MyDataClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(12, 3, 4);
                    yield return new TestCaseData(12, 2, 6).SetName("! - {m}{a}"); // https://github.com/nunit/docs/wiki/Template-Based-Test-Naming
                    yield return new TestCaseData(12, 4, 3).Explicit("Takes too much time");
                    yield return new TestCaseData(12, 5, 3).Ignore("Ask Ian why it's not working");
                }
            }
        }

        // You can parametrize all test methods at once
        // https://github.com/nunit/docs/wiki/TestFixtureData
        
        
        #endregion


        #region Theories

        // https://github.com/nunit/docs/wiki/Theory-Attribute

        public class SqrtTests
        {
            [DatapointSource]
            public double[] Values = { 0.0, 1.0, -1.0, 42.0 };

            [Theory]
            public void SquareRootDefinition(double num)
            {
                Assume.That(num >= 0.0);

                var sqrt = Math.Sqrt(num);

                Assert.That(sqrt >= 0.0);
                Assert.That(sqrt * sqrt, Is.EqualTo(num).Within(0.000001));
            }
        }

        [Theory]
        public void SquareRootDefinition(Size size)
        {
        }



        #endregion
    }

}