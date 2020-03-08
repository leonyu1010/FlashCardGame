using FlashCardGame.Core;
using FlashCardGame.Core.Constants;
using Serilog;
using System;
using System.Collections.Generic;
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
                    string _file = AppConstants.LogFolder + "FlashCard.log";
                    _logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Async(a => a.File(_file)).CreateLogger();
                }
                return _logger;
            }
        }

        private static ILogger _logger;
    }
}