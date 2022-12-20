using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GarPuller.Queue;

namespace GarPuller.Tests
{
    public class GarPullerGoQueueTest
    {
        [Fact]
        public async Task CheckUpdateWorkOnlyOnce() {
            var go = new GarPullerGoQueue();
            await go.Start();
            if (go.Go()) {
                if (go.CheckUpdate())
                    Debug.WriteLine("Check is right");
                if (go.CheckUpdate())
                    Debug.WriteLine("Check is not right !!!");
            }
            if (go.Go()) {
                if (!go.CheckUpdate())
                    Debug.WriteLine("Not Check is right");
                if (!go.CheckUpdate())
                    Debug.WriteLine("Not Check is right");
            }
            go.Dismiss();
            if (go.Go()) {
                Debug.WriteLine("Go is not right !!!");
               if (go.CheckUpdate())
                    Debug.WriteLine("Check is not right !!!");                
            }

        }
    }
}