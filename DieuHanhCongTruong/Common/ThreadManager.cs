using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DieuHanhCongTruong.Common
{
    class ThreadManager
    {
        public static List<Thread> threadPhanTichOffline = new List<Thread>();

        public static bool CheckThread(Thread currentThread)
        {
            //int threadCount = threadPhanTichOffline.Count;
            bool isRunning = false;
            for (int i = 0; i < threadPhanTichOffline.Count; i++)
            {
                if (threadPhanTichOffline[i].IsAlive)
                {
                    isRunning = true;
                }
                else
                {
                    if(threadPhanTichOffline[i] != currentThread)
                    {
                        threadPhanTichOffline.RemoveAt(i);
                        i--;
                    }
                }
            }
            return isRunning;
        }
    }
}
