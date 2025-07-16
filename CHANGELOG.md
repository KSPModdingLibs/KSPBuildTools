# Changelog

All notable changes to this project will be documented in this file

## Unreleased

### Msbuild

- Renamed global msbuild properties to have the `KSPBT` prefix to avoid namespace collisions with other frameworks
  - `KSPRoot` is now `KSPBTGameRoot`. It should no longer be referenced within a .csproj file
  - `RepoRootPath` is now `KSPBTModRoot`, and should now point to the mod folder within GameData rather than the
    root of a git repo
  - `BinariesOutputRelativePath` is now `KSPBTModPluginFolder`
  - `GenerateKSPAssemblyAttribute` is now `KSPBTGenerateAssemblyAttribute` and defaults to true
  - `GenerateKSPAssemblyDependencyAttributes` is now `KSPBTGenerateDependencyAttributes` and defaults to true
  - `ReferenceUnityAssemblies` is now `KSPBTReferenceUnityAssemblies`
  - `ReferenceKSPAssemblies` is now `KSPBTReferenceGameAssemblies`
- Added the `KSPBTReferenceSystemAssemblies` property to control referencing the mono system DLLs within the KSP
  managed folder. Setting this property to false will load the implicit framework DLLs instead.
- Mod dependencies should now be declared with
  `ModReference` items. This avoids the need for the KSP install path to be known at evaluation time.


## 0.0.4 - 2025-06-15

### Library

* Added a logging utility for use by mods

### Actions

* `compile` now uses `dotnet msbuild` to build the project
* Dotnet compiler version can be specified in the `compile` options

### Build

* Fixed several places in KSPCommon.targets that didn't check for empty values properly
* Support all forms of version numbers
  * KSPAssembly and KSPAssemblyDependency may optionally be major.minor (omitting patch)
  * KSPVersionFile.Version now defaults to $(FileVersion) if not set, which should support any number of elements
* ProjectReference may now include <KSPAssemblyName> which will generate a `KSPAssemblyDependency` attribute
* Added `ReferenceUnityAssemblies` and `ReferenceKSPAssemblies` for disabling the automatic inclusion of Unity and KSP assembly references
* Prevented automatic inclusion of mscorlib from nuget on some compiler versions


## 0.0.3 - 2024-12-16

### Actions

- Made `compile` step of `create-release` conditional based on the `use-msbuild` input

### Build

- Fixed directories not being copied to output (#38)
- Fixed error when `GenerateKSPAssemblyAttribute` is unset


## 0.0.2 - 2024-10-21

first stable release