// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System.Runtime.CompilerServices;

using EgonsoftHU.Extensions.Bcl;

namespace EgonsoftHU.Extensions.Logging.Serilog
{
    /// <summary>
    /// Contains the extension methods for the <see cref="PropertyBagEnricher"/> type.
    /// </summary>
    public static class PropertyBagEnricherExtensions
    {
        /// <summary>
        /// Adds the caller member name (i.e. where to log event occurs) as a property
        /// that will be added to all log events enriched by this enricher.
        /// </summary>
        /// <param name="enricher">An instance of the <see cref="PropertyBagEnricher"/> class.</param>
        /// <param name="sourceMember">The caller member name. By default, it is provided by the <see cref="CallerMemberNameAttribute"/>.</param>
        /// <returns>The enricher instance.</returns>
        /// <remarks>
        /// The log event property name is <c>SourceMember</c> by default.<br/>
        /// It can be customized by setting the <c>SourceMemberNamingConfiguration.Current.PropertyName</c> property.
        /// </remarks>
        public static PropertyBagEnricher Here(this PropertyBagEnricher enricher, [CallerMemberName] string sourceMember = null)
        {
            enricher.ThrowIfNull(nameof(enricher));

            return enricher.Add(SourceMemberNamingConfiguration.Current.PropertyName, sourceMember);
        }
    }
}
