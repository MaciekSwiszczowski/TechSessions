using System;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Branching
{

    internal class ReferenceType
    {
        public int Value;
    }

    internal struct ValueType
    {
        public int Value;
    }

    internal struct ExtendedValueType
    {
        public int Value;
        private double _otherData;
    }

    public class ClassVsStruct
    {
        private ReferenceType[] _referenceTypeData;
        private ReferenceType[] _referenceTypeData2;
        private ValueType[] _valueTypeData;
        private ExtendedValueType[] _extendedValueTypeData;
        private int[] _data;

        [Params(16*1_000)]
        public int Size { get; set; }

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
                _referenceTypeData[i] = new ReferenceType { Value = _data[i] };
                _referenceTypeData2[i] = new ReferenceType { Value = _data[i] };
                _valueTypeData[i] = new ValueType { Value = _data[i] };
                _extendedValueTypeData[i] = new ExtendedValueType { Value = _data[i] };
            }
        }

        [Benchmark(Baseline = true)]
        public int ReferenceTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _referenceTypeData[i].Value;
            }

            return sum;
        }

        [Benchmark]
        public int ReferenceTypeSum2()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _referenceTypeData2[i].Value;
            }

            return sum;
        }

        //[Benchmark]
        public int ValueTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _valueTypeData[i].Value;
            }

            return sum;
        }

        //[Benchmark]
        public int ExtendedValueTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _extendedValueTypeData[i].Value;
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
                sum += array[i].Value;
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
                sum += array[i].Value;
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
                sum += array[i].Value;
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
                sum += array[i].Value;
                sum += array[i + 1].Value;
                sum += array[i + 2].Value;
                sum += array[i + 3].Value;
                sum += array[i + 4].Value;
                sum += array[i + 5].Value;
                sum += array[i + 6].Value;
                sum += array[i + 7].Value;
                sum += array[i + 8].Value;
                sum += array[i + 9].Value;
                sum += array[i + 10].Value;
                sum += array[i + 11].Value;
                sum += array[i + 12].Value;
                sum += array[i + 13].Value;
                sum += array[i + 14].Value;
                sum += array[i + 15].Value;
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
                    sum += array[i + j].Value;
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
                sum += array[i].Value;
                sum += array[i + 1].Value;
                sum += array[i + 2].Value;
                sum += array[i + 3].Value;
                sum += array[i + 4].Value;
                sum += array[i + 5].Value;
                sum += array[i + 6].Value;
                sum += array[i + 7].Value;
                sum += array[i + 8].Value;
                sum += array[i + 9].Value;
                sum += array[i + 10].Value;
                sum += array[i + 11].Value;
                sum += array[i + 12].Value;
                sum += array[i + 13].Value;
                sum += array[i + 14].Value;
                sum += array[i + 15].Value;
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
                sum += array[i].Value;
                sum += array[i + 1].Value;
                sum += array[i + 2].Value;
                sum += array[i + 3].Value;
                sum += array[i + 4].Value;
                sum += array[i + 5].Value;
                sum += array[i + 6].Value;
                sum += array[i + 7].Value;
                sum += array[i + 8].Value;
                sum += array[i + 9].Value;
                sum += array[i + 10].Value;
                sum += array[i + 11].Value;
                sum += array[i + 12].Value;
                sum += array[i + 13].Value;
                sum += array[i + 14].Value;
                sum += array[i + 15].Value;
            }

            return sum;
        }
    }
}