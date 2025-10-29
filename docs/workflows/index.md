# Github Workflows

KSPBuildTools provides several reusable workflows for use with Github Actions. These are full-blown workflows that can be triggered from your own repository on certain events.

For usage, see [Reusing Workflows](https://docs.github.com/en/actions/sharing-automations/reusing-workflows) in the Github docs.

```{warning}
Due to a limitation of Github actions, only *tagged releases* have reusable workflows with the correct action version. If you pin a workflow to a branch, or leave your workflow usage unpinned, it may run action versions incompatible with the workflow
```

```{toctree}
---
caption: Contents
maxdepth: 1
glob:
---

*

```