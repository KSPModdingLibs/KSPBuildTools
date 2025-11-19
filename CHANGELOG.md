# Changelog

All notable changes to this project will be documented in this file

## 1.0.0-alpha.3 - 2025-11-14

### Msbuild

- Renamed global msbuild properties to have the `KSPBT_` prefix to avoid namespace collisions with other frameworks
  - `KSPRoot` is now `KSPBT_GameRoot`. It should no longer be referenced within a .csproj file
  - `RepoRootPath` is now `KSPBT_ModRoot`, and should now point to the mod folder within GameData rather than the
    root of a git repo
  - `BinariesOutputRelativePath` is now `KSPBT_ModPluginFolder`
  - `GenerateKSPAssemblyAttribute` is now `KSPBT_GenerateAssemblyAttribute` and defaults to true
  - `GenerateKSPAssemblyDependencyAttributes` is now `KSPBT_GenerateDependencyAttributes` and defaults to true
  - `ReferenceUnityAssemblies` is now `KSPBT_ReferenceUnityAssemblies`
  - `ReferenceKSPAssemblies` is now `KSPBT_ReferenceGameAssemblies`
- Added the `KSPBT_ReferenceSystemAssemblies` property to control referencing the mono system DLLs within the KSP
  managed folder. Setting this property to false will load the implicit framework DLLs instead.
- Mod dependencies should now be declared with
  `ModReference` items. This avoids the need for the KSP install path to be known at evaluation time.
- Fix `KSP_VERSION_MAX` getting mangled when using an existing version file (#64)
- Fix incorrect behavior when building without a solution (#50)

### Actions

- KSPBT actions used in reusable workflows are now pinned with each tag, instead of using actions from `main`. All calls to reusable workflows should be pinned to a tag to ensure the correct actions are being used. (#21)
- `compile` action: Use `dotnet restore` instead of `nuget restore` by default, allowing the action to work on any Ubuntu runner image. Added the `use-nuget-restore` option to restore the previous behavior for projects that use packages.config for dependencies. (#68)
- `compile` action: Removed call to`actions/setup-dotnet`. Setting up .NET should be done as a separate step. (#65)
- `setup-ckan` action: Sped up execution by skipping recommended packages and man-db updates
- `setup-ckan` action: Add `ckan-install-method` option for installation method. Currently supports `'apt'` for installation on Debian/Ubuntu, or `'skip'` to skip installation for runners that already have CKAN installed.
- `assemble-release` action: `outputs.artifact-path` now includes the `.zip` extension (#51)

### Library

- Removed Log.cs and the entire includes directory. Please use [KSPCommunityLib](https://git.offworldcolonies.nexus/KSPModdingLibs/KSPCommunityLib) instead.


## 0.0.5 - 2025-11-07

Several non-breaking bugfixes backported from the next development version

### Docs

- Fixed git submodule example to work even for tagged releases (#49)

### Build

- BACKPORT: Fix `KSP_VERSION_MAX` getting mangled when using an existing version file (#64)

### Actions

- BACKPORT: KSPBT actions used in reusable workflows are now pinned with each tag, instead of using actions from `main`. All calls to reusable workflows should be pinned to a tag to ensure the correct actions are being used. (#21)


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