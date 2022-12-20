using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;

namespace GarPuller.Queue
{
    public class GarPullerFileQueue : Queue<GarFile>
    {
        public new Task Enqueue(GarFile file)
        {
            return Task.Run(() => base.Enqueue(file));
        }

        public GarFile? Current(bool remove = false)
        {
            if(base.Count > 0)
                return remove ? base.Dequeue() : base.Peek();
            else
                return default(GarFile);
        } 
    }
}