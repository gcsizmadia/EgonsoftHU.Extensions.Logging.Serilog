﻿// Copyright © 2022-2024 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Runtime.CompilerServices;

using EgonsoftHU.Extensions.Bcl;

using Serilog;

namespace EgonsoftHU.Extensions.Logging
{
    /// <summary>
    /// Contains the extension methods for the <see cref="ILogger"/> type.
    /// </summary>
    public static class ILoggerExtensions
    {
        /// <summary>
        /// Create a logger that enriches log events with the caller member name (i.e. where the log event occurs).
        /// </summary>
        /// <param name="logger">An instance of a type that implements the <see cref="ILogger"/> interface.</param>
        /// <param name="sourceMember">The caller member name. By default, it is provided by the <see cref="CallerMemberNameAttribute"/>.</param>
        /// <returns>A logger that will enrich log events with the caller member name.</returns>
        /// <remarks>
        /// The log event property name is <c>SourceMember</c> by default.<br/>
        /// It can be customized by setting the <c>SourceMemberNamingConfiguration.Current.PropertyName</c> property.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="logger"/> is <see langword="null" />.</exception>
        public static ILogger Here(this ILogger logger, [CallerMemberName] string? sourceMember = null)
        {
            logger.ThrowIfNull();

            return logger.ForContext(SourceMemberNamingConfiguration.Current.PropertyName, sourceMember);
        }
    }
}
