// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using EPiServer.Logging;
using ILogger = DbLocalizationProvider.Logging.ILogger;

namespace DbLocalizationProvider.EPiServer;

public class LoggerAdapter : ILogger
{
    private readonly global::EPiServer.Logging.ILogger _logger;

    public LoggerAdapter()
    {
        _logger = LogManager.GetLogger(typeof(LoggerAdapter));
    }

    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Info(string message)
    {
        _logger.Information(message);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

    public void Error(string message, Exception exception)
    {
        _logger.Error(message, exception);
    }
}
