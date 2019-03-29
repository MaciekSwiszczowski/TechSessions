using System;
using System.Collections.Generic;
using LightBDD.Framework.Scenarios;
using LightBDD.MsTest2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightBDD.Features
{
    [TestClass]
    public partial class ExampleTestsWithLightBdd
    {
        [Scenario]
        [DataRow(2)]
        public void ParameterNamesExample(int amount)
        {
            Runner.RunScenario(
                given => Stack_Is_Created(),
                when => I_Add_Count_Elements(amount),
                then => Stack_Should_Contain_COUNT_Elements(amount),
                and => Stack_Should_Contain_Elements(amount)
                );
        }

        public static IEnumerable<object[]> Assets { get; } = new []
        {
            new[] { new Asset { Amount = 3.142592, CreatedOn = DateTime.Now, Name = "ether" } },
            new[] { new Asset { Amount = Math.PI, CreatedOn = DateTime.MinValue, Name = "vacuum" } }
        };

        [Scenario]
        [DynamicData(nameof(Assets))]
        public void TestParameterFormattersExample(Asset asset)
        {
            Runner.RunScenario(
                given => Stack_Is_Created(),
                when => I_Add_One_ASSET(asset)

            );
        }



        //[Scenario]
        //[DynamicData(nameof(Assets))]
        //public void TestParameterFormattersExample(
        //    [FormatCollection(valueFormat: "[{0}]")]
        //    [FormatBoolean("yes", "no")]
        //    [Format("{0:£0.00#}", SupportedType = typeof(Asset))]

        //    Asset asset
        //)
        //{
        //    Runner.RunScenario(
        //        given => Stack_Is_Created(),
        //        when => I_Add_One_ASSET(asset)

        //    );
        //}




        //[Scenario]
        //[DataRow(2)]
        //[DataRow(5)]
        //public void FormattingTestParameters(int amount)
        //{
        //    Runner.RunScenario(
        //        given => Stack_Is_Created(),
        //        when => I_Add_Count_Elements(amount),
        //        then => Stack_Should_Contain_COUNT_Elements(amount)
        //    );
        //}

    }

    
}
