using System;
using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Running;
using Benchmarks.Branching;
using Benchmarks.MemoryAlignment;
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

            //BenchmarkRunner.Run<MinMaxBenchmark>();
            //BenchmarkRunner.Run<DeconstructDateTime>();
            //BenchmarkRunner.Run<MyMath>();
            //BenchmarkRunner.Run<AvoidBranching>();

           //BenchmarkRunner.Run<Min>();
           //BenchmarkRunner.Run<CacheMissesLooping>();
            

           //BenchmarkRunner.Run<LargeObjectsCacheMisses>();
           //BenchmarkRunner.Run<ClassVsStruct>();
           //BenchmarkRunner.Run<CacheInvalidation>();
           BenchmarkRunner.Run<TwoArraysExample>();
           //BenchmarkRunner.Run<SecondTwoArraysExample>();



           Console.ReadKey();
        }
    }
}