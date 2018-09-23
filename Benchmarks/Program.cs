using System;
using BenchmarkDotNet.Running;
using Benchmarks.Simd;
using Benchmarks.Tuples;

namespace Benchmarks
{
    class Program
    {
        static void Main()
        {

            //var summary = BenchmarkRunner.Run<MinMaxBenchmark>();
            var summary = BenchmarkRunner.Run<DeconstructDateTime>();
            //var summary = BenchmarkRunner.Run<MyMath>();

            Console.ReadKey();
        }
    }
}
