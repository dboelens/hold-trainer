# IFR Hold Trainer
[![Test Pipeline](https://github.com/dboelens/hold-trainer/actions/workflows/dotnet.yml/badge.svg)](https://github.com/dboelens/hold-trainer/actions/workflows/dotnet.yml)

This application allows IFR students to receive a random hold clearance draw that clearance on their iPad.

## Install Instructions
Requires
- Dotnet 8.0
- MAUI
- XCode

For installing MAUI, refer to their installation [instructions](https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation?view=net-maui-8.0&tabs=vswin)

## Building
To build the code, run the following dotnet command

```
dotnet build
```

## Publishing
To publish, run the following dotnet command

```
dotnet publish -f net8.0-ios -c Release -p:ArchiveOnBuild=true -p:CodesignKey="{your code signing key}" -p:CodesignProvision="{your code signing provision profile} 
```