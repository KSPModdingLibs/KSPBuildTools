# Create Release

````{gh-actions:workflow}
---
path: .github/workflows/create-release.yml
---

Builds and packages a new version of mod.  You can reference this workflow in your own repository on [workflow-dispatch](https://docs.github.com/en/actions/managing-workflow-runs-and-deployments/managing-workflow-runs/manually-running-a-workflow) and have the developer type in a version number.  Then it does the rest!  

```{note}
This action DOES commit files to git (updated changelogs, version files, etc) so if you're testing it out you should do it on a separate branch.
```

After running {gh-actions:action}`update-version`, this workflow commits the changelog and version file changes and creates a new tag.  Then it runs {gh-actions:action}`compile` and {gh-actions:action}`assemble-release`.  And then finally it creates a draft github release with the packaged mod attached.
For details:

* [update-version action](#update-version)
* [compile action](#compile)
* [assemble-release action](#assemble-release)

[Example usage from RasterPropMonitor](https://github.com/JonnyOThan/RasterPropMonitor/blob/master/.github/workflows/create-release.yml)

````