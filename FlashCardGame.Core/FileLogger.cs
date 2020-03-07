using FlashCardGame.Core.Constants;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public static class LoggerFactor
    {
        public static IAppLogger GetFileLogger()
        {
            string _file = AppConstants.LogFolder + "DeckLinkService.log";
            var logger = (new LoggerConfiguration().WriteTo.Async(a => a.File(_file)).CreateLogger() as ILogger);
            return (logger as IAppLogger);
        }
    }

    public class FileLogger
    {
        public FileLogger()
        {
            string _file = AppConstants.LogFolder + "DeckLinkService.log";
            var logger = (new LoggerConfiguration().WriteTo.Async(a => a.File(_file)).CreateLogger() as ILogger);
        }
    }
}