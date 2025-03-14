using BenchmarkDotNet.Running;

namespace Benchmark48
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<NestedAsync>();
        }
    }
} 