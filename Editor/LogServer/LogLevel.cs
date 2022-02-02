using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.LogServer
{
    public enum LogLevel : int
    {
        Info = 1,
        Warning = 2,
        Error = 4,
        Fatal = 8,
        Overlap = 16,
        Performance = 32,
        Counter = 64,
        MethodCall = 128
    }
}
