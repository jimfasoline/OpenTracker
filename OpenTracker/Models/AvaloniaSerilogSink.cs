﻿using Avalonia.Controls;
using Avalonia.Logging;
using Serilog;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    /// <summary>
    /// This class contains logic for converting Avalonia logs to Serilog file logs.
    /// </summary>
    public class AvaloniaSerilogSink : ILogSink
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">
        /// The file path to which the logs will output.
        /// </param>
        /// <param name="minimumLevel">
        /// The minimum logging level to output.
        /// </param>
        public AvaloniaSerilogSink(string file, Serilog.Events.LogEventLevel minimumLevel)
        {
            _logger = new LoggerConfiguration().WriteTo.File(
                file, minimumLevel, rollingInterval: RollingInterval.Day).CreateLogger();
        }
        
        public bool IsEnabled(LogEventLevel level, string area)
        {
            return _logger.IsEnabled((Serilog.Events.LogEventLevel)level);
        }

        public void Log(LogEventLevel level, string area, object source, string messageTemplate)
        {
            Log(level, area, source, messageTemplate, Array.Empty<object>());
        }

        public void Log<T0>(
            LogEventLevel level, string area, object source, string messageTemplate,
            T0 propertyValue0)
        {
            _ = propertyValue0 ?? throw new ArgumentNullException(nameof(propertyValue0));

            Log(level, area, source, messageTemplate, new object[] { propertyValue0 });
        }

        public void Log<T0, T1>(
            LogEventLevel level, string area, object source, string messageTemplate,
            T0 propertyValue0, T1 propertyValue1)
        {
            _ = propertyValue0 ?? throw new ArgumentNullException(nameof(propertyValue0));
            _ = propertyValue1 ?? throw new ArgumentNullException(nameof(propertyValue1));

            Log(
                level, area, source, messageTemplate,
                new object[] { propertyValue0, propertyValue1 });
        }

        public void Log<T0, T1, T2>(
            LogEventLevel level, string area, object source, string messageTemplate,
            T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _ = propertyValue0 ?? throw new ArgumentNullException(nameof(propertyValue0));
            _ = propertyValue1 ?? throw new ArgumentNullException(nameof(propertyValue1));
            _ = propertyValue2 ?? throw new ArgumentNullException(nameof(propertyValue2));

            Log(
                level, area, source, messageTemplate,
                new object[] { propertyValue0, propertyValue1, propertyValue2 });
        }

        public void Log(LogEventLevel level, string area, object source, string messageTemplate, params object[] propertyValues)
        {
            for (int i = 0; i < propertyValues.Length; i++)
            {
                propertyValues[i] = GetHierarchy(propertyValues[i]);
            }

            _logger.Write((Serilog.Events.LogEventLevel)level, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Gets the hierarchy of the Avalonia control object.
        /// </summary>
        /// <param name="source">
        /// An Avalonia control from which the log is generated as an object.
        /// </param>
        /// <returns>
        /// A string representing the visual hierarchy of the control as an object.
        /// </returns>
        private static object GetHierarchy(object source)
        {
            if (source is IControl visual)
            {
                var visualString = visual.ToString() ??
                    throw new NullReferenceException();

                var hierarchy = new List<string>
                {
                    visualString
                };

                while ((visual = visual.Parent) != null)
                {
                    visualString = visual.ToString() ??
                        throw new NullReferenceException();

                    hierarchy.Insert(0, visualString);
                }

                return string.Join("/", hierarchy);
            }

            return source;
        }
    }
}
