using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using static BenchmarkDotNet.Diagnosers.HardwareCounter;

namespace Benchmarks.Branching
{
    [HardwareCounters(BranchInstructions, BranchMispredictions, CacheMisses)]
    [InliningDiagnoser(true, true)]
    public class CacheInvalidation
    {
        private static readonly int NumberOfThreads = Environment.ProcessorCount;

        
        private int[] _data;

        [Params(/*10_000,*/ 100_000/*, 1_000_000*/)]
        public int Size { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _data = new int[Size];

            var random = new Random(42);
            for (var i = 0; i < Size; i++)
            {
                _data[i] = random.Next(10000);
            }
        }

        private long Sequential(int from, int to)
        {
            var sum = 0;

            for (var i = from; i < to; i++)
            {
                sum += _data[i];
            }

            return sum;
        }

        private void Sequential(int from, int to, out long sum)
        {
            sum = 0;

            for (var i = from; i < to; i++)
            {
                sum += _data[i];
            }
        }

        [Benchmark(Baseline = true)]
        public long Sequential()
        {
            return Sequential(0, Size);
        }

        [Benchmark]
        public long ParallelWithoutCacheInvalidation()
        {
            var tasks = new Task[NumberOfThreads];
            var results = new long[NumberOfThreads];

            var stepSize = Size / NumberOfThreads;


            for (var i = 0; i < NumberOfThreads; i++)
            {
                var index = i;
                var from = i * stepSize;
                var to = (i != NumberOfThreads - 1) ? from + stepSize : Size;
                tasks[i] = Task.Run(() => { results[index] = Sequential(from, to); });
            }

            Task.WhenAll(tasks).Wait();

            return results.Sum();
        }

        [Benchmark]
        public long ParallelWithCacheInvalidation()
        {
            var tasks = new Task[NumberOfThreads];
            var results = new long[NumberOfThreads];

            var stepSize = Size / NumberOfThreads;


            for (var i = 0; i < NumberOfThreads; i++)
            {
                var index = i;
                var from = i * stepSize;
                var to = (i != NumberOfThreads - 1) ? from + stepSize : Size;
                tasks[i] = Task.Run(() => { Sequential(from, to, out results[index]); });
            }

            Task.WhenAll(tasks).Wait();

            return results.Sum();
        }
    }
}
