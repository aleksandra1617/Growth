using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Growth.Utilities
{
    public sealed class ConsoleFormatterOptionsWrapper : ConsoleFormatterOptions
    {
        public string? CustomPrefix { get; set; }

        public string? CustomSuffix { get; set; }
    }

    public class TimePrefixConsoleFormatter : ConsoleFormatter, IDisposable
    {
        private readonly IDisposable _optionsReloadToken;
        private ConsoleFormatterOptionsWrapper _formatterOptions;

        public TimePrefixConsoleFormatter(IOptionsMonitor<ConsoleFormatterOptionsWrapper> options)
            // Case insensitive
            : base(nameof(TimePrefixConsoleFormatter)) =>
                (_optionsReloadToken, _formatterOptions) = (options.OnChange(ReloadLoggerOptions), options.CurrentValue);

        private void ReloadLoggerOptions(ConsoleFormatterOptionsWrapper options) => _formatterOptions = options;

        public override void Write<TState>
        (
            in LogEntry<TState> logEntry,
            IExternalScopeProvider scopeProvider,
            TextWriter textWriter)
        {
            string message = logEntry.Formatter(logEntry.State, logEntry.Exception);

            if (message == null)
                return;

            WritePrefix(textWriter);
            textWriter.Write(message);
            WriteSuffix(textWriter);
        }

        private void WritePrefix(TextWriter textWriter)
        {
            DateTime now = _formatterOptions.UseUtcTimestamp
                ? DateTime.UtcNow
                : DateTime.Now;

            textWriter.Write($"{_formatterOptions.CustomPrefix} {now.ToString(_formatterOptions.TimestampFormat)}");
        }

        private void WriteSuffix(TextWriter textWriter) => textWriter.WriteLine($" {_formatterOptions.CustomSuffix}");

        public void Dispose() => _optionsReloadToken?.Dispose();
    }
}
