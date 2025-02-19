# MSBuild Properties

```{confval} KSPRoot
This property should be set to the root directory of your KSP install. You should not set this in your csproj, see [Locating your KSP Install](getting-started.md/#locating-your-ksp-install)
```

```{confval} RepoRootPath
---
default: `$(SolutionDir)`
---

specifies the root directory of your mod repository.  Generally you'll want to set this to be relative to the csproj file using `$(MSBuildThisFileDirectory)`.
```

```{confval} BinariesOutputRelativePath
---
default: `GameData/$(SolutionName)`
---

the directory where compiled binaries should be copied.  This is relative to the `RepoRootPath`.  The binaries will be copied to this directory after each build.
```

```{confval} CKANCompatibleVersions
---
default: `1.12 1.11 1.10 1.9 1.8`
---

Used by the `CKANInstall` target to set additional KSP versions to treat as compatible when installing 
```

```{confval} GenerateKSPAssemblyAttribute
If set to `true`, automatically generates the `KSPAssembly` for your assembly from the `Version` property.
```

```{confval} GenerateKSPAssemblyDependencyAttributes
If set to `true`, automatically generates `KSPAssemblyDependency` attributes for each dependency. Dependencies should have either the `CKANIdentifier` metadata or `KSPAssemblyName` metadata. Versions can be supplied with `CKANVersion` or `KSPAssemblyVersion`. 
```

```{confval} ReferenceUnityAssemblies
If set to `true` (default), adds assembly references to all UnityEngine assemblies in the KSP install.  You can set this to `false` to opt out of this behavior if you want to create a pure C# assembly that does not depend on Unity.
```

```{confval} ReferenceKSPAssemblies
If set to `true` (default), adds references to Assembly-CSharp and Assembly-CSharp-firstpass assemblies from the KSP install.  You can set this to `false` to opt out of this behavior.
```
