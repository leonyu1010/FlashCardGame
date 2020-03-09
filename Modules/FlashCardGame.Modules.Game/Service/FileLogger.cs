using FlashCardGame.Core.Constants;
using Serilog;
using System.IO;
using System.Reflection;

namespace FlashCardGame.Modules.Game.Service
{
    public class FileLogger
    {
        public static ILogger Singleton
        {
            get
            {
                lock (_lock)
                {
                    if (_logger == null)
                    {
                        string folder = string.Empty;
                        if (Directory.Exists(AppConstants.LogFolder))
                        {
                            folder = AppConstants.LogFolder;
                        }
                        else
                        {
                            folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        }

                        string _file = Path.Combine(folder, "FlashCard.log");
                        _logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Async(a => a.File(_file)).CreateLogger();
                    }
                    return _logger;
                }
            }
        }

        private static readonly object _lock = new object();
        private static ILogger _logger;
    }

    //public class LazyFileLogger
    //{
    //    private static readonly Lazy<ILogger> _lazy = new Lazy<ILogger>(() => CreateLogger());
    //    public static ILogger Singleton { get { return _lazy.Value; } }

    //    private static ILogger CreateLogger()
    //    {
    //        string folder = string.Empty;

    //        if (Directory.Exists(AppConstants.LogFolder))
    //        {
    //            folder = AppConstants.LogFolder;
    //        }
    //        else
    //        {
    //            folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    //        }

    //        string file = Path.Combine(folder, "FlashCard.log");
    //        return new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Async(a => a.File(file)).CreateLogger();
    //    }
    //}
}