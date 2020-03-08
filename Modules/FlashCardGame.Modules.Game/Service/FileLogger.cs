using FlashCardGame.Core;
using FlashCardGame.Core.Constants;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FlashCardGame.Modules.Game.Service
{
    public class FileLogger
    {
        public static ILogger Singleton
        {
            get
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

        private static ILogger _logger;
    }
}