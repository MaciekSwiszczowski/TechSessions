using System;
using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Running;
using BenchmarksCore.GetHashCode;


namespace BenchmarksCore
{
    public class Program
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");


            //BenchmarkRunner.Run<HashCodes>();
            BenchmarkRunner.Run<HashCodesInAction>();


            Console.ReadKey();
        }
    }


}
