This repository aims to provide a common set of tools for developing mods for Kerbal Space Program.  Note that it's still in "alpha" stages so expect things to change or break.

To use it, either:

* Add this repository as a submodule:

  `git submodule add https://github.com/KSPModdingLibs/KSPBuildTools.git`

* For the github actions and workflows, you can directly reference this repository from your own workflows.  [This repo](https://github.com/KSPModdingLibs/.github/tree/main/workflow-templates) contains workflow templates that you can start from.
* Or you can copy the files you want into your own repository and use them however you like - though that will make it harder to get updates

Most things in this repository will work best if you have a directory in your repository that corresponds to the directory that the user will install into GameData (or several such directories).  Placing these into a GameData folder in your repository is recommended but not required.

While working on your mod, I recommend that you create a junction or symlink from the game's GameData folder pointing at the content folder in your repository.  That way any changes you make will be immediately available and you don't need to deploy or copy anything.  If you'd like to see other workflows supported please ask!

# KSPCommon.targets

This is a [msbuild](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-concepts) targets file that you can include in your .csproj files.

What it does:

- Provides a standard way to find your KSP install and the game's libraries that works on anyone's machine and any operating system
- Provides a way for individual developers to select their KSP install location without modifying git-tracked files
- Properties can be set for the entire mod instead of needing to do it per project file
- Provides a standard way to copy output files that works on all operating systems
- Sets the debug symbols format to portable so that you can [debug your mod](https://gist.github.com/gotmachine/d973adcb9ae413386291170fa346d043)
- Sets up Visual Studio's debugging start actions so you can launch KSP directly from VS
- Includes a target for installing dependencies with CKAN
- Designed to be used by the [Build github workflow](#compile-action)

To use it, import KSPCommon.targets in your .csproj file after it imports Microsoft.CSharp.targets.  You should remove ALL the existing assembly references to `System`, `Assembly-CSharp`, and `Unity`.

```xml
<Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
<Import Project="$(SolutionDir)KSPBuildTools\KSPCommon.targets" />
```

Here's an example from [kOS](https://github.com/KSP-KOS/KOS/blob/22808556c090ebe63cb96452e43bb224cff4c27e/src/kOS/kOS.csproj#L260).

Note that `KSPCommon.targets` makes use of `KSPCommon.props` for advanced users, which sets all the below properties but does not include the build targets.  If you only want the properties and not the targets, you can use `KSPCommon.props` instead.

The following properties are exposed to be customized per mod, project, or user.  Properties that represent directories should *not* include a trailing slash.

#### `RepoRootPath`

Default value: `$(SolutionDir)`

This specifies the root directory of your mod repository.  Generally you'll want to set this to be relative to `$(SolutionDir)`.

#### `BinariesOutputRelativePath`

Default value: `GameData\$(SolutionName)`

This is the directory where compiled binaries should be copied.  This is relative to the `RepoRootPath`.  The binaries will be copied after each build.

#### `KSPRoot`

This property should be set to the root directory of your KSP install.  If it is not specified, then `KSPCommon.props` will try some defaults:

- If `$(ReferencePath)` is set, then that becomes the value of the `KSPRoot` property.  This is the best way for individual developers to specify where their KSP install is, because the `$(ReferencePath)` is typically stored in the `.csproj.user` file that is not committed to version control.  NOTE: `.csproj.user` files are imported by `Microsoft.CSharp.targets` which is why it's important for `KSPCommon.targets` to be placed *afterwards*.  If it comes first, you won't be able to use `$(ReferencePath)`.
- If `$(SolutionDir)KSP/buildID.txt` exists, then `$(SolutionDir)KSP` becomes the value of the `KSPRoot` property.  This could be a full copy of a KSP install, or just a symlink or junction to one.
- If `KSPRoot` still isn't set, then it will default to `C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program` on Windows or `$(HOME)/Library/Application Support/Steam/steamapps/common/Kerbal Space Program` on OSX.

#### `CKANCompatibleVersions`

Default value: `1.12 1.11 1.10 1.9 1.8`

Used by the `CKANInstall` target to set additional KSP versions to treat as compatible when installing dependencies.

### Customization

Properties can be customized at several points:

- Per-project properties should be set in the `.csproj` file before importing `KSPCommon.targets`
- Per-mod properties for mods with more than one `.csproj` file should be set in `$(SolutionName).props` which will be imported by `KSPCommon.props`
- Per-user properties should be set in `KSPCommon.props.user`.  This is usually where you want to set the path to your KSP installation.  You should have `.user` files added to your `.gitignore` file.

### Referencing Dependencies

Referencing assemblies (DLLs) from other mods should be done with a HintPath relative to `$(KSPRoot)`.  In addition, you can include the CKAN identifier of the mod to make it installable with the `CKANInstall` target.

Example from [Shabby](https://github.com/KSPModdingLibs/Shabby/blob/e61ec5084b83c7e6941e62f43439cdd28fe867e6/Source/Shabby.csproj#L30):

```
    <Reference Include="0Harmony, Culture=neutral, PublicKeyToken=null">
      <HintPath>$(KSPRoot)/GameData/000_Harmony/0Harmony.dll</HintPath>
      <CKANIdentifier>Harmony2</CKANIdentifier>
    </Reference>
```

### CKANInstall target

This is a build target that you can invoke with msbuild to install all the dependencies of your mod using CKAN.  Each dependency should be marked with its `CKANIdentifier` as shown above.  Dependencies will be installed in `$(KSPRoot)`.  It may also be invoked from the [`install-dependencies`](#install-dependencies) action.

Example usage:

```
msbuild -t:CKANInstall
```

# update-version.sh

This is a bash shell script that can update version numbers in several text files (AssemblyInfo.cs, .version files, readme, etc).  All it does is process files with a common extension and replaces certain tokens in them.

This file can be run from your own repository locally, and it's also used in some of the github actions and workflows.  If you're already using git for windows, you have a bash shell that you can run this from.

## Usage

`update-version.sh [-g (true|false)] [-d (true|false)] VERSION_STRING TEMPLATE_EXTENSION [FILES]`

Example:

`update-version.sh 1.2.3.4 .vtxt AssemblyInfo.cs.vtxt`

Example template files:

* [AssemblyInfo.cs from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/SharedAssemblyInfo.cs.versiontemplate)
* [.version file from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/GameData/JSI/RasterPropMonitor/RasterPropMonitor.version.versiontemplate)

### `VERSION_STRING`

**Required.** This is the new version you want to set, in MAJOR.MINOR.PATCH.BUILD form.  Any of the values can be omitted (starting from the right).

### `TEMPLATE_EXTENSION`

**Optional.** Default: `.versiontemplate`

This is a file extension that indicates which files the script should consider.  When processing a file, if it ends with this extension then the script will store the output in a new file with the template extension removed.  Otherwise the file is updated in-place.

### `FILES`

**Optional.**

This is a list of files to process.  If omitted, the script will process all files in the subtree that end with `TEMPLATE_EXTENSION`.

### `-g (true|false)`

**Optional.** Default: false

If true, calls `git add` on each of the modified files (but does not commit).

### `-d (true|false)`

**Optional.** Default: false

If true, deletes the template file after processing (if the file was not updated in-place).

## Tokens

The script will replace the following tokens in processed files:

* `@VERSION_STRING@` : replaced with the raw `VERSION_STRING` 
* `@VERSION_FULL@` : replaced with `VERSION_MAJOR.VERSION_MINOR.VERSION_PATCH.VERSION_BUILD`.  That is, it is `VERSION_STRING` with all non-digit characters removed
* `@VERSION_MAJOR@` : replaced with the major version number
* `@VERSION_MINOR@` : replaced with the minor version number
* `@VERSION_PATCH@` : replaced with the patch version number
* `@VERSION_BUILD@` : replaced with the build version number

# Github Workflows

These are full-blown workflows that can be triggered from your own repository on certain events.

## [build](https://github.com/KSPModdingLibs/KSPBuildTools/blob/main/.github/workflows/build.yml)

Compiles a KSP mod and uploads the results as a workflow artifact.  It's meant to be suitable for continuous integration builds, as it simply compiles whatever is in the repository without updating version numbers etc.

For details:

* [compile action](#compile)
* [assemble-release action](#assemble-release)

[Example usage from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/.github/workflows/build.yml)

## [create-release](https://github.com/KSPModdingLibs/KSPBuildTools/blob/main/.github/workflows/create-release.yml)

Builds and packages a new version of mod.  You can reference this workflow in your own repository on `workflow-dispatch` and have the developer type in a version number.  Then it does the rest!  

**Note that this action DOES commit files to git (updated changelogs, version files, etc) so if you're testing it out you should do it on a separate branch.**

After running `update-version`, this workflow commits the changelog and version file changes and creates a new tag.  Then it runs `compile` and `assemble-release`.  And then finally it creates a draft github release with the packaged mod attached.

For details:

* [update-version action](#update-version)
* [compile action](#compile)
* [assemble-release action](#assemble-release)

[Example usage from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/.github/workflows/create-release.yml)

## [validate](https://github.com/KSPModdingLibs/KSPBuildTools/blob/main/.github/workflows/validate.yml)

Performs validation to help check for errors.  Right now it just invokes CKAN's KSPMMCfgParser action to check for syntax errors in cfg files, but it may do more in the future.  You may want to add this to a continuous integration workflow that is triggered on pull requests and commits.

# Github Actions

These are smaller units of tasks that can be combined in workflows to help automate testing, building, and releasing mods.

## [assemble-release](https://github.com/KSPModdingLibs/KSPBuildTools/blob/main/.github/actions/assemble-release/action.yml)

Packages a mod for release or upload.  Uses `actions/upload-artifact` so the output is attached to the workflow job and can be used by other workflow jobs.

Env:

* `RELEASE_STAGING`

  The artifact files will be copied to this directory before being packaged in the zip file.  This becomes the input for `upload-artifact`

Inputs:

* `artifacts`

  A list of files or directories to include in the output (relative to the repository root).  Defaults to `GameData LICENSE* README* CHANGELOG*` - that is, anything in the GameData directory, and any license, readme, or changelog files will be included.

* `output-file-name`

  The name of the output zip file (without the extension).  Defaults to the repository name.  This is used in the upload-artifact action.  In addition, a zip file with this name will be created in the github workspace so that it can be immediately consumed by other actions in the workflow without having to download the artifact.

Outputs:

* `artifact-id` and `artifact-url`

  The outputs from `actions/upload-artifact`

## [compile](https://github.com/KSPModdingLibs/KSPBuildTools/blob/main/.github/actions/compile/action.yml)

Compiles C# code using `msbuild` into a mod assembly.  This action will install any dependent mods and restore NuGet packages.

Environment:

* `KSP_ROOT`

  The path to use as the root of a KSP install.  Dependencies will be downloaded here and the `ksp-zip-url` libraries will be extracted here.  This is generally set by the `build` or `assemble-release` workflows.

Inputs:

* `build-configuration`

  The project configuration to build.  Defaults to `Release`.

* `solution-file-path`

  The path to the solution file to build.  Defaults to empty, which will invoke `msbuild` on the root directory of the repo and builds any `*.sln` file it finds there.

* `ksp-zip-url`

  A url for a zip file that contains the assemblies from the game to link against.  This should either be stripped so that it only contains public interfaces, or encrypted so that the libraries are not being redistributed unprotected.

  Defaults to `https://github.com/KSPModdingLibs/KSPLibs/raw/main/KSP-1.12.5.zip` which contains stripped versions of the libraries and should be suitable for most users.

* `ksp-zip-password`

  If the ksp library zip is encrypted, this is the password.  It should be stored in your repository's secrets.

* All inputs from [`install-dependencies`](#install-dependencies)


## [install-dependencies](https://github.com/KSPModdingLibs/KSPBuildTools/blob/main/.github/actions/install-dependencies/action.yml)

Uses CKAN to install any dependent mods so that your code can be compiled against them.  At least one of `dependency-identifiers` or `msbuild-dependency-target` should be specified, or this action won't do anything.

Inputs:

* `dependency-identifiers`

  Optional.  A list of mod identifiers to install

* `msbuild-dependency-target`

  Optional.  The name of a msbuild target to build in order to install dependencies.
  If your dependencies are specified in the .csproj file using `CKANIdentifier` and you're using the standard `KSPCommon.targets` file, then you should set this to `CKANInstall`.

* `ckan-compatible-versions`

  A list of versions for ckan to treat as compatible when installing the dependencies.  Defaults to `1.12 1.11 1.10 1.9 1.8`

* `ckan-filters`

  A list of install filters (files that should *NOT* be installed).  Defaults to `.dds .png .bmp .mu .mbm .jpg .wav` so that large content files can be skipped - only DLL files should be necessary for compilation.

## [update-version](https://github.com/KSPModdingLibs/KSPBuildTools/blob/main/.github/actions/update-version/action.yml)

Uses [yaclog](https://yaclog.readthedocs.io/en/latest/index.html) and [yaclog-ksp](https://pypi.org/project/yaclog-ksp/) to update a changelog and get release notes.  Then runs [update-version.sh](#update-version.sh) to replace version tokens in several text files.  All modifications will be staged to git but not committed.

Inputs:

* `version-string`

  Required.  A version number in major.minor.patch.build form, or one of the special strings `--major` `--minor` `--patch` which will increment the version based on the most recent entry in the changelog file.

* `changelog-input-file`

  Optional.  Default: `CHANGELOG.md`

  The name of a changelog file to use.  Should be formatted according to [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), with a `## Unreleased` section at the top for changes that are pending release.  `yaclog release` will be invoked which replaces the `## Unreleased` section with the version number and the date stamp.

* `changelog-output-file`

  Optional.  If specified, this action will run `yaclog-ksp` on the changelog input file which generates a changelog .cfg file suitable for [Kerbal Changelog](https://github.com/HebaruSan/KerbalChangelog/blob/master/README.md).  As such, `changelog-output-file` should be somewhere in your mod's artifact path so that it will be included when the mod is installed.

* `release-notes-output-file`

  Optional.  If specified, uses `yaclog` to save the most recent changelog info in markdown format to this file.  This can then be used by the [`assemble-release` action](#assemble-release).

* `template-extension`

  Corresponds to the input to `update-version.sh`. Defaults to `.versiontemplate`
  
* `files`

  Corresponds to the input to `update-version.sh`.

* `ksp-build-tools-root`

  Where to download `update-version.sh`.

Outputs:

* The final version string is saved in `VERSION_STRING` as an environment variable and `outputs.version-string`
* The path to the release notes file is saved in `RELEASE_NOTES_FILE` as an environment variable and `outputs-release-notes-output-file`
