namespace Benchmarks.MemoryAlignment
{
    using System;
    using BenchmarkDotNet.Attributes;

    // Must run as AnyCPU!

    [LegacyJitX86Job]
    public class TwoArraysExample
    {

        private int[] _firstData;
        private int[] _secondData;
        private int[] _thirdData;
        private short _layoutChange = 0;
        private int[] _fourthData;

        [Params(1000, 32 * 100, 16 * 1000, 10_001)]
        public int Size { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _firstData = new int[Size];
            _secondData = new int[Size];
            _thirdData = new int[Size];
            _fourthData = new int[Size];

            var random = new Random();
            for (var i = 0; i < Size; i++)
            {
                var value = random.Next(100000);
                _firstData[i] = value;
                _secondData[i] = value;
                _thirdData[i] = value;
                _fourthData[i] = value;
            }
        }

        [Benchmark(Baseline = true)]
        public int FirstArraySum()
        {
            return Sum(_firstData);
        }

        [Benchmark]
        public int SecondArraySum()
        {
            return Sum(_secondData);
        }

        [Benchmark]
        public int ThirdArraySum()
        {
            return Sum(_thirdData);
        }

        [Benchmark]
        public int FourthArraySum()
        {
            return Sum(_fourthData);
        }

        private int Sum(int[] array)
        {
            var sum = 0;
            var data = array;

            for (var i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }

            return sum;
        }
    }

}