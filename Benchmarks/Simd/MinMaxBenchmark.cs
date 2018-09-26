using System;
using BenchmarkDotNet.Attributes;
using Simd;

namespace Benchmarks.Simd
{
    // https://instil.co/2016/03/21/parallelism-on-a-single-core-simd-with-c/
    // a workshop: https://github.com/goldshtn/simd-workshop
    // https://app.pluralsight.com/library/courses/making-dotnet-applications-even-faster/table-of-contents

    // for exp, log and pow with floats: https://github.com/mjmckp/VectorMathFuns/blob/master/Program.cs

    [MemoryDiagnoser]
    public class MinMaxBenchmark
    {
        private readonly int[] _data = new int[8 * 1000];

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(42);
            for (var i = 0; i < _data.Length; ++i)
                _data[i] = random.Next(1000);

            _data[5] = 1111;
            _data[6] = -1111;
        }

        [Benchmark]
        public (int min, int max) MinMaxNaive()
        {
            var max = int.MinValue;
            var min = int.MaxValue;

            var dataLength = _data.Length;
            for (var index = 0; index < dataLength; index++)
            {
                min = Math.Min(min, _data[index]);
                max = Math.Max(max, _data[index]);
            }

            return (min, max);
        }

        [Benchmark(Baseline = true)]
        public (int min, int max) MinMaxSimd()
        {
            return MinMax.MinMaxSimd(_data);
        }



        
    }
}
