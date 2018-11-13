using System;
using System.IO;
using Newtonsoft.Json;

namespace Io.Abstractions
{
    public class FileOperations
    {
        private readonly string _fileName;
        private readonly string _subdirectory;

        public FileOperations()
        {
            _fileName = Path.GetRandomFileName();
            _subdirectory = @"c:\temp\test";
        }

        public string Save()
        {
            var date = DateTime.Now;

            var json = JsonConvert.SerializeObject(date, Formatting.Indented);

            if (!Directory.Exists(_subdirectory))
            {
                Directory.CreateDirectory(_subdirectory);
            }

            var file = File.CreateText(Path.Combine(_subdirectory, _fileName));

            file.Write(json);
            file.Close();

            return Path.Combine(_subdirectory, _fileName);
        }

        public string Read()
        {
            return File.ReadAllText(Path.Combine(_subdirectory, _fileName));
        }
    }
}
