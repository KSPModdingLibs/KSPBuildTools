# Getting Started


## Including the Package
There are several ways to use the KSPBuildTools package in your mod. If you are using
an [SDK-style csproj file](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview), it is recommended to use NuGet

### Install with NuGet

either run the following command:

````{jinja}
```console
dotnet add package KSPBuildTools{% if nuget_version %} --version {{nuget_version}}{% endif %} 
```
````

or add the following to your csproj:

````{jinja}
```xml
<ItemGroup>
  <PackageReference Include="KSPBuildTools"{% if nuget_version %} Version="{{nuget_version}}"{% endif %}/> 
</ItemGroup>
```
````

### Including the Targets File Directly

If you aren't able to use the NuGet option, you can include the targets file directly using a git submodule.

First, run the following command in your git project root:

````{jinja}
```console
git submodule add{% if git_ref %} --branch {{git_ref}} {% endif %}https://github.com/KSPModdingLibs/KSPBuildTools.git
```
````

Then include the targets file in your csproj. Make sure you use the correct path relative to your project file.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  
  <!-- Project properties and items go here -->  
  
  <Import Project="$(MSBuildThisFileDirectory)../KSPBuildTools/KSPCommon.targets" />
</Project>
```

Or you can copy the files you want into your own repository and use them however you like - though that will make it harder to get updates

## Locating your KSP Install

KSPBuildTools needs to know where you have KSP installed in order to reference the game dlls. These are all specific to your own computer, and should not be included in your git repo.

There are several options for this. KSPBuildTools will choose in the following order. Either [autodiscovery in the solution directory](#solution-directory) or [setting a reference path in a .user file](#environment-variable) are the recommended methods for most users.

### KSPRoot MSBuild Variable

If the {confval}`KSPRoot` MSBuild variable is already set, KSPBuildTools will use it as-is. This can be set in a .user file.

### Environment Variable

Set the {envvar}`KSP_ROOT` environment variable to the root of a KSP install. This is useful for CI workflows such as those using the {gh-actions:action}`compile` action.

### Solution Directory

KSPBuildTools will look for a "KSP" directory in your solution directory and use it if it is a valid install. It identifies valid installs by looking for `assembly-csharp.dll` in the appropriate subdirectory for your operating system.

### Reference Path

KSPBuildTools will use the `ReferencePath` MSBuild variable if it is a valid KSP install. This can be set in a user file located at `{csproj path}.user`. If you use Visual Studio, it can generate this file and variable for you.

### From Steam

KSPBuildTools will use the default Steam install location if it is a valid install
