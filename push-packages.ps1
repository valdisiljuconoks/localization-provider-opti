cd .\.nuget

nuget push .\DbLocalizationProvider.EPiServer.5.3.0.nupkg -source https://nuget.episerver.com/feed/packages.svc
nuget push .\DbLocalizationProvider.EPiServer.JsResourceHandler.5.3.0.nupkg -source https://nuget.episerver.com/feed/packages.svc
nuget push .\DbLocalizationProvider.AdminUI.EPiServer.5.3.0.nupkg -source https://nuget.episerver.com/feed/packages.svc/
nuget push .\DbLocalizationProvider.AdminUI.EPiServer.Xliff.5.3.0.nupkg -source https://nuget.episerver.com/feed/packages.svc/
nuget push .\DbLocalizationProvider.MigrationTool.5.3.0.nupkg -source https://nuget.episerver.com/feed/packages.svc/

cd ..\
