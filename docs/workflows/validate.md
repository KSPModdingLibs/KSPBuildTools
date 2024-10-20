# Validate

````{gha:workflow}
---
path: .github/workflows/validate.yml
---

Performs validation to help check for errors.  Right now it just invokes CKAN's KSPMMCfgParser action to check for syntax errors in cfg files, but it may do more in the future.  You may want to add this to a continuous integration workflow that is triggered on pull requests and commits.

````