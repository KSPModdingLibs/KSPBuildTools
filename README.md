# KSPBuildTools

This repository aims to provide a common set of tools for developing mods for Kerbal Space Program.

## KSPCommon.props

This is a [msbuild](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-concepts) property file that you can include in your .csproj files.

What it does:

- Provides a standard way to find your KSP install and the game's libraries that works on anyone's machine and any operating system
- Provides a way for individual developers to select their KSP install location without modifying git-tracked files
- Properties can be set for the entire mod instead of needing to do it per project file
- Provides a standard way to copy output files that works on all operating systems
- Sets the debug symbols format to portable so that you can [debug your mod](https://gist.github.com/gotmachine/d973adcb9ae413386291170fa346d043)
- Sets up Visual Studio's debugging start actions so you can launch KSP directly from VS
- Designed to be used by the [Build github workflow](#build-workflow)

To use it, copy this file somewhere into your repository and add the following in your .csproj file where you would normally have assembly references.  You should remove ALL the existing assembly references to `System`, `Assembly-CSharp`, and `Unity`.

```xml
<Import Project="$(SolutionDir)KSPCommon.props" />
```

Here's an example from [RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/a8baf7a5e1a8915b640ea85a15c221433005632c/RasterPropMonitor/RasterPropMonitor.csproj#L57).

The following properties are exposed to be customized per mod, project, or user.  Properties that represent directories should *not* include a trailing slash.

#### `RepoRootPath`

Default value: `$(SolutionDir)`

This specifies the root directory of your mod repository.

#### `BinariesOutputRelativePath`

Default value: `GameData\$(SolutionName)`

This is the directory where compiled binaries should be copied.  This is relative to the `RepoRootPath`

#### `KSPRoot`

Default value: `$(ReferencePath)` or `C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program`

The root directory of a KSP installation.  This should be customized in a `.props.user` file - or set the Reference Path of the project to your ksp install root.

### Customization

Properties can be customized at several points:

- Per-project properties should be set in the `.csproj` file before importing `KSPCommon.props`
- Per-mod properties for mods with more than one `.csproj` file should be set in `$(SolutionName).props` which will be imported by `KSPCommon.props`
- Per-user properties should be set in `KSPCommon.props.user`.  This is usually where you want to set the path to your KSP installation.  You should have `.user` files added to your `.gitignore` file.

## Build Workflow

This is a reusable github workflow that can compile a mod and produce a release zip file.  This is very much a work in progress.
