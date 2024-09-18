`build-assetbundles` Action
===========================

Builds one or more assetbundles. Wraps the [GameCI Unity Builder](https://github.com/marketplace/actions/unity-builder) action in order to call the Unity editor. Assetbundles are cached to speed up your workflow if none of the assets change from build to build.

## Usage

### Activation
The Unity editor requires a login to function, even with the free edition. You will have to make some repository secrets in order to allow Unity to function. Follow the instructions in the [GameCI documentation](https://game.ci/docs/github/activation) to set these up.

### Using a Unity project
If your repo has a complete Unity project already checked in, you can point the action at it and have it build all your defined asset bundles.

```yaml
- uses: github.com/KSPModdingLibs/KSPBuildTools/.github/actions/build-assetbundles@main
  with: 
    project-path: 'MyUnityProject'
  env:
    UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
    UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
    UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
```
### Using a list of assets
Alternatively, you can give the action an assetbundle name and a list of assets to compile, and it will create a temporary project in order to compile them

```yaml
- uses: github.com/KSPModdingLibs/KSPBuildTools/.github/actions/build-assetbundles@main
  with: 
    assetbundle-name: mybundle.shab
    asset-files: 'MyAssets/*.shader MyAssets/*.cginc'
  env:
    UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
    UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
    UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
```

### Build Method
By default, this action will use the [`KSPBuildTools.KSPBuildTools.AssetBundleBuilder.BuildBundles` build method](AssetBundleBuilder.cs). This function calls on the Unity build pipeline to build your assetbundles. If you need, you can provide your own custom build function and call it with the [`#build-method`](#build-method-1) input.

## Inputs

### `unity-version`

  Which Unity version to use. Defaults to 2019.4.18f1, which supports KSP 1.12

### `project-dir`

  Path to an existing Unity project (the folder that contains "Assets"). Must be relative to `github.workspace` (usually your repository root). Mutually exclusive with `assetbundle-name` and `asset-files`.

### `asset-files`

  Glob of assets to bundle. Mutually exclusive with `project-dir`

### `assetbundle-name`

  Name of the assetbundle to generate from `asset-files`. Mutually exclusive with `project-dir`

### `output-dir`

  Directory in which to place generated assetbundle(s).

### `build-method`

  `[Namespace].Class.Function` to call in order to build your assetbundle. Defaults to `KSPBuildTools.KSPBuildTools.AssetBundleBuilder.BuildBundles`