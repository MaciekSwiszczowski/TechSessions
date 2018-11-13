using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using static BenchmarkDotNet.Diagnosers.HardwareCounter;

namespace Benchmarks.Branching
{
    [HardwareCounters(BranchInstructions, BranchMispredictions)]
    public class AvoidBranching
    {
        private readonly List<int> _data = new List<int>();
        private readonly List<int> _dataSorted = new List<int>();

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(42);
            for (var i = 0; i < 1000; ++i)
            {
                _data.Add(random.Next(100000));
                _dataSorted.Add(_data[i]) ;
            }

            _dataSorted.Sort();
        }

        [Benchmark]
        public int SumBiggerThan50000BranchedUnsorted()
        {
            var result = 0;

            for (var i = 0; i < _data.Count; ++i)
            {
                if (_data[i] > 50000)
                    result += i;
            }

            return result;
        }

        [Benchmark(Baseline = true)]
        public int SumBiggerThan50000Sorted()
        {
            var result = 0;
           
            for (var i = 0; i < _dataSorted.Count; ++i)
            {
                if (_dataSorted[i] > 50000)
                    result += i;
            }

            return result;
        }

    }
}
