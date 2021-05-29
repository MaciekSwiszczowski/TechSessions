using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class HashSetAccess
    {
        [Params(30, 60)] public int Size { get; set; }
        [Params(0, 1, 2)] public int ChosenSize { get; set; }

        private Dummy[] _values;
        private readonly List<Dummy> _chosenOnesList = new();
        private readonly List<int> _chosenOnesListOfHashes = new();
        private readonly HashSet<Dummy> _chosenOnesHashSet = new();
        private readonly HashSet<int> _chosenOnesHashSetOfHashes = new();

        [GlobalSetup]
        public void GlobalSetup()
        {
            var rand = new Random(12321);

            _values = new Dummy[Size];
            for (var index = 0; index < _values.Length; index++)
            {
                _values[index] = new Dummy
                {
                    Value = rand.Next(10000)
                };
            }

            for (var chosenSize = 0; chosenSize < ChosenSize; chosenSize++)
            {
                var index = rand.Next(Size);
                var chosen = _values[index];

                _chosenOnesList.Add(chosen);
                _chosenOnesListOfHashes.Add(chosen.GetHashCode());
                _chosenOnesHashSet.Add(chosen);
                _chosenOnesHashSetOfHashes.Add(chosen.GetHashCode());
            }
        }

        [Benchmark(Baseline = true)]
        public int ListTest()
        {
            var counter = 0;
            var length = _values.Length;

            for (var index = 0; index < length; index++)
            {
                var value = _values[index];
                if (_chosenOnesList.Contains(value))
                    counter += value.Value;
            }

            return counter;
        }

        [Benchmark]
        public int ListTestWithSizeCheck()
        {
            var counter = 0;
            var length = _values.Length;

            if (_chosenOnesList.Count == 0)
                return 0;

            for (var index = 0; index < length; index++)
            {
                var value = _values[index];
                if (_chosenOnesList.Contains(value))
                    counter += value.Value;
            }

            return counter;
        }

        [Benchmark]
        public int HashTest_Contains()
        {
            var counter = 0;
            var length = _values.Length;

            for (var index = 0; index < length; index++)
            {
                var value = _values[index];
                if (_chosenOnesHashSet.Contains(value))
                    counter += value.Value;
            }

            return counter;
        }

        [Benchmark]
        public int HashTest_TryGetValue()
        {
            var counter = 0;
            var length = _values.Length;

            for (var index = 0; index < length; index++)
            {
                var value = _values[index];
                if (_chosenOnesHashSet.TryGetValue(value, out _))
                    counter += value.Value;
            }

            return counter;
        }

        [Benchmark]
        public int ListOfHashes()
        {
            var counter = 0;
            var length = _values.Length;

            for (var index = 0; index < length; index++)
            {
                var value = _values[index];
                if (_chosenOnesListOfHashes.Contains(value.GetHashCode()))
                    counter += value.Value;
            }

            return counter;
        }

        [Benchmark]
        public int HashSetOfHashes()
        {
            var counter = 0;
            var length = _values.Length;

            for (var index = 0; index < length; index++)
            {
                var value = _values[index];
                if (_chosenOnesHashSetOfHashes.Contains(value.GetHashCode()))
                    counter += value.Value;
            }

            return counter;
        }

        [Benchmark]
        public int HashSetOfHashesWithSizeCheck()
        {
            var counter = 0;
            var length = _values.Length;

            if (_chosenOnesHashSetOfHashes.Count == 0)
                return 0;

            for (var index = 0; index < length; index++)
            {
                var value = _values[index];
                if (_chosenOnesHashSetOfHashes.Contains(value.GetHashCode()))
                    counter += value.Value;
            }

            return counter;
        }
    }


    public class Dummy
    {
        public int Value { get; set; }
    }
}