This repository aims to provide a common set of tools for developing mods for Kerbal Space Program.

# KSPCommon.props

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

# update-version.sh

This is a bash shell script that can help with updating version numbers in several text files (AssemblyInfo.cs, .version files, readme, etc).  All it does is process files with a common extension and replaces certain tokens in them.

You can copy this file to your own mod repo to use it locally, and it's also used in some of the github actions and workflows.  If you're already using git for windows, you have a bash shell that you can run this from.

## Usage

`update-version.sh VERSION_STRING TEMPLATE_EXTENSION [FILES]`

Example:

`update-version.sh 1.2.3.4 .vtxt AssemblyInfo.cs.vtxt`

Example template files:

* [AssemblyInfo from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/SharedAssemblyInfo.cs.versiontemplate)
* [.version file from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/GameData/JSI/RasterPropMonitor/RasterPropMonitor.version.versiontemplate)

### `VERSION_STRING`

**Required.** This is the new version you want to set, in MAJOR.MINOR.PATCH.BUILD form.  Any of the values can be omitted (starting from the right).

### `TEMPLATE_EXTENSION`

**Optional.** Default: `.versiontemplate`

This is a file extension that indicates which files the script should consider.  When processing a file, if it ends with this extension then the script will store the output in a new file with the template extension removed.  Otherwise the file is updated in-place.

### `FILES`

**Optional.**

This is a list of files to process.  If omitted, the script will process all files in the subtree that end with `TEMPLATE_EXTENSION`.

## Tokens

The script will replace the following tokens in processed files:

* `@VERSION_STRING@` : replaced with the raw `VERSION_STRING` 
* `@VERSION_FULL@` : replaced with `VERSION_MAJOR.VERSION_MINOR.VERSION_PATCH.VERSION_BUILD`.  That is, it is `VERSION_STRING` with all non-digit characters removed
* `@VERSION_MAJOR@` : replaced with the major version number
* `@VERSION_MINOR@` : replaced with the minor version number
* `@VERSION_PATCH@` : replaced with the patch version number
* `@VERSION_BUILD@` : replaced with the build version number

# Build Workflow

This is a reusable github workflow that can compile a mod and produce a release zip file.  This is very much a work in progress.
