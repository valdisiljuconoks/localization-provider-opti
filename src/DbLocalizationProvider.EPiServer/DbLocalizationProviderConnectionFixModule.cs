// Copyright (c) 2019 Valdis Iljuconoks.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Configuration;
using EPiServer.Data.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace DbLocalizationProvider.EPiServer
{
    /// <summary>
    ///     Added default connection name fix module for EPiServer.
    ///     This is due to the fact that DbLocalizationProvider might be called before init method is run
    ///     (for example - when display channels get initialized and Episerver calls localization provider to get display
    ///     name).
    ///     Related to #114
    /// </summary>
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class DbLocalizationProviderConnectionFixModule : IConfigurableModule
    {
        public void Initialize(InitializationEngine context) { }

        public void Uninitialize(InitializationEngine context) { }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            //var epiDataOptions = context.Locate.Advanced.GetInstance<DataAccessOptions>();
            ConfigurationContext.Setup(ctx => { ctx.Connection = EPiServerDataStoreSection.Instance.DataSettings.ConnectionStringName; });

            foreach(ConnectionStringSettings connectionStringSettings in ConfigurationManager.ConnectionStrings)
                if(string.Equals(connectionStringSettings.Name, ConfigurationContext.Current.Connection, StringComparison.InvariantCultureIgnoreCase))
                    ConfigurationContext.Current.DbContextConnectionString = ConfigurationManager.ConnectionStrings[ConfigurationContext.Current.Connection].ConnectionString;
        }
    }
}
