using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PrintJob
{
    public interface IFileLoader
    {
        // Reads the source and loads the data into memory.
        void Load();
        // Stores the data retreived during loading.
        IEnumerable<string> Lines { get; }
    }

    public class FileLoader : IFileLoader
    {
        public FileLoader(string fileName)
        {
            FileName = fileName;
        }
        public string FileName { get; set; }
        public IEnumerable<string> Lines { get; private set; }
        public void Load()
        {
            Lines = File.ReadAllLines(FileName).ToList();
        }
    }
}