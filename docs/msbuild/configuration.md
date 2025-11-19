# Configuration Reference

## Properties

```{confval} KSPBT_ModRoot
---
default: `$(MSBuildProjectDir)/../GameData/$(MSBuildProjectName)/`
---

specifies the root directory of your mod (the folder that gets placed into GameData).  Generally you'll want to set this to be relative to the csproj file using `$(MSBuildThisFileDirectory)`.
```

```{confval} KSPBT_ModPluginFolder
---
default: `./`
---

the directory where compiled binaries should be copied. This is relative to the {confval}`KSPBT_ModRoot`. The DLLs will be copied to this directory after each build.
```

```{confval} KSPBT_CKANCompatibleVersions
---
default: `1.12 1.11 1.10 1.9 1.8`
---

Used by the `CKANInstall` target to set additional KSP versions to treat as compatible when installing 
```

```{confval} KSPBT_ReferenceSystemAssemblies
---
default: `true`
---
If set to `true`, adds assembly references to Mono System DLLs.
```

```{confval} KSPBT_ReferenceUnityAssemblies
---
default: `true`
---
If set to `true`, adds assembly references to all UnityEngine assemblies in the KSP install. 
```

```{confval} KSPBT_ReferenceGameAssemblies
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, adds references to Assembly-CSharp and Assembly-CSharp-firstpass assemblies from the KSP install.
```

```{confval} KSPBT_ReferenceModAssemblies
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, adds references to the assemblies included in `ModReference` list.
```

```{confval} KSPBT_GenerateAssemblyAttribute
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically generates the `KSPAssembly` for your assembly from the `Version` property.
```

```{confval} KSPBT_GenerateDependencyAttributes
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically generates `KSPAssemblyDependency` attributes for each dependency. Dependencies should have either the `CKANIdentifier` metadata or `KSPAssemblyName` metadata. Versions can be supplied with `CKANVersion` or `KSPAssemblyVersion`. 
```

```{confval} KSPBT_CopyDLLsToPluginFolder
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically copies the compiled DLL to the {confval}`KSPBT_ModPluginFolder`.
```

```{confval} KSPBT_GenerateVersionFile
---
default: `true`, except in the `Unity` configuration
---
If set to `true`, automatically generates a version file using the information in any {confval}`KSPVersionFile` items. Without defining a {confval}`KSPVersionFile`, nothing will be generated. See [](generating-version-files.md/) for more details. 
```

````{confval} KSPBT_GameRoot
```{warning}
You should **not** set or use this property in your csproj file.
```
This property should be set to the root directory of your KSP install. see [Locating your KSP Install](ksp-install)
````

## Items

````{confval} ModReference
A reference to another mod that is a dependency. This mod will be automatically referenced in the build process and installed using CKAN if an identifier is given. See [](dependencies.md) for examples.

```{rubric} Metadata
```

```{describe} Identity
The name of the mod you are referencing, as set in that mod's `KSPAssemblyAttribute`
```

```{describe} DLLPath
The path of the mod's assembly to reference when building, relative to {confval}`KSPBT_GameRoot`.
```

```{describe} CKANIdentifier
The name of the mod in CKAN to install before building.
```

```{describe} CKANVersion
The specific version to install from CKAN, if any.
```

Any additional metadata is copied to the resulting [`Reference`](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#reference) item 
````

````{confval} KSPVersionFile
Defines a version file to generate. See [](generating-version-files.md) for examples.

```{rubric} Metadata
```

```{describe} Identity
To create a new version file from scratch, set to `.`. Otherwise set to a path to a json version file to use as a base
```
```{describe} Destination
Path to where the generated json version file should be placed
```

```{confval} Name
---
default: `$(ProjectName)`
---
The mod name. Corresponds to the `NAME` value in json.
```

```{confval} Version
---
default: `$(FileVersion)`
---
The mod version. Corresponds with the `VERSION` value in json.
```

```{describe} URL
The URL of the remote version file. Corresponds with the `URL` value in json.
```

```{describe} Download
Where to link players to update your mod. Corresponds with the `DOWNLOAD` value in json.
```

```{confval} KSP_Version
---
default: `1.12`
---
The KSP version the mod is targeting. Corresponds with the `KSP_VERSION` value in json.
```

```{confval} KSP_Version_Min
---
default: `1.8`
---
The minimum supported KSP version. Corresponds with the `KSP_VERSION_MIN` value in json.
```

```{confval} KSP_Version_Max
---
default: `1.12`
---
The maximum supported KSP version. Corresponds with the `KSP_VERSION_MAX` value in json.
```

````
