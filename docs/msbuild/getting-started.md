# Getting Started


## Including the Package
There are several ways to use the KSPBuildTools package in your mod.  [KSPBuildTools is available on NuGet](https://www.nuget.org/packages/KSPBuildTools), which is the recommended way to use it.

A detailed walkthrough for creating a new mod project using KSPBuildTools can be found [here](https://github.com/KSPModdingLibs/KSPModdingWiki/wiki/Creating-a-new-Plugin-Mod).

### Install with NuGet - Older Framework Style projects

1. Right-click on your project file in Visual Studio and select "Manage NuGet Packages."
2. Search for KSPBuildTools
3. Optionally, check "Include prerelease" to get access to the bleeding edge of KSPBuildTools
4. Click "Install"

### Install with NuGet - SDK Style projects

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
git submodule add https://github.com/KSPModdingLibs/KSPBuildTools.git
{% if git_ref %}git -C KSPBuildTools/ fetch{% endif %}
{% if git_ref %}git -C KSPBuildTools/ checkout {{git_ref}}{% endif %}
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

### Remove existing assembly references

If you're adding KSPBuildTools to an existing mod, or if you just started a blank project, you likely have some assembly dependencies already in your csproj file.  You need to remove these.  KSPBuildTools will automatically reference the KSP, Unity, and Mono assemblies that are part of your KSP install.  There are a few small differences between those Mono libraries and the regular .net framework ones.

1. Right-click on your project and select "Unload"
2. Right-click again and select "edit"
3. Remove ALL `<Reference>` items
4. Right-click on your project and reload

## Locating your KSP Install

KSPBuildTools needs to know where you have KSP installed in order to reference the game dlls. These are all specific to your own computer, and should not be included in your git repo.

There are several options for this. KSPBuildTools will choose in the following order. Either [autodiscovery in the solution directory](#solution-directory) or [setting a reference path in a .user file](#environment-variable) are the recommended methods for most users.

### KSPBT_GameRoot MSBuild Property

If the {confval}`KSPBT_GameRoot` MSBuild property is already set, KSPBuildTools will use it as-is. This can be set in your .csproj.user file.

### Environment Variable

Set the {envvar}`KSP_ROOT` environment variable to the root of a KSP install. This is useful for CI workflows such as those using the {gha:action}`compile` action.

### Solution Directory

KSPBuildTools will look for a "KSP" directory in your solution directory and use it if it is a valid install. It identifies valid installs by looking for `assembly-csharp.dll` in the appropriate subdirectory for your operating system.

### Reference Path

KSPBuildTools will use the `ReferencePath` MSBuild property if it is a valid KSP install. This can be set in a user file located at `{csproj path}.user`. If you use Visual Studio, it can generate this file and property for you.

### From Steam

KSPBuildTools will use the default Steam install location if it is a valid install
