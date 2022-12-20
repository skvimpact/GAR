using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;

namespace GarPuller.Queue
{
    public class GarPullerProcessQueue : Queue<Guid>
    {
        public new Task Enqueue(Guid correlationId)
        {
            return Task.Run(() => base.Enqueue(correlationId));
        }        
        public Guid Current(bool remove = false)
        {
            if(base.Count > 0)
                return remove ? base.Dequeue() : base.Peek();
            else
                return Guid.Empty;
        }          
    }
}