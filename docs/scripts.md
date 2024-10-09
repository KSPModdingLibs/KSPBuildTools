# Shell Scripts

``` {program} update-version.sh
```

## update-version.sh

This is a bash shell script that can update version numbers in several text files (AssemblyInfo.cs, .version files, readme, etc). All it does is process files with a common extension and replaces certain tokens in them.

This file can be run from your own repository locally, and it's also used in some of the github actions and workflows. If you're already using git for windows, you have a bash shell that you can run this from.

### Usage

```{object} update-version.sh [-g (true|false)] [-d (true|false)] VERSION_STRING TEMPLATE_EXTENSION [FILES]
```
Example:
`update-version.sh 1.2.3.4 .vtxt AssemblyInfo.cs.vtxt`

Example template files:

* [AssemblyInfo.cs from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/SharedAssemblyInfo.cs.versiontemplate)
* [.version file from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/GameData/JSI/RasterPropMonitor/RasterPropMonitor.version.versiontemplate)

```{option} VERSION_STRING
**Required**

This is the new version you want to set, in MAJOR.MINOR.PATCH.BUILD form. Any of the values can be omitted (starting from the right).
```

```{option} TEMPLATE_EXTENSION
**Optional.** Default: `.versiontemplate`

This is a file extension that indicates which files the script should consider. When processing a file, if it ends with this extension then the script will store the output in a new file with the template extension removed. Otherwise the file is updated in-place.
```

```{option} FILES
**Optional.**

This is a list of files to process. If omitted, the script will process all files in the subtree that end with
`TEMPLATE_EXTENSION`.
```

```{option} -g (true|false)
**Optional.** Default: false

If true, calls `git add` on each of the modified files (but does not commit).
```

```{option} -d (true|false)
**Optional.** Default: false

If true, deletes the template file after processing (if the file was not updated in-place).
```

### Tokens

The script will replace the following tokens in processed files:

* `@VERSION_STRING@` : replaced with the raw `VERSION_STRING`
* `@VERSION_FULL@` : replaced with `VERSION_MAJOR.VERSION_MINOR.VERSION_PATCH.VERSION_BUILD`. That is, it is
  `VERSION_STRING` with all non-digit characters removed
* `@VERSION_MAJOR@` : replaced with the major version number
* `@VERSION_MINOR@` : replaced with the minor version number
* `@VERSION_PATCH@` : replaced with the patch version number
* `@VERSION_BUILD@` : replaced with the build version number