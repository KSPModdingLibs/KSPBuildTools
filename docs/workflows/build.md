# Build

```{gh-actions:workflow}
---
path: .github/workflows/build.yml
---

Compiles a KSP mod and uploads the results as a workflow artifact.  It's meant to be suitable for continuous integration builds, as it simply compiles whatever is in the repository without updating version numbers etc.

For details:

* [compile action](#compile)
* [assemble-release action](#assemble-release)

```