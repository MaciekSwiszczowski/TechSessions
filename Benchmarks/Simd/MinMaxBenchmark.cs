using System;
using BenchmarkDotNet.Attributes;
using Simd;

namespace Benchmarks.Simd
{
    //
    //                   x64 ONLY!!!
    //

    // haven't found any good MS docs

    // https://instil.co/2016/03/21/parallelism-on-a-single-core-simd-with-c/
    // a workshop: https://github.com/goldshtn/simd-workshop

    // not all operations implemented efficiently!
    // issues in debug mode not present in release. (debugging with Console.WriteLine may be hard) 

    // for float exp, log and pow: https://github.com/mjmckp/VectorMathFuns/blob/master/Program.cs

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
