using System;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Tuples
{

    [MemoryDiagnoser]
    public class DeconstructDateTime
    {
        [Benchmark]
        public string NoTuplesVersion()
        {
            var dateTime = DateTime.Now;
            var day = dateTime.Day;
            var month = dateTime.Day;
            var year = dateTime.Day;
            return $"{day} {month} {year}";
        }

        [Benchmark(Baseline = true)]
        public string TuplesVersion()
        {
            var (day, month, year) = DateTime.Now;
            return $"{day} {month} {year}";
        }

    }

    public static class DeconstructExtensions
    {
        public static void Deconstruct(this DateTime dateTime, out int day, out int month, out int year)
        {
            year = dateTime.Year;
            month = dateTime.Month;
            day = dateTime.Day;
        }
    }
}
