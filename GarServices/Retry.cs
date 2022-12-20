using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarServices
{
    public class Retry
    {
        public static void DoWithRetry(Action action, TimeSpan sleepPeriod, Action<int> log, int tryCount = 3)
        {
            if (tryCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(tryCount));

            while (true) {
                try {
                    log(tryCount);
                    action();
                    break; // success!
                } catch {
                    if (--tryCount == 0)
                        throw;
                    Thread.Sleep(sleepPeriod);
                }
            }
        
        }
        public static async Task DoWithRetryAsync(Func<Task> action, TimeSpan sleepPeriod, int tryCount = 3)
        {
            if (tryCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(tryCount));

            while (true) {
                try {
                    await action();
                    return; // success!
                } catch {
                    if (--tryCount == 0)
                        throw;
                    await Task.Delay(sleepPeriod);
                }
                
            }
        }

        public static async Task<T> DoWithRetryAsync<T>(Func<Task<T>> action, TimeSpan sleepPeriod, Action<int> log, int tryCount = 3)
        {
            if (tryCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(tryCount));

            while (true) {
                try {
                    log(tryCount);
                    return await action();
                    //return; // success!
                } catch {
                    if (--tryCount == 0)
                        throw;
                    await Task.Delay(sleepPeriod);
                }
                
            }
        }


        public async Task<T> Retry2<T>(Func<T> action, TimeSpan retryInterval, int retryCount)
{
    try
    {
        return action();
    }
    catch when (retryCount != 0)
    {
        await Task.Delay(retryInterval);
        return await Retry2(action, retryInterval, --retryCount);
    }
}

       // public static Task DoWithRetryAsync(Task task, TimeSpan timeSpan)
      //  {
//         //   throw new NotImplementedException();
      //  }
    }
}