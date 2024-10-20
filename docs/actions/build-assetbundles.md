# Build Assetbundles

```{gha:action}
:path: .github/actions/build-assetbundles
```

## Usage

### Activation
The Unity editor requires a login to function, even with the free edition. You will have to make some repository secrets in order to allow Unity to function. Follow the instructions in the [GameCI documentation](https://game.ci/docs/github/activation) to set these up.

### Using a Unity project
If your repo has a complete Unity project already checked in, you can point the action at it and have it build all your defined asset bundles.

```{gha:example}
- uses: ./.github/actions/build-assetbundles
  with: 
    project-dir: 'MyUnityProject'
  env:
    UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
    UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
    UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
```
### Using a list of assets
Alternatively, you can give the action an assetbundle name and a list of assets to compile, and it will create a temporary project in order to compile them

```{gha:example}
- uses: ./.github/actions/build-assetbundles
  with: 
    assetbundle-name: mybundle.shab
    asset-files: 'MyAssets/*.shader MyAssets/*.cginc'
  env:
    UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
    UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
    UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
```

### Build Method
By default, this action will use the [`KSPBuildTools.KSPBuildTools.AssetBundleBuilder.BuildBundles` build method](../../.github/actions/build-assetbundles/AssetBundleBuilder.cs). This function calls on the Unity build pipeline to build your assetbundles. If you need, you can provide your own custom build function and call it with the {any}`build-method` input.
