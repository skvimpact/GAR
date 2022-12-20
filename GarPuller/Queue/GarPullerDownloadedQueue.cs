using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarPuller.Queue
{
    public class GarPullerDownloadedQueue : Queue<(string, Guid)>
    {
        public new Task Enqueue((string, Guid) file)
        {
            return Task.Run(() => base.Enqueue(file));
        }

        public (string, Guid) Current(bool remove = false)
        {
            if(base.Count > 0)
                return remove ? base.Dequeue() : base.Peek();
            else
                return (string.Empty, Guid.Empty);
        }     
    }
}