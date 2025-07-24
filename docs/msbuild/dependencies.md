# Using Dependencies

KSPBuildTools can help manage other mods that you depend on

## Referencing Dependency DLLs

Mod DLLs should be referenced with the {confval}`ModReference` item, like so. 

```xml
<ItemGroup>
  <!-- Depends on Modulemanager and Harmony -->
  <ModReference Include="Modulemanager">
    <DLLPath>GameData/Modulemanager*.dll</DLLPath>
  </ModReference>
  <ModReference Include="0Harmony">
    <DLLPath>GameData/000_Harmony/0Harmony.dll</DLLPath>
  </ModReference>
</ItemGroup>
```

## Installing Dependencies Automatically

KSPBuildTools can install CKAN mods automatically when built. This is useful for CI workflows such as those using the {gha:action}`compile` action, or to make it easier for others to compile your mod themselves. Either add the `<CKANIdentifier>` metadata to your `<ModReference>` items, or if the dependency mod doesn't have a dll you need to reference you can use the `<CKANDependency>` item.

```xml
<ItemGroup>
  <!-- Depends on Modulemanager -->
  <ModReference Include="Modulemanager">
    <DLLPath>GameData/Modulemanager*.dll</DLLPath>
    <CKANIdentifier>ModuleManager</CKANIdentifier>
  </ModReference>
  
  <!-- Also depends on Tantares SP, which doesn't have a plugin -->
  <CKANDependency Include="TantaresSP"/>
</ItemGroup>
```

You can also mark explicit versions to install. 

```xml
<ItemGroup>
  <!-- Depends on Modulemanager 4.2.3 ONLY -->
  <ModReference Include="Modulemanager">
    <DLLPath>GameData/Modulemanager*.dll</DLLPath>
    <CKANIdentifier>ModuleManager</CKANIdentifier>
    <CKANVersion>4.2.3</CKANVersion>
  </ModReference>
  
  <!-- Also depends on Tantares SP, which doesn't have a plugin -->
  <CKANDependency Include="TantaresSP==6.0"/>
</ItemGroup>
```

## Generating KSP Dependency Attributes

KSP mods should mark their dependency DLLs using the `KSPAssemblyDependency` attribute.

`[assembly: KSPAssemblyDependency("0Harmony", 0, 0, 0)]`{l=csharp}

If the {confval}`KSPBT_GenerateDependencyAttributes` property is set to `true` (the default), KSPBuildTools will generate these attributes automatically.

The assembly name is taken from the value of the item.

The version is taken from the `<KSPAssemblyVersion>` metadata value, however leaving it at the default of 0.0.0 is usually acceptable

```xml
<ItemGroup>
  <!-- Depends on Tweakscale -->
  <Reference Include="Scale">
    <DLLPath>GameData/TweakScale/plugins/Scale.dll</DLLPath>
    <CKANIdentifier>TweakScaleRescaled</CKANIdentifier>
    <KSPAssemblyVersion>3.2.0</KSPAssemblyVersion>
  </Reference>
</ItemGroup>
```