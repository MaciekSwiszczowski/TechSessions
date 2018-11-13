using System;
using System.IO;
using System.IO.Abstractions;
using Newtonsoft.Json;

namespace Io.Abstractions
{
    public class FileOperationsAbstracted
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _fileName;
        private readonly string _subdirectory;

        public FileOperationsAbstracted(IFileSystem fileSystem)
        {

        }

        public FileOperationsAbstracted() : this(new FileSystem())
        {
        }

        //public string Save()
        //{

        //}

        //public string Read()
        //{

        //}
    }
}
