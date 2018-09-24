using System;
using Moq;
using NUnit.Framework;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable UnusedVariable
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable UnusedMember.Local
#pragma warning disable 219

namespace NUnitAndMoq
{
    public interface IData
    {
        int Get(int count);
        string Name { get; }
        int Age { get; }

        event EventHandler DataChanged;
    }

    /**********************************************************************************
    * 
    * 
    *            https://github.com/nunit/docs/wiki/NUnit-Documentation
    * 
    * 
    **********************************************************************************/


    [TestFixture]
    public class MoqExamples
    {

        [Test]
        public void Examples()
        {

            // 1. Creating instances
            Mock<IData> dataMock = new Mock<IData>();
            IData dataObject = dataMock.Object;

            // 2. Setting up properties and results
            dataMock.Setup(data => data.Age).Returns(140);

            dataMock
                .Setup(data => data.Get(It.IsAny<int>()))
                .Returns(3);

            //or
            dataMock
                .Setup(data => data.Get(It.IsAny<int>())) // 'It' has some other possibilities
                .Returns<int>(argument => argument * 2);

            //or
            dataMock.SetupSequence(data => data.Get(It.IsAny<int>()))
                .Returns(3) // will be returned on 1st invocation
                .Returns(2) // will be returned on 2nd invocation
                .Returns(1) // will be returned on 3rd invocation
                .Returns(0) // will be returned on 4th invocation
                .Throws(new InvalidOperationException()); // will be thrown on 5th invocation

            // 3. Raising events
            dataMock.Raise(view => view.DataChanged += null, this, EventArgs.Empty);

            // 4. Verifying that a method was invoked
            dataMock.Object.Get(1);
            dataMock.Verify(_ => _.Get(It.IsAny<int>()), Times.Once()); // 'Times' has some other possibilities

            // 5. Callback 
            bool getWasCalled = false;
            dataMock
                .Setup(data => data.Get(It.IsAny<int>()))
                .Callback<int>(_ => getWasCalled = true)
                .Returns(3);
        }




        private IData _data = Mock.Of<IData>();

        private readonly IData _otherData = Mock.Of<IData>(context =>
            context.Age == 140 &&
            context.Name == "Maciek");

        [Test]
        public void Examples2()
        {
            // what if I need to add a setup?
            Mock<IData> dataMock = Mock.Get(_otherData);
            
            dataMock.Setup(data => data.Age).Returns(100);
        }
    }
}
