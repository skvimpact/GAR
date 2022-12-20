using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarPuller
{
    public class DirectoryService
    {
        private readonly string _dirPath;
        
        public DirectoryService(string dirPath) {
            _dirPath = dirPath;
        }
        public async Task<long> Size() {
            DirectoryInfo dirInfo = new DirectoryInfo(_dirPath);
            return await Task.Run(() => dirInfo.EnumerateFiles( "*", SearchOption.AllDirectories).Sum(file => file.Length));
        }

    }
}