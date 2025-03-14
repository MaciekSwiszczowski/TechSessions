using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmark48
{
    [MemoryDiagnoser]
    public class NestedAsync
    {
        private readonly int[] _array = new int[100];

        [GlobalSetup]
        public void Setup()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                _array[i] = i;
            }
        }

        [Benchmark(Baseline = true)]
        public int SyncSum()
        {
            var sum = 0;
            for (var i = 0; i < _array.Length; i++)
            {
                sum += _array[i];
            }
            return sum;
        }

        [Benchmark]
        public async Task<int> AsyncSum()
        {
            var sum = 0;
            for (var i = 0; i < _array.Length; i++)
            {
                sum += _array[i];
            }
            return sum;
        }

        [Benchmark]
        public Task<int> ReturnAsyncSum()
        {
            return AsyncSum();
        }

        [Benchmark]
        public async Task<int> AwaitAsyncSum()
        {
            return await AsyncSum();
        }
    }
} 