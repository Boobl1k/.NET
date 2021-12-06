using System;
using Microsoft.Extensions.Logging;
using WebApplication.Controllers;

namespace WebApplication;

public class ExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger) =>
        _logger = logger;

    private void Handle(LogLevel logLevel, Exception exception) =>
        _logger.Log(logLevel, exception.Message);

    private void Handle(LogLevel logLevel, NullReferenceException exception) =>
        _logger.Log(logLevel, $"чо нул пихаешь в мой метод? {exception.Message}");

    private void Handle(LogLevel logLevel, DivideByZeroException exception) =>
        _logger.Log(logLevel, $"не дели на 0 {exception.Message}");

    public void DoHandle(LogLevel logLevel, Exception exception) => 
        Handle(logLevel, (dynamic) exception);
}
