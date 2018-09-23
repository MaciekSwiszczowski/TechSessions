
using System;
using System.Numerics;

namespace Simd
{
    public static class MinMax
    {
        public static (int min, int max) MinMaxNaive(int[] data)
        {
            var max = int.MinValue;
            var min = int.MaxValue;

            var dataLength = data.Length;
            for (var index = 0; index < dataLength; index++)
            {
                min = Math.Min(min, data[index]);
                max = Math.Max(max, data[index]);
            }

            return (min, max);
        }

        public static (int min, int max) MinMaxSimd(int[] data)
        {
            var vmin = new Vector<int>(int.MaxValue);
            var vmax = new Vector<int>(int.MinValue);
            var vecSize = Vector<int>.Count;

            for (var i = 0; i < data.Length; i += vecSize)
            {
                var vdata = new Vector<int>(data, i);
                var minMask = Vector.LessThan(vdata, vmin);
                var maxMask = Vector.GreaterThan(vdata, vmax);
                vmin = Vector.ConditionalSelect(minMask, vdata, vmin);
                vmax = Vector.ConditionalSelect(maxMask, vdata, vmax);
            }

            var min = int.MaxValue;
            var max = int.MinValue;
            for (var i = 0; i < vecSize; ++i)
            {
                min = Math.Min(min, vmin[i]);
                max = Math.Max(max, vmax[i]);
            }

            return (min, max);
        }


    }
}