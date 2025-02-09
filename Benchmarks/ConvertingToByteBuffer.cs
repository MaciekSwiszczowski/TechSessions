// ReSharper disable always ForCanBeConvertedToForeach
// ReSharper disable always LoopCanBeConvertedToQuery

using System;
using System.Buffers.Binary;
using BenchmarkDotNet.Attributes;

namespace Benchmarks;

[MemoryDiagnoser]
// ReSharper disable once ClassCanBeSealed.Global
public class ConvertingToByteBuffer
{
    private byte[] _intBuffer;
    private int[] _intValues;

    private byte[] _longBuffer;
    private long[] _longValues;
    private byte[] _shortBuffer;
    private short[] _shortValues;
    private byte[] _uintBuffer;
    private uint[] _uintValues;
    private byte[] _ulongBuffer;
    private ulong[] _ulongValues;
    private byte[] _ushortBuffer;
    private ushort[] _ushortValues;

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random();

        _longValues =
        [
            long.MinValue, long.MaxValue, 0, -1, 1,
            random.NextInt64(long.MinValue / 2, long.MaxValue / 2),
            random.NextInt64(long.MinValue / 2, long.MaxValue / 2),
        ];

        _ulongValues =
        [
            ulong.MinValue, ulong.MaxValue, 0, 1,
            (ulong)random.NextInt64(0, (long)(ulong.MaxValue / 2)),
            (ulong)random.NextInt64(0, (long)(ulong.MaxValue / 2)),
        ];

        _intValues =
        [
            int.MinValue, int.MaxValue, 0, -1, 1,
            random.Next(int.MinValue / 2, int.MaxValue / 2),
            random.Next(int.MinValue / 2, int.MaxValue / 2),
        ];

        _uintValues =
        [
            uint.MinValue, uint.MaxValue, 0, 1,
            (uint)random.Next(0, int.MaxValue / 2),
            (uint)random.Next(0, int.MaxValue / 2),
        ];

        _shortValues =
        [
            short.MinValue, short.MaxValue, 0, -1, 1,
            (short)random.Next(short.MinValue / 2, short.MaxValue / 2),
            (short)random.Next(short.MinValue / 2, short.MaxValue / 2),
        ];

        _ushortValues =
        [
            ushort.MinValue, ushort.MaxValue, 0, 1,
            (ushort)random.Next(0, ushort.MaxValue / 2),
            (ushort)random.Next(0, ushort.MaxValue / 2),
        ];

        _longBuffer = new byte[sizeof(long)];
        _ulongBuffer = new byte[sizeof(ulong)];
        _intBuffer = new byte[sizeof(int)];
        _uintBuffer = new byte[sizeof(uint)];
        _shortBuffer = new byte[sizeof(short)];
        _ushortBuffer = new byte[sizeof(ushort)];
    }

    [Benchmark(Baseline = true)]
    public int ConvertUsingBitConverter_Int()
    {
        var sum = 0;
        for (var i = 0; i < _intValues.Length; i++)
        {
            var result = BitConverter.GetBytes(_intValues[i]);
            for (var j = 0; j < result.Length; j++)
            {
                sum += result[j];
            }
        }
        return sum;
    }
/*
    
    [Benchmark]
    public int ConvertUsingBinaryPrimitives_Int()
    {
        var sum = 0;
        for (var i = 0; i < _intValues.Length; i++)
        {
            BinaryPrimitives.WriteInt32LittleEndian(_intBuffer, _intValues[i]);
            for (var j = 0; j < _intBuffer.Length; j++)
            {
                sum += _intBuffer[j];
            }
        }
        return sum;
    }

    // Benchmarks for uint
    [Benchmark]
    public int ConvertUsingBitConverter_UInt()
    {
        var sum = 0;
        for (var i = 0; i < _uintValues.Length; i++)
        {
            var result = BitConverter.GetBytes(_uintValues[i]);
            for (var j = 0; j < result.Length; j++)
            {
                sum += result[j];
            }
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_UInt()
    {
        var sum = 0;
        for (var i = 0; i < _uintValues.Length; i++)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(_uintBuffer, _uintValues[i]);
            for (var j = 0; j < _uintBuffer.Length; j++)
            {
                sum += _uintBuffer[j];
            }
        }
        return sum;
    }

    // Benchmarks for long
    [Benchmark]
    public int ConvertUsingBitConverter_Long()
    {
        var sum = 0;
        for (var i = 0; i < _longValues.Length; i++)
        {
            var result = BitConverter.GetBytes(_longValues[i]);
            for (var j = 0; j < result.Length; j++)
            {
                sum += result[j];
            }
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_Long()
    {
        var sum = 0;
        for (var i = 0; i < _longValues.Length; i++)
        {
            BinaryPrimitives.WriteInt64LittleEndian(_longBuffer, _longValues[i]);
            for (var j = 0; j < _longBuffer.Length; j++)
            {
                sum += _longBuffer[j];
            }
        }
        return sum;
    }

    // Benchmarks for ulong
    [Benchmark]
    public int ConvertUsingBitConverter_ULong()
    {
        var sum = 0;
        for (var i = 0; i < _ulongValues.Length; i++)
        {
            var result = BitConverter.GetBytes(_ulongValues[i]);
            for (var j = 0; j < result.Length; j++)
            {
                sum += result[j];
            }
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_ULong()
    {
        var sum = 0;
        for (var i = 0; i < _ulongValues.Length; i++)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(_ulongBuffer, _ulongValues[i]);
            for (var j = 0; j < _ulongBuffer.Length; j++)
            {
                sum += _ulongBuffer[j];
            }
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBitConverter_Short()
    {
        var sum = 0;
        for (var i = 0; i < _shortValues.Length; i++)
        {
            var result = BitConverter.GetBytes(_shortValues[i]);
            for (var j = 0; j < result.Length; j++)
            {
                sum += result[j];
            }
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_Short()
    {
        var sum = 0;
        for (var i = 0; i < _shortValues.Length; i++)
        {
            BinaryPrimitives.WriteInt16LittleEndian(_shortBuffer, _shortValues[i]);
            for (var j = 0; j < _shortBuffer.Length; j++)
            {
                sum += _shortBuffer[j];
            }
        }
        return sum;
    }
*/
    [Benchmark]
    public int ConvertUsingBitConverter_UShort()
    {
        var sum = 0;
        for (var i = 0; i < _ushortValues.Length; i++)
        {
            var result = BitConverter.GetBytes(_ushortValues[i]);
            for (var j = 0; j < result.Length; j++)
            {
                sum += result[j];
            }
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_UShort()
    {
        var sum = 0;
        for (var i = 0; i < _ushortValues.Length; i++)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(_ushortBuffer, _ushortValues[i]);
            for (var j = 0; j < _ushortBuffer.Length; j++)
            {
                sum += _ushortBuffer[j];
            }
        }
        return sum;
    }
}