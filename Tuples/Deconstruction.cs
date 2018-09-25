using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Tuples
{
    [TestClass]
    public class Deconstruction
    {
        [TestMethod]
        public void OnDeconstruct()
        {
            var (min, max) = new MinMax(new [] {1, 43, -5, -1});
            var minMax = new MinMax(new[] { 1, 43, -5, -1 });

            min.ShouldBe(-5);
            max.ShouldBe(43);

            minMax.ShouldBeAssignableTo<MinMax>();
        }

        //[TestMethod]
        //public void MoreOnDeconstruct()
        //{
        //    var (min, _, exception) = new MinMax(new [] {1, 43, -5, -1});

        //    min.ShouldBe(-5);
        //    exception.ShouldBeNull();
        //}
    }

    public class MinMax
    {
        private readonly (int min, int max) _minMax;
        private ArgumentException _exception;

        public void Deconstruct(out int min, out int max)
        {
            min = _minMax.min;
            max = _minMax.max;
        }

        //public void Deconstruct(out int min, out int max, out Exception ex)
        //{
        //    min = _minMax.min;
        //    max = _minMax.max;
        //    ex = _exception;
        //}

        public MinMax(int[] values)
        {
            _minMax = GetMinMax(values);
        }

        private (int min, int max) GetMinMax(int[] values)
        {
            (int min, int max) minMax = (int.MaxValue, int.MinValue);

            if (values == null || values.Length == 0)
            {
                _exception = new ArgumentException("input collection is incorrect");
                return minMax;
            }

            foreach (var value in values)
            {
                minMax.min = Math.Min(minMax.min, value);
                minMax.max = Math.Max(minMax.max, value);
            }

            return minMax;
        }
    }
}
