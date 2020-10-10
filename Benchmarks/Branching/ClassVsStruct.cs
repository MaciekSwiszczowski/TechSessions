using System;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Branching
{
    internal class ReferenceType
    {
        public int value;
    }

    internal struct ValueType
    {
        public int value;
    }

    internal struct ExtendedValueType
    {
        public int value;
#pragma warning disable 169
        private double _otherData;
#pragma warning restore 169
    }

    public class ClassVsStruct
    {
        private ReferenceType[] _referenceTypeData;
        private ReferenceType[] _referenceTypeData2;
        private ValueType[] _valueTypeData;
        private ExtendedValueType[] _extendedValueTypeData;
        private int[] _data;

        [Params(16 * 1_000)] public int Size { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _referenceTypeData = new ReferenceType[Size];
            _referenceTypeData2 = new ReferenceType[Size];
            _valueTypeData = new ValueType[Size];
            _extendedValueTypeData = new ExtendedValueType[Size];

            _data = new int[Size];

            var random = new Random();
            for (var i = 0; i < Size; i++)
            {
                _data[i] = random.Next(100000);
                _referenceTypeData[i] = new ReferenceType {value = _data[i]};
                _referenceTypeData2[i] = new ReferenceType {value = _data[i]};
                _valueTypeData[i] = new ValueType {value = _data[i]};
                _extendedValueTypeData[i] = new ExtendedValueType {value = _data[i]};
            }
        }

        [Benchmark(Baseline = true)]
        public int ReferenceTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _referenceTypeData[i].value;
            }

            return sum;
        }

        [Benchmark]
        public int ReferenceTypeSum2()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _referenceTypeData2[i].value;
            }

            return sum;
        }

        //[Benchmark]
        public int ValueTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _valueTypeData[i].value;
            }

            return sum;
        }

        //[Benchmark]
        public int ExtendedValueTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _extendedValueTypeData[i].value;
            }

            return sum;
        }


        //[Benchmark]
        public int ReferenceTypeOptimizedSum()
        {
            var sum = 0;
            var array = _referenceTypeData;

            for (var i = 0; i < array.Length; i++)
            {
                sum += array[i].value;
            }

            return sum;
        }

        //  [Benchmark]
        public int ValueTypeOptimizedSum()
        {
            var sum = 0;
            var array = _valueTypeData;

            for (var i = 0; i < array.Length; i++)
            {
                sum += array[i].value;
            }

            return sum;
        }

        //    [Benchmark]
        public int ExtendedValueTypeOptimizedSum()
        {
            var sum = 0;
            var array = _extendedValueTypeData;

            for (var i = 0; i < array.Length; i++)
            {
                sum += array[i].value;
            }

            return sum;
        }


        //      [Benchmark]
        public int ReferenceTypeUnrolledSum()
        {
            var sum = 0;
            var array = _referenceTypeData;

            for (var i = 0; i < array.Length; i += 16)
            {
                sum += array[i].value;
                sum += array[i + 1].value;
                sum += array[i + 2].value;
                sum += array[i + 3].value;
                sum += array[i + 4].value;
                sum += array[i + 5].value;
                sum += array[i + 6].value;
                sum += array[i + 7].value;
                sum += array[i + 8].value;
                sum += array[i + 9].value;
                sum += array[i + 10].value;
                sum += array[i + 11].value;
                sum += array[i + 12].value;
                sum += array[i + 13].value;
                sum += array[i + 14].value;
                sum += array[i + 15].value;
            }

            return sum;
        }

//        [Benchmark]
        public int ReferenceTypeUnrolledNestedSum()
        {
            var sum = 0;
            var array = _referenceTypeData;

            for (var i = 0; i < array.Length; i += 16)
            {
                for (var j = 0; j < 16; j++)
                {
                    sum += array[i + j].value;
                }
            }

            return sum;
        }

        //      [Benchmark]
        public int ValueTypeUnrolledSum()
        {
            var sum = 0;
            var array = _valueTypeData;

            for (var i = 0; i < array.Length; i += 16)
            {
                sum += array[i].value;
                sum += array[i + 1].value;
                sum += array[i + 2].value;
                sum += array[i + 3].value;
                sum += array[i + 4].value;
                sum += array[i + 5].value;
                sum += array[i + 6].value;
                sum += array[i + 7].value;
                sum += array[i + 8].value;
                sum += array[i + 9].value;
                sum += array[i + 10].value;
                sum += array[i + 11].value;
                sum += array[i + 12].value;
                sum += array[i + 13].value;
                sum += array[i + 14].value;
                sum += array[i + 15].value;
            }

            return sum;
        }

//        [Benchmark]
        public int ExtendedValueTypeUnrolledSum()
        {
            var sum = 0;
            var array = _extendedValueTypeData;

            for (var i = 0; i < array.Length; i += 16)
            {
                sum += array[i].value;
                sum += array[i + 1].value;
                sum += array[i + 2].value;
                sum += array[i + 3].value;
                sum += array[i + 4].value;
                sum += array[i + 5].value;
                sum += array[i + 6].value;
                sum += array[i + 7].value;
                sum += array[i + 8].value;
                sum += array[i + 9].value;
                sum += array[i + 10].value;
                sum += array[i + 11].value;
                sum += array[i + 12].value;
                sum += array[i + 13].value;
                sum += array[i + 14].value;
                sum += array[i + 15].value;
            }

            return sum;
        }
    }
}