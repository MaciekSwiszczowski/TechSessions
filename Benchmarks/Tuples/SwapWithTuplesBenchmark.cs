using BenchmarkDotNet.Attributes;

namespace Benchmarks.Tuples
{

    [MemoryDiagnoser]
    public class SwapWithTuplesBenchmark
    {
        private int _x = 100000;
        private int _y = 123456;

        [Benchmark]
        public int SwapWithTuple()
        {
            (_x, _y) = (_y, _x);
            return _x + _y;
        }

        [Benchmark(Baseline = true)]
        public int TrivialSwap()
        {
            var temp = _x;
            _x = _y;
            _y = temp;
            return _x + _y;
        }

    }
}
