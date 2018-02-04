Add-Type -assembly "system.io.compression.filesystem"

cd .\.nuget

# episerver libraries

.\nuget.exe pack ..\src\DbLocalizationProvider.EPiServer\DbLocalizationProvider.EPiServer.csproj -Properties Configuration=Release #-IncludeReferencedProjects

$moduleName = "DbLocalizationProvider.AdminUI.EPiServer"
$source = $PSScriptRoot + "\src\" + $moduleName + "\modules\_protected\" + $moduleName
$destination = $PSScriptRoot + "\src\" + $moduleName + "\" + $moduleName + ".zip"

If(Test-path $destination) {Remove-item $destination}
[io.compression.zipfile]::CreateFromDirectory($Source, $destination)

.\nuget.exe pack ..\src\DbLocalizationProvider.AdminUI.EPiServer\DbLocalizationProvider.AdminUI.EPiServer.csproj -Properties Configuration=Release #-IncludeReferencedProjects
.\nuget.exe pack ..\src\DbLocalizationProvider.EPiServer.JsResourceHandler\DbLocalizationProvider.EPiServer.JsResourceHandler.csproj -Properties Configuration=Release #-IncludeReferencedProjects
.\nuget.exe pack ..\src\DbLocalizationProvider.AdminUI.EPiServer.Xliff\DbLocalizationProvider.AdminUI.EPiServer.Xliff.csproj -Properties Configuration=Release #-IncludeReferencedProjects
.\nuget.exe pack ..\src\DbLocalizationProvider.MigrationTool\DbLocalizationProvider.MigrationTool.csproj -Properties Configuration=Release -tool

cd ..\
