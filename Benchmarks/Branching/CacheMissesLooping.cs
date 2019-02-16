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
    //[Orderer(SummaryOrderPolicy.SlowestToFastest)]
    public class CacheMissesLooping
    {
        private int[] _data;
        private int[] _rotated;

        [Params(/*5, 10,*/ 20 /*,100*/)]
        public int Step { get; set; }

        [Params(/*1000,*/ 2000)]
        public int Size { get; set; }


        [GlobalSetup]
        public void GlobalSetup()
        {
            _data = new int[Size * Size];
            _rotated = new int[Size * Size];

            var random = new Random();
            for (var i = 0; i < Size * Size; i++)
            {
                _data[i] = random.Next(100000);
            }
        }

        [Benchmark(Baseline = true)]
        public int[] RotateNaive()
        {
            for (var y = 0; y < Size; y++)
            for (var x = 0; x < Size; x++)
            {
                _rotated[x * Size + y] = _data[y * Size + x];
            }

            return _rotated;
        }


        [Benchmark]
        public int[] RotateBlocked()
        {
            for (var y = 0; y < Size; y += Step)
            for (var x = 0; x < Size; x += Step)
            for (var dy = 0; dy < Step; dy++)
            for (var dx = 0; dx < Step; dx++)
            {
                _rotated[(x + dx) * Size + (y + dy)] = _data[(y + dy) * Size + (x + dx)];
            }

            return _rotated;
        }

    }
}