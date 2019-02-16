using System;
using BenchmarkDotNet.Attributes;
using static BenchmarkDotNet.Diagnosers.HardwareCounter;

namespace Benchmarks.Branching
{

    public class LargeObject
    {
        private readonly int[] _whatever;

        public int Value { get; }


        public LargeObject(int size)
        {
            _whatever = new int[size];

            var random = new Random();
            for (var i = 0; i < _whatever.Length; i++)
            {
                _whatever[i] = random.Next(100000);
            }


            Value = random.Next(10000);
        }
    }

    [HardwareCounters(BranchInstructions, BranchMispredictions, CacheMisses)]
    //[MemoryDiagnoser]
    //[InliningDiagnoser]
    [RankColumn]
    public class LargeObjectsCacheMisses
    {
        private readonly LargeObject[] _largeData = new LargeObject[2000];
        private readonly int[] _data = new int[2000];

        [Params(100, 1000, 10000)]
        public int LargeObjectSize { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random();
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = random.Next(100000);
                _largeData[i] = new LargeObject(LargeObjectSize);
            }
        }

        [Benchmark]
        public int LargeDataSum()
        {
            var sum = 0;

            for (var i = 0; i < _largeData.Length; i++)
            {
                sum += _largeData[i].Value;
            }

            return sum;
        }

        [Benchmark(Baseline = true)]
        public int ArraySum()
        {
            var sum = 0;

            for (var i = 0; i < _data.Length; i++)
            {
                sum += _data[i];
            }

            return sum;
        }
    }
}
