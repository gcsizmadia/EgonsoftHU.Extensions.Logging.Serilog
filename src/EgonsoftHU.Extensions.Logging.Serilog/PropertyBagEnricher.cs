// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using EgonsoftHU.Extensions.Bcl;

using Serilog.Core;
using Serilog.Events;

namespace EgonsoftHU.Extensions.Logging.Serilog
{
    /// <summary>
    /// Applied during logging to add additional information to log events.
    /// </summary>
    public partial class PropertyBagEnricher : ILogEventEnricher
    {
        private readonly Dictionary<string, LogEventPropertyCreationInfo> properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBagEnricher"/> class.
        /// </summary>
        public PropertyBagEnricher()
        {
            properties = new Dictionary<string, LogEventPropertyCreationInfo>(StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBagEnricher"/> class.
        /// </summary>
        /// <returns>A new instance of the <see cref="PropertyBagEnricher"/> class.</returns>
        public static PropertyBagEnricher Create()
        {
            return new PropertyBagEnricher();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBagEnricher"/> class that
        /// contains the SourceMember property with the specified value of the <paramref name="sourceMember"/> parameter.
        /// </summary>
        /// <param name="sourceMember">The caller member name. By default, it is provided by the <see cref="CallerMemberNameAttribute"/>.</param>
        /// <returns>A new instance of the <see cref="PropertyBagEnricher"/> class.</returns>
        public static PropertyBagEnricher CreateForSourceMember([CallerMemberName] string? sourceMember = null)
        {
            return new PropertyBagEnricher().Here(sourceMember);
        }

        /// <summary>
        /// Add a property that will be added to all log events enriched by this enricher.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">
        /// Whether to destructure the value. See https://github.com/serilog/serilog/wiki/Structured-Data
        /// </param>
        /// <returns>The enricher instance, for chaining Add operations together.</returns>
        public PropertyBagEnricher Add(string propertyName, object? value, bool destructureObjects = false)
        {
            propertyName.ThrowIfNullOrWhiteSpace();

            if (!properties.ContainsKey(propertyName))
            {
                properties.Add(
                    propertyName,
                    new LogEventPropertyCreationInfo(propertyName, value, destructureObjects)
                );
            }

            return this;
        }

        /// <inheritdoc/>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.ThrowIfNull();
            propertyFactory.ThrowIfNull();

            foreach (LogEventPropertyCreationInfo property in properties.Values)
            {
                logEvent.AddPropertyIfAbsent(
                    propertyFactory.CreateProperty(
                        property.PropertyName,
                        property.Value,
                        property.DestructureObjects
                    )
                );
            }
        }
    }
}
