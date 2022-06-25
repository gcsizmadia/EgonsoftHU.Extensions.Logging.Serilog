// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Runtime.CompilerServices;

using EgonsoftHU.Extensions.Bcl;

namespace EgonsoftHU.Extensions.Logging.Serilog
{
    /// <summary>
    /// Controls the log event property name for the source member provided by <see cref="CallerMemberNameAttribute"/>.
    /// </summary>
    public class SourceMemberNamingConfiguration
    {
        /// <summary>
        /// The default log event property name for the source member.
        /// </summary>
        public const string DefaultPropertyName = "SourceMember";

        private string currentPropertyName = DefaultPropertyName;

        private SourceMemberNamingConfiguration()
        {
        }

        /// <summary>
        /// Gets the current configuration for the log event property name for the source member.
        /// </summary>
        public static SourceMemberNamingConfiguration Current { get; } = new SourceMemberNamingConfiguration();

        /// <summary>
        /// Gets or sets the log event property name for the source member.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the property name is set to be <c>null</c>, <see cref="String.Empty"/> or consists only of white-space characters.
        /// </exception>
        public string PropertyName
        {
            get => currentPropertyName;
            set
            {
                value.ThrowIfNullOrWhiteSpace();
                currentPropertyName = value;
            }
        }
    }
}
