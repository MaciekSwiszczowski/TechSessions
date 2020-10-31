using System.Buffers;
using BenchmarkDotNet.Attributes;
using Microsoft.Toolkit.HighPerformance.Buffers;

namespace Benchmarks.Pooling
{
    /// <summary>
    /// See: https://adamsitnik.com/Array-Pool/
    /// </summary>
    [MemoryDiagnoser]
    public class ArrayPoolBenchmark
    {
        [Params(100, 1000)] public int SizeInBytes { get; set; }

        [Params(1000)] public int Repetitions { get; set; }

        [Benchmark(Baseline = true)]
        public int Allocate()
        {
            var sum = 0;

            var repetitions = Repetitions;
            for (var i = 0; i < repetitions; i++)
            {
                var buffer = new byte[SizeInBytes];
                sum += buffer.Length;
            }

            return sum;
        }

        [Benchmark]
        public int RentAndReturn_Shared()
        {
            var sum = 0;

            var repetitions = Repetitions;
            for (var i = 0; i < repetitions; i++)
            {
                var array = ArrayPool<byte>.Shared.Rent(SizeInBytes);
                try
                {
                    sum += array.Length;
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(array);
                }
            }

            return sum;
        }

        [Benchmark]
        public int SpanOwner()
        {
            var sum = 0;
            var repetitions = Repetitions;
            for (var i = 0; i < repetitions; i++)
            {
                using var buffer = SpanOwner<byte>.Allocate(SizeInBytes, AllocationMode.Default);
                sum += buffer.Length;
            }

            return sum;
        }

        //[Benchmark]
        public int MemoryOwner()
        {
            var sum = 0;
            var repetitions = Repetitions;
            for (var i = 0; i < repetitions; i++)
            {
                using var buffer = MemoryOwner<byte>.Allocate(SizeInBytes, AllocationMode.Default);
                sum += buffer.Length;
            }

            return sum;
        }

    }
}