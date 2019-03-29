using LightBDD.Core.Configuration;
using LightBDD.Core.Formatting.Values;
using LightBDD.Framework.Configuration;
using LightBDD.Framework.Reporting.Formatters;
using LightBDD.MsTest2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightBDD
{
    [TestClass]
    public class LightBddIntegration
    {
        [AssemblyInitialize] public static void Setup(TestContext testContext) { LightBddScope.Initialize(OnConfigure); }
        [AssemblyCleanup] public static void Cleanup() { LightBddScope.Cleanup(); }

        private static void OnConfigure(LightBddConfiguration configuration)
        {
            // some example customization of report writers
            configuration
                .ReportWritersConfiguration()
                .Clear()
                .AddFileWriter<HtmlReportFormatter>("FeaturesReport.html")
                ;

            configuration
                .ValueFormattingConfiguration()
                .RegisterExplicit(typeof(Asset), new AssetFormatter())
                ;
        }
    }

    internal class AssetFormatter : IValueFormatter
    {
        public string FormatValue(object value, IValueFormattingService formattingService)
        {
            var asset = (Asset) value;
            return $"Asset. Name: {asset.Name}, amount: {asset.Amount}";
        }
    }
}