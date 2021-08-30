Add-Type -assembly "system.io.compression.filesystem"

cd .\.nuget

# episerver libraries

cd .\..\src\DbLocalizationProvider.EPiServer\
dotnet build -c Release
dotnet pack -c Release
# dotnet pack --include-symbols -p:SymbolPackageFormat=snupkg
copy .\bin\Release\DbLocalizationProvider.EPiServer.*.nupkg .\..\..\.nuget\
copy .\bin\Release\DbLocalizationProvider.EPiServer.*.snupkg .\..\..\.nuget\
cd .\..\..\.nuget\

cd .\..\src\DbLocalizationProvider.AdminUI.EPiServer\
dotnet build -c Release
dotnet pack -c Release
# dotnet pack --include-symbols -p:SymbolPackageFormat=snupkg
copy .\bin\Release\DbLocalizationProvider.AdminUI.EPiServer.*.nupkg .\..\..\.nuget\
copy .\bin\Release\DbLocalizationProvider.AdminUI.EPiServer.*.snupkg .\..\..\.nuget\
cd .\..\..\.nuget\

cd ..