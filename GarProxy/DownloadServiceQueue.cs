using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarProxy
{
    public class DownloadServiceQueue
    {
        private readonly Queue<(string, Guid)> files = new Queue<(string, Guid)>();
        private readonly List<Guid> archive = new List<Guid>();
        private (string, Guid) _current;
        public Task Enqueue((string,Guid) file)
        {
            return Task.Run(() => {
            if (!archive.Contains(file.Item2))
                files.Enqueue(file);
            });
        }

        public (string, Guid) Dequeue()
        {
            if(files.Count > 0)
                _current = files.Dequeue();
            else
                _current.Item1 = string.Empty;
            return _current;
        }

        public void Archive()
        {
            if (_current.Item1 != null)
                archive.Add(_current.Item2);
        }    
    }
}