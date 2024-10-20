# Publish To Spacedock

````{gha:workflow}
---
path: .github/workflows/publish-to-spacedock.yml
---

Publishes a github release to Spacedock. You can reference this workflow from one in your own repository that is triggered on a release being published, so that it automatically also gets uploaded to Spacedock.

Calls get-release-info and then uses KSP2Community's [spacedock-upload](https://github.com/KSP2Community/spacedock-upload) action to publish it. You will need to provide your spacedock username and mod id as variables, and the spacedock password as a secret. You could either hardcode the mod ID and username in your repository's workflow, or use a repository or organization variable.

[Example from RasterPropMonitor](https://github.com/FirstPersonKSP/RasterPropMonitor/blob/master/.github/workflows/publish-to-spacedock.yml)
````