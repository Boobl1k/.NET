using System;
using Microsoft.Extensions.Logging;
using WebApplication.Controllers;

namespace WebApplication;

public class ExceptionHandler : ILogger<CalculatorController>
{
    private readonly ILogger _logger;

    public ExceptionHandler(ILogger logger) =>
        _logger = logger;

    private void Handle(LogLevel logLevel, Exception exception) =>
        _logger.Log(logLevel, exception.Message);

    private void Handle(LogLevel logLevel, NullReferenceException exception) =>
        _logger.Log(logLevel, $"чо нул пихаешь в мой метод? {exception.Message}");

    private void Handle(LogLevel logLevel, DivideByZeroException exception) =>
        _logger.Log(logLevel, $"не дели на 0 {exception.Message}");

    public IDisposable BeginScope<TState>(TState state) => default;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
        Func<TState, Exception, string> formatter) =>
        Handle(logLevel, (dynamic) exception);
}
