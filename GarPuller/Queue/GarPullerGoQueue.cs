using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarPuller.Queue
{
    public class GarPullerGoQueue : Queue<bool>
    {
        private Queue<bool> checkUpdate = new Queue<bool>();
        public Task Start()
        {
            return Task.Run(() => {
                if (base.Count == 0) {
                    base.Enqueue(true);
                    checkUpdate.Enqueue(true);
                }});
        }
        public bool Go()
        {
            return (base.Count > 0);
        }  
        public void Dismiss() {
            base.Clear();           
        }
        public bool CheckUpdate() {
            return checkUpdate.Count > 0 ? checkUpdate.Dequeue() : false;    
        }
    }
}