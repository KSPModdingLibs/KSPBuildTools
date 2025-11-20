# MSBuild Reference

KSPBuildTools provides msbuild properties and targets for developing KSP plugins.

What it does:

- Provides a standard way to find your KSP install and the game's libraries that works on anyone's machine and any operating system
- Provides a way for individual developers to select their KSP install location without modifying git-tracked files
- Properties can be set for the entire mod instead of needing to do it per project file
- Provides a standard way to copy output files that works on all operating systems
- Sets the debug symbols format to portable so that you can [debug your mod](https://gist.github.com/gotmachine/d973adcb9ae413386291170fa346d043)
- Sets up Visual Studio's debugging start actions so you can launch KSP directly from VS
- Includes a target for installing dependencies with CKAN
- Designed to be used by the [Build github workflow](https://kspbuildtools.readthedocs.io/en/docs/actions/index.html)
- Includes support for version stamping

```{toctree}
---
caption: Contents
maxdepth: 1
---

getting-started
ksp-install
dependencies
generating-version-files
configuration
```

