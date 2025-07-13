# Generating Version Files

KSPBuildTools can generate .version files for use with KSP-AVC and CKAN

To use, add the following to your csproj, filling in the URLs and paths for your own mod:

```xml
<!-- Version Files -->
<ItemGroup>
  <KSPVersionFile Include=".">
    <Destination>$(KSPModRoot)/mymod.version</Destination>
    <URL>https://github.com/username/repo/releases/latest/download/mymod.version</URL>
    <Download>https://github.com/username.repo/releases/latest</Download>
  </KSPVersionFile>
</ItemGroup>
```

The resulting file looks like this:

```json
{
  "NAME": "my-mod",
  "VERSION": "1.0.0",
  "URL": "https://github.com/username/repo/releases/latest/download/mymod.version",
  "DOWNLOAD": "https://github.com/username.repo/releases/latest",
  "KSP_VERSION": "1.12",
  "KSP_VERSION_MIN": "1.8",
  "KSP_VERSION_MAX": "1.12"
}

```

The `<Destination>` node is where the resulting .version file will be generated when your mod is compiled. It should be added to your .gitignore file.

The `VERSION` field is filled in with the `$(FileVersion)` MSBuild variable. This can be overridden with a `<Version>` metadata node within `<KSPVersionFile>`.

The `KSP_VERSION`, `KSP_VERSION_MIN` and `KSP_VERSION_MAX` fields default to compatibility with ksp 1.8 to 1.12. These values can be overridden with the `<KSP_Version>`, `<KSP_Version_Min>` and `<KSP_Version_Max>` metadata fields.

## Starting from an existing file

The `include` value can also be set to a json file including any other values you may want to include, with just the above properties updated before writing to the destination

```xml
<!-- Version Files -->
<ItemGroup>
  <KSPVersionFile Include="my-mod.version">
    <Destination>$(KSPModRoot)/mymod.version</Destination>
  </KSPVersionFile>
</ItemGroup>
```