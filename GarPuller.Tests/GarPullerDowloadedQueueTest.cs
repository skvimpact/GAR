using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GarPuller.Queue;

namespace GarPuller.Tests
{
    public class GarPullerDowloadedQueueTest
    {
        [Fact]
        public async Task QueueCanBeAdded() {
            var queue = new GarPullerDownloadedQueue();
            await queue.Enqueue(("a", Guid.Empty));
            await queue.Enqueue(("b", Guid.Empty));
            await queue.Enqueue(("c", Guid.Empty));
            var z = queue.Current(true).Item1;
            z = queue.Current().Item1;
            z = queue.Current(true).Item1;
            z = queue.Current().Item1;
            z = queue.Current().Item1;
            z = queue.Current().Item1;
            var current = queue.Current();
            if (current.Item2 == Guid.Empty) {
                Debug.WriteLine("Guid.Empty");
            }
            var current2 = queue.Peek();

        }
    }
}