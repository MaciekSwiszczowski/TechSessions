using System;
using System.Globalization;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace BenchmarksCore
{
    public class Program
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");


            BenchmarkRunner.Run<ClassVsStruct>();


            Console.ReadKey();
        }
    }

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

    //[HardwareCounters(CacheMisses, BranchInstructions, BranchMispredictions)]
    [PlainExporter]
    [CoreJob]
    public class ClassVsStruct
    {
        private ReferenceType[] _referenceTypeData;
        private ValueType[] _valueTypeData;
        private ExtendedValueType[] _extendedValueTypeData;
        private int[] _data;

        [Params(100/*, 500, 1000/*, 10000, 100_000*/)]
        public int Size { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _referenceTypeData = new ReferenceType[Size];
            _valueTypeData = new ValueType[Size];
            _extendedValueTypeData = new ExtendedValueType[Size];

            _data = new int[Size];

            var random = new Random();
            for (var i = 0; i < Size; i++)
            {
                _data[i] = random.Next(100000);
                _referenceTypeData[i] = new ReferenceType { Value = _data[i] };
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
        public int ValueTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _valueTypeData[i].Value;
            }

            return sum;
        }

        [Benchmark]
        public int ExtendedValueTypeSum()
        {
            var sum = 0;

            for (var i = 0; i < Size; i++)
            {
                sum += _extendedValueTypeData[i].Value;
            }

            return sum;
        }
    }
}
