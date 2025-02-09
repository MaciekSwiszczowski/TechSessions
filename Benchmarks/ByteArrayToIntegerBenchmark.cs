// ReSharper disable always ForCanBeConvertedToForeach
// ReSharper disable always LoopCanBeConvertedToQuery

using System;
using System.Buffers.Binary;
using BenchmarkDotNet.Attributes;

namespace Benchmarks;

[MemoryDiagnoser]
// ReSharper disable once ClassCanBeSealed.Global
public class ByteArrayToIntegerBenchmark
{
    private byte[][] _intByteArrays;
    private byte[][] _longByteArrays;
    private byte[][] _shortByteArrays;
    private byte[][] _uintByteArrays;
    private byte[][] _ulongByteArrays;
    private byte[][] _ushortByteArrays;

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random();

        _longByteArrays =
        [
            BitConverter.GetBytes(long.MinValue), BitConverter.GetBytes(long.MaxValue),
            BitConverter.GetBytes(0L), BitConverter.GetBytes(-1L), BitConverter.GetBytes(1L),
            BitConverter.GetBytes(random.NextInt64(long.MinValue / 2, long.MaxValue / 2)),
            BitConverter.GetBytes(random.NextInt64(long.MinValue / 2, long.MaxValue / 2)),
        ];

        _ulongByteArrays =
        [
            BitConverter.GetBytes(ulong.MinValue), BitConverter.GetBytes(ulong.MaxValue),
            BitConverter.GetBytes(0UL), BitConverter.GetBytes(1UL),
            BitConverter.GetBytes((ulong)random.NextInt64(0, (long)(ulong.MaxValue / 2))),
            BitConverter.GetBytes((ulong)random.NextInt64(0, (long)(ulong.MaxValue / 2))),
        ];

        _intByteArrays =
        [
            BitConverter.GetBytes(int.MinValue), BitConverter.GetBytes(int.MaxValue),
            BitConverter.GetBytes(0), BitConverter.GetBytes(-1), BitConverter.GetBytes(1),
            BitConverter.GetBytes(random.Next(int.MinValue / 2, int.MaxValue / 2)),
            BitConverter.GetBytes(random.Next(int.MinValue / 2, int.MaxValue / 2)),
        ];

        _uintByteArrays =
        [
            BitConverter.GetBytes(uint.MinValue), BitConverter.GetBytes(uint.MaxValue),
            BitConverter.GetBytes(0U), BitConverter.GetBytes(1U),
            BitConverter.GetBytes((uint)random.Next(0, int.MaxValue / 2)),
            BitConverter.GetBytes((uint)random.Next(0, int.MaxValue / 2)),
        ];

        _shortByteArrays =
        [
            BitConverter.GetBytes(short.MinValue), BitConverter.GetBytes(short.MaxValue),
            BitConverter.GetBytes((short)0), BitConverter.GetBytes((short)-1), BitConverter.GetBytes((short)1),
            BitConverter.GetBytes((short)random.Next(short.MinValue / 2, short.MaxValue / 2)),
            BitConverter.GetBytes((short)random.Next(short.MinValue / 2, short.MaxValue / 2)),
        ];

        _ushortByteArrays =
        [
            BitConverter.GetBytes(ushort.MinValue), BitConverter.GetBytes(ushort.MaxValue),
            BitConverter.GetBytes((ushort)0), BitConverter.GetBytes((ushort)1),
            BitConverter.GetBytes((ushort)random.Next(0, ushort.MaxValue / 2)),
            BitConverter.GetBytes((ushort)random.Next(0, ushort.MaxValue / 2)),
        ];
    }

    // Benchmarks for int
    [Benchmark(Baseline = true)]
    public int ConvertUsingBitConverter_Int()
    {
        var sum = 0;
        for (var i = 0; i < _intByteArrays.Length; i++)
        {
            var result = BitConverter.ToInt32(_intByteArrays[i], 0);
            sum += result;
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_Int()
    {
        var sum = 0;
        for (var i = 0; i < _intByteArrays.Length; i++)
        {
            var result = BinaryPrimitives.ReadInt32LittleEndian(_intByteArrays[i]);
            sum += result;
        }
        return sum;
    }

    // Benchmarks for uint
    [Benchmark]
    public int ConvertUsingBitConverter_UInt()
    {
        var sum = 0;
        for (var i = 0; i < _uintByteArrays.Length; i++)
        {
            var result = BitConverter.ToUInt32(_uintByteArrays[i], 0);
            sum += (int)result;
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_UInt()
    {
        var sum = 0;
        for (var i = 0; i < _uintByteArrays.Length; i++)
        {
            var result = BinaryPrimitives.ReadUInt32LittleEndian(_uintByteArrays[i]);
            sum += (int)result;
        }
        return sum;
    }

    // Benchmarks for long
    [Benchmark]
    public long ConvertUsingBitConverter_Long()
    {
        long sum = 0;
        for (var i = 0; i < _longByteArrays.Length; i++)
        {
            var result = BitConverter.ToInt64(_longByteArrays[i], 0);
            sum += result;
        }
        return sum;
    }

    [Benchmark]
    public long ConvertUsingBinaryPrimitives_Long()
    {
        long sum = 0;
        for (var i = 0; i < _longByteArrays.Length; i++)
        {
            var result = BinaryPrimitives.ReadInt64LittleEndian(_longByteArrays[i]);
            sum += result;
        }
        return sum;
    }

    // Benchmarks for ulong
    [Benchmark]
    public long ConvertUsingBitConverter_ULong()
    {
        long sum = 0;
        for (var i = 0; i < _ulongByteArrays.Length; i++)
        {
            var result = BitConverter.ToUInt64(_ulongByteArrays[i], 0);
            sum += (long)result;
        }
        return sum;
    }

    [Benchmark]
    public long ConvertUsingBinaryPrimitives_ULong()
    {
        long sum = 0;
        for (var i = 0; i < _ulongByteArrays.Length; i++)
        {
            var result = BinaryPrimitives.ReadUInt64LittleEndian(_ulongByteArrays[i]);
            sum += (long)result;
        }
        return sum;
    }

    // Benchmarks for short
    [Benchmark]
    public int ConvertUsingBitConverter_Short()
    {
        var sum = 0;
        for (var i = 0; i < _shortByteArrays.Length; i++)
        {
            var result = BitConverter.ToInt16(_shortByteArrays[i], 0);
            sum += result;
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_Short()
    {
        var sum = 0;
        for (var i = 0; i < _shortByteArrays.Length; i++)
        {
            var result = BinaryPrimitives.ReadInt16LittleEndian(_shortByteArrays[i]);
            sum += result;
        }
        return sum;
    }

    // Benchmarks for ushort
    [Benchmark]
    public int ConvertUsingBitConverter_UShort()
    {
        var sum = 0;
        for (var i = 0; i < _ushortByteArrays.Length; i++)
        {
            var result = BitConverter.ToUInt16(_ushortByteArrays[i], 0);
            sum += result;
        }
        return sum;
    }

    [Benchmark]
    public int ConvertUsingBinaryPrimitives_UShort()
    {
        var sum = 0;
        for (var i = 0; i < _ushortByteArrays.Length; i++)
        {
            var result = BinaryPrimitives.ReadUInt16LittleEndian(_ushortByteArrays[i]);
            sum += result;
        }
        return sum;
    }
}