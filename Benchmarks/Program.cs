using System;
using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Running;
using Benchmarks.Async;
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
           BenchmarkRunner.Run<SwapWithTuplesBenchmark>();
           //BenchmarkRunner.Run<SecondTwoArraysExample>();



           Console.ReadKey();
        }
    }
}