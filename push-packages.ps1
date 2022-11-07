cd .\.nuget

nuget push .\DbLocalizationProvider.EPiServer.7.5.0.nupkg -source https://nuget.episerver.com/feed/packages.svc
nuget push .\DbLocalizationProvider.EPiServer.JsResourceHandler.7.5.0.nupkg -source https://nuget.episerver.com/feed/packages.svc
nuget push .\DbLocalizationProvider.AdminUI.EPiServer.7.5.0.nupkg -source https://nuget.episerver.com/feed/packages.svc/
nuget push .\DbLocalizationProvider.AdminUI.EPiServer.Xliff.7.5.0.nupkg -source https://nuget.episerver.com/feed/packages.svc/
nuget push .\DbLocalizationProvider.MigrationTool.7.5.0.nupkg -source https://nuget.episerver.com/feed/packages.svc/

cd ..\
