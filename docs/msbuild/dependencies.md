# Using Dependencies

KSPBuildTools can help manage other mods that you depend on

## Referencing Dependency DLLs

Mod DLLs should be referenced as with any other DLLs, like so. See [the Microsoft docs on Reference items](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#reference) for more info. Make sure that `<Private>False</Private>`{l=xml} is set or the DLL will be copied to your output directory.

the {confval}`KSPBTGameRoot` property can be used to reference the KSP install wherever it is. 
```xml
<ItemGroup>
  <!-- Depends on Modulemanager and Harmony -->
  <Reference Include="$(KSPBTGameRoot)/Modulemanager*.dll">
    <Private>False</Private>
  </Reference>
  <Reference Include="0Harmony, Culture=neutral, PublicKeyToken=null">
    <HintPath>$(KSPBTGameRoot)/GameData/000_Harmony/0Harmony.dll</HintPath>
    <Private>False</Private>
  </Reference>
</ItemGroup>
```

## Installing Dependencies Automatically

KSPBuildTools can install CKAN mods automatically when built. This is useful for CI workflows such as those using the {gha:action}`compile` action, or to make it easier for others to compile your mod themselves. Either add the `<CKANIdentifier>` metadata to your `<Reference>` items, or if the dependency mod doesn't have a dll you need to reference you can use the `<CKANDependency>` item.

```xml
<ItemGroup>
  <!-- Depends on Modulemanager -->
  <Reference Include="$(KSPBTGameRoot)/Modulemanager*.dll">
    <Private>False</Private>
    <CKANIdentifier>ModuleManager</CKANIdentifier>
  </Reference>
  
  <!-- Also depends on Tantares SP, which doesn't have a plugin -->
  <CKANDependency Include="TantaresSP"/>
</ItemGroup>
```

You can also mark explicit versions to install. 

```xml
<ItemGroup>
  <!-- Depends on Modulemanager 4.2.3 ONLY -->
  <Reference Include="$(KSPBTGameRoot)/Modulemanager*.dll">
    <Private>False</Private>
    <CKANIdentifier>ModuleManager</CKANIdentifier>
    <CKANVersion>4.2.3</CKANVersion>
  </Reference>
  
  <!-- Also depends on Tantares SP, which doesn't have a plugin -->
  <CKANDependency Include="TantaresSP==6.0"/>
</ItemGroup>
```

## Generating KSP Dependency Attributes

KSP mods should mark their dependency DLLs using the `KSPAssemblyDependency` attribute.

`[assembly: KSPAssemblyDependency("0Harmony", 0, 0, 0)]`{l=csharp}

If the {confval}`KSPBTGenerateDependencyAttributes` property is set to `true`, KSPBuildTools will generate these attributes automatically. It uses any `Reference` item that has a `<CKANIdentifier>` or `<KSPAssemblyName>` metadata value.

The assembly name is set to the `<KSPAssemblyName>` metadata value, and falls back to the `<CKANIdentifier>` metadata value. 

The version is taken from the `<KSPAssemblyVersion>` metadata value, however leaving it at the default of 0.0.0 is usually acceptable

```xml
<ItemGroup>
  <!-- Depends on Tweakscale -->
  <Reference Include="$(KSPBTGameRoot)/GameData/TweakScale/plugins/Scale.dll">
    <Private>False</Private>
    <CKANIdentifier>TweakScaleRescaled</CKANIdentifier>
    <KSPAssemblyName>Scale</KSPAssemblyName>
    <KSPAssemblyVersion>3.2.0</KSPAssemblyVersion>
  </Reference>
</ItemGroup>

<PropertyGroup>
  <!-- Generate assembly attributes -->
  <KSPBTGenerateAssemblyAttribute>true</KSPBTGenerateAssemblyAttribute>
  <KSPBTGenerateDependencyAttributes>true</KSPBTGenerateDependencyAttributes>
</PropertyGroup>
```