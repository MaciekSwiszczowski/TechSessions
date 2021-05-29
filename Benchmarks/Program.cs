using System;
using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Running;
using Benchmarks.MulticastDelegate;

namespace Benchmarks
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            BenchmarkRunner.Run<HashSetAccess>();



            Console.ReadKey();
        }
    }
}