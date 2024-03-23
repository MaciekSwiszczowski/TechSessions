using System;
using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            //BenchmarkRunner.Run<HashSetAccess>();
            var summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();


            Console.ReadKey();
        }
    }
}