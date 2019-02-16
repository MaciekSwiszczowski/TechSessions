using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using static BenchmarkDotNet.Diagnosers.HardwareCounter;

namespace Benchmarks.Branching
{
    [HardwareCounters(BranchInstructions, BranchMispredictions, CacheMisses)]
    //[MemoryDiagnoser]
    //[InliningDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.SlowestToFastest)]
    public class Min
    {
        private readonly int[] _valuesA = new int[1000];
        private readonly int[] _valuesB = new int[1000];

        public Min()
        {
            var random = new Random();
            for (var i = 0; i < 1000; i++)
            {
                _valuesA[i] = int.MinValue + random.Next(int.MaxValue) * 2;
                _valuesB[i] = int.MinValue + random.Next(int.MaxValue) * 2;
            }
        }

        [Benchmark]
        public long SumMinsWithMathLibrary()
        {
            var a = _valuesA;
            var b = _valuesB;

            long sum = 0;

            for (var i = 0; i < 1000; i++)
            {
                sum += Math.Min(a[i], b[i]);
            }

            return sum;
        }

        [Benchmark]
        public long SumMinsWithOperator()
        {
            var a = _valuesA;
            var b = _valuesB;

            long sum = 0;

            for (var i = 0; i < 1000; i++)
            {
                sum += a[i] < b[i] ? a[i] : b[i];
            }

            return sum;
        }

        [Benchmark]
        public long SumMinsWithBitOperations()
        {
            var a = _valuesA;
            var b = _valuesB;

            long sum = 0;

            for (var i = 0; i < 1000; i++)
            {
                sum += a[i] & ((a[i] - b[i]) >> 31) | b[i] & (~(a[i] - b[i]) >> 31);
            }

            return sum;
        }


    }
}
