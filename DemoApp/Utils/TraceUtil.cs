using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DemoApp.Utils
{
    public class TraceUtil
    {        

        public static void PrintThreadInfo(string prefix, Thread t)
        {
            Trace.WriteLine(GetThreadInfoMsg(prefix, t), "INFO");
        }

        public static string GetThreadInfoMsg(string prefix, Thread t)
        {

            return $"{prefix}. ThreadID: {Thread.CurrentThread.ManagedThreadId}. IsBackground - {Thread.CurrentThread.IsBackground} IsThreadPool - {Thread.CurrentThread.IsThreadPoolThread}";
        }

    }
}
