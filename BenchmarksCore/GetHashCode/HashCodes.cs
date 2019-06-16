using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace BenchmarksCore.GetHashCode
{
    [MemoryDiagnoser]
    [HardwareCounters(HardwareCounter.BranchInstructions, HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses)]

    public class HashCodes
    {
        private A[] _dataA;
        private B[] _dataB;
        private DotNetCoreHashCode[] _dataDotNetCoreHashCode;
        private TrivialAndWrong[] _dataTrivialAndWrong;
        private int[] _results;


        [Params(1000 /*,10_000*/ /*, 1_000_000*/)] public int Size { get; set; }


        [GlobalSetup]
        public void GlobalSetup()
        {
            _dataA = new A[Size];
            _dataB = new B[Size];
            _dataDotNetCoreHashCode = new DotNetCoreHashCode[Size];
            _dataTrivialAndWrong = new TrivialAndWrong[Size];
            _results = new int[Size];

            var r = new Random(1234);
            float GetNextDouble() => 5000f - 10_000f * (float)r.NextDouble();

            for (var i = 0; i < Size; i++)
            {
                var (x, y, z) = (GetNextDouble(), GetNextDouble(), GetNextDouble());
                _dataA[i] = new A(x, y, z);
                _dataB[i] = new B(x, y, z);
                _dataDotNetCoreHashCode[i] = new DotNetCoreHashCode(x, y, z);
                _dataTrivialAndWrong[i] = new TrivialAndWrong(x, y, z);
            }
        }

        [Benchmark]
        public int[] ResharperLikeHashCodes()
        {
            var size = _results.Length;

            for (var i = 0; i < size; i++)
            {
                _results[i] = _dataA[i].GetHashCode();
            }

            return _results;
        }

        [Benchmark(Baseline = true)]
        public int[] JonSkeetLikeHashCodes()
        {
            var size = _results.Length;

            for (var i = 0; i < size; i++)
            {
                _results[i] = _dataB[i].GetHashCode();
            }

            return _results;
        }

        [Benchmark]
        public int[] DotNetCoreLikeHashCodes()
        {
            var size = _results.Length;

            for (var i = 0; i < size; i++)
            {
                _results[i] = _dataDotNetCoreHashCode[i].GetHashCode();
            }

            return _results;
        }

        [Benchmark]
        public int[] TrivialHashCodes()
        {
            var size = _results.Length;

            for (var i = 0; i < size; i++)
            {
                _results[i] = _dataTrivialAndWrong[i].GetHashCode();
            }

            return _results;
        }
    }

    public readonly struct A
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public A(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }

    public readonly struct B
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public B(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }
    }

    public readonly struct DotNetCoreHashCode
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public DotNetCoreHashCode(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    }

    public readonly struct TrivialAndWrong
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public TrivialAndWrong(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override int GetHashCode() => 1;
    }
}
