// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using DbLocalizationProvider.AspNetCore;
using EPiServer.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DbLocalizationProvider.EPiServer;

/// <inheritdoc />
public class OptimizelyUsageConfigurator : IUsageConfigurator
{
    /// <inheritdoc />
    public void Configure(ConfigurationContext context, IServiceProvider serviceProvider)
    {
        // here (after container creation) we can "finalize" some of the service setup procedures
        context.Logger = new LoggerAdapter();

        // respect configuration whether we should sync and register resources
        // skip if application currently is in read-only mode
        var dbMode = serviceProvider.GetRequiredService<IDatabaseMode>().DatabaseMode;
        if (context.DiscoverAndRegisterResources)
        {
            context.DiscoverAndRegisterResources = dbMode != DatabaseMode.ReadOnly;
        }
    }
}
