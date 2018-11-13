using System;
using BenchmarkDotNet.Attributes;
using static BenchmarkDotNet.Diagnosers.HardwareCounter;

namespace Benchmarks.Branching
{
    [DisassemblyDiagnoser]
    [HardwareCounters(BranchInstructions, BranchMispredictions)]
    public class Abs
    {
        private readonly int[] _valuesA = new int[1000];

        public Abs()
        {
            var random = new Random();
            for (var i = 0; i < 1000; i++)
            {
                _valuesA[i] = int.MinValue + random.Next(int.MaxValue) * 2;
            }
        }

        [Benchmark]
        public long SumAbsWithMathLibrary()
        {
            var a = _valuesA;

            long sum = 0;

            for (var i = 0; i < 1000; i++)
            {
                sum += Math.Abs(a[i]);
            }

            return sum;
        }

        [Benchmark]
        public long SumAbsWithOperator()
        {
            var a = _valuesA;

            long sum = 0;

            for (var i = 0; i < 1000; i++)
            {
                sum += a[i] > 0 ? a[i] : -a[i];
            }

            return sum;
        }

        [Benchmark]
        public long SumAbsWithBitOperations()
        {
            var a = _valuesA;

            long sum = 0;

            for (var i = 0; i < 1000; i++)
            {
                int y = a[i] >> 31;
                sum += (a[i] ^ y) - y;
            }

            return sum;
        }


    }
}
