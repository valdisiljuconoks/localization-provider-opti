// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using DbLocalizationProvider.AspNetCore;
using DbLocalizationProvider.AspNetCore.ClientsideProvider;
using Microsoft.AspNetCore.Builder;

namespace DbLocalizationProvider.EPiServer.ClientsideProvider
{
    /// <summary>
    /// Analyzer is happy now
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds clientside localization provider to your app.
        /// </summary>
        /// <param name="builder">Application builder.</param>
        /// <returns>The same builder to support fluent API configuration.</returns>
        public static IApplicationBuilder UseEpiserverDbLocalizationClientsideProvider(this IApplicationBuilder builder)
        {
            builder.UseDbLocalizationClientsideProvider();

            return builder;
        }
    }
}
