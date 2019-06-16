using System;
using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Async
{
    public class FileAccess
    {
        // async streams!!!

        private const int NumberOfFiles = 5;
        private readonly string[] _fileNames = new string[NumberOfFiles];
        private const int FileSize = 1024 * 1024 * 10;

        [Params(/*4096, 50_000,*/ 120_000)]
        public int ChunkSize { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var buffer = new byte[FileSize];

            for (var i = 0; i < NumberOfFiles; i++)
            {
                _fileNames[i] = "FileAccessBenchmark" + i;

                var stream = File.Create(_fileNames[i]);
                stream.Write(buffer, 0, FileSize);
                stream.Close();
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            foreach (var fileName in _fileNames)
            {
                File.Delete(fileName);
            }
        }

        [Benchmark(Baseline = true)]
        public async Task ReadAsyncWithTasks()
        {
            var tasks = new Task[NumberOfFiles];

            for (var i = 0; i < NumberOfFiles; i++)
            {
                var fileName = _fileNames[i];
                tasks[i] = Task.Run(() =>
                {
                    var stream = File.OpenRead(fileName);

                    ReadFile(stream, fileName);

                    stream.Close();
                    stream.Dispose();
                });
            }

            await Task.WhenAll(tasks);
        }

        private void ReadFile(FileStream stream, string fileName)
        {
            int bytesRead;
            var bytesReadSoFar = 0;
            var buffer = new byte[FileSize + ChunkSize];

            do
            {
                bytesRead = stream.Read(buffer, bytesReadSoFar, ChunkSize);
                bytesReadSoFar += bytesRead;
            }
            while (bytesRead > 0);

            if (bytesReadSoFar != FileSize)
                throw new ApplicationException($"Failed to read the whole file: {fileName}");
        }

        [Benchmark]
        public void ReadSync()
        {
            for (var i = 0; i < NumberOfFiles; i++)
            {
                var stream = File.OpenRead(_fileNames[i]);

                ReadFile(stream, _fileNames[i]);

                stream.Close();
                stream.Dispose();
            }
        }
        [Benchmark]
        public async Task ReadSyncWith_ReadAsync()
        {
            for (var i = 0; i < NumberOfFiles; i++)
            {
                var stream = File.OpenRead(_fileNames[i]);

                int bytesRead;
                var bytesReadSoFar = 0;
                var buffer = new byte[FileSize + ChunkSize];

                do
                {
                    bytesRead = await stream.ReadAsync(buffer, bytesReadSoFar, ChunkSize);
                    bytesReadSoFar += bytesRead;
                }
                while (bytesRead > 0);

                if (bytesReadSoFar != FileSize)
                    throw new ApplicationException($"Failed to read the whole file: {_fileNames[i]}");

                stream.Close();
                stream.Dispose();
            }
        }

        //[Benchmark]
        public async Task ReadAsyncWithAsyncRead()
        {
            var buffer = new byte[1024 * 1024];
            var tasks = new Task[NumberOfFiles];

            for (var i = 0; i < NumberOfFiles; i++)
            {
                var fileName = _fileNames[i];
                tasks[i] = Task.Run(async () =>
                {
                    var stream = new FileStream(
                        fileName,
                        FileMode.Open,
                        System.IO.FileAccess.Read,
                        FileShare.None,
                        4096,
                        FileOptions.SequentialScan | FileOptions.Asynchronous);

                    int bytesRead;
                    do
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, ChunkSize);
                    }
                    while (bytesRead > 0);
                    stream.Close();
                    stream.Dispose();
                });
            }

            await Task.WhenAll(tasks);
        }

    }
}
