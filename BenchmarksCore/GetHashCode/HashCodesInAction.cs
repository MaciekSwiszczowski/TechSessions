using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace BenchmarksCore.GetHashCode
{
    public class HashCodesInAction
    {
        private A[] _dataA;
        private B[] _dataB;
        private DotNetCoreHashCode[] _dataDotNetCoreHashCode;
        private TrivialAndWrong[] _dataTrivialAndWrong;


        [Params(1000, 10_000 /*, 1_000_000*/)] public int Size { get; set; }


        [GlobalSetup]
        public void GlobalSetup()
        {
            _dataA = new A[Size];
            _dataB = new B[Size];
            _dataDotNetCoreHashCode = new DotNetCoreHashCode[Size];
            _dataTrivialAndWrong = new TrivialAndWrong[Size];

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
        public Dictionary<A, float> ResharperLikeHashCodes()
        {
            var size = Size;
            var results = new Dictionary<A, float>();
            var data = _dataA;

            for (var i = 0; i < size; i++)
            {
                if (!results.ContainsKey(data[i]))
                    results.Add(data[i], data[i].X + data[i].Y + data[i].Z);
            }

            return results;
        }

        [Benchmark(Baseline = true)]
        public Dictionary<B, float> JonSkeetLikeHashCodes()
        {
            var size = Size;
            var results = new Dictionary<B, float>();
            var data = _dataB;

            for (var i = 0; i < size; i++)
            {
                if (!results.ContainsKey(data[i]))
                    results.Add(data[i], data[i].X + data[i].Y + data[i].Z);
            }

            return results;
        }

        [Benchmark]
        public Dictionary<DotNetCoreHashCode, float> DotNetCoreLikeHashCodes()
        {
            var size = Size;
            var results = new Dictionary<DotNetCoreHashCode, float>();
            var data = _dataDotNetCoreHashCode;

            for (var i = 0; i < size; i++)
            {
                if (!results.ContainsKey(data[i]))
                    results.Add(data[i], data[i].X + data[i].Y + data[i].Z);
            }

            return results;
        }

        // [Benchmark] - trivial hash code breaks Dictionary :)
        public Dictionary<TrivialAndWrong, float> TrivialHashCodes()
        {
            var size = Size;
            var results = new Dictionary<TrivialAndWrong, float>();
            var data = _dataTrivialAndWrong;

            for (var i = 0; i < size; i++)
            {
                if (!results.ContainsKey(data[i]))
                    results.Add(data[i], data[i].X + data[i].Y + data[i].Z);
            }

            return results;
        }
    }
}