# Migration Guide
If you are migrating a mod from a <1.0.0 version KSP Build Tools, follow the instructions below

## MSBuild Properties

- `KSPRoot` was renamed to {confval}`KSPBT_GameRoot`. It should no longer be referenced within a .csproj file
- `RepoRootPath` was renamed to {confval}`KSPBT_ModRoot`, and should now point to the mod folder within GameData rather than the root of a git repo
- `BinariesOutputRelativePath` was renamed to {confval}`KSPBT_ModPluginFolder`
- `GenerateKSPAssemblyAttribute` was renamed to {confval}`KSPBT_GenerateAssemblyAttribute` and defaults to true
- `GenerateKSPAssemblyDependencyAttributes` was renamed to {confval}`KSPBT_GenerateDependencyAttributes` and defaults to true
- `ReferenceUnityAssemblies` was renamed to {confval}`KSPBT_ReferenceUnityAssemblies`
- `ReferenceKSPAssemblies` was renamed to {confval}`KSPBT_ReferenceGameAssemblies`

## MSBuild Items

Dependency mods should now be specified with {confval}`ModReference` items. For example

```xml
<ItemGroup>
  <Reference Include="0Harmony, Culture=neutral, PublicKeyToken=null">
    <HintPath>$(KSPRoot)/GameData/000_Harmony/0Harmony.dll</HintPath>
    <CKANIdentifier>Harmony2</CKANIdentifier>
    <Private>False</Private>
  </Reference>
</ItemGroup>
```

would become

```xml
<ItemGroup>
  <ModReference Include="0Harmony">
    <DLLPath>GameData/000_Harmony/0Harmony.dll</DLLPath>
    <CKANIdentifier>Harmony2</CKANIdentifier>
    <Private>False</Private>
  </ModReference>
</ItemGroup>
```

## CI Actions 

### {gha:action}`compile`

The compile action no longer runs `setup-dotnet`. You need to run this step separately.

If your mod uses a `packages.config` file, you need to specify `use-nuget-restore` like so

```yaml
- uses: KSPModdingLibs/KSPBuildTools/.github/actions/compile@1.0.0
  with:
    use-nuget-restore: 'true'
```

If your mod *does not* use a `packages.config` file, you can migrate your workflows on Github runners to use Ubuntu 24.04 or later.

## Library

If you made use of the logging utilities under the `KSPBuildTools` namespace, you should migrate to [KSPCommunityLib](https://git.offworldcolonies.nexus/KSPModdingLibs/KSPCommunityLib) instead.
