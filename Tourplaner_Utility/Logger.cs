using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Logging;
using ILogger = log4net.Core.ILogger;


namespace Tourplaner_Utility
{
    public static class Logginfactory
    {
        public static ILog CreateLogger(Type typename)
        {
            return LogManager.GetLogger(typename);
        }


    }
}
