using System;
using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Running;
using Benchmarks.Branching;
using Benchmarks.Simd;
using Benchmarks.Tuples;

namespace Benchmarks
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            //var summary = BenchmarkRunner.Run<MinMaxBenchmark>();
            //var summary = BenchmarkRunner.Run<DeconstructDateTime>();
            //var summary = BenchmarkRunner.Run<MyMath>();
            //var summary = BenchmarkRunner.Run<AvoidBranching>();



            var summary = BenchmarkRunner.Run<Abs>();
            //var a = new AvoidBranching();
            //a.Setup();
            //a.SumBiggerThan500Branched();

            Console.ReadKey();
        }
    }
}