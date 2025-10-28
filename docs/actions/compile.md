# Compile

````{gha:action}
:path: .github/actions/compile

```{warning}
Due to the [handover of the Mono project](https://github.com/mono/mono/issues/21796), the `ubuntu-latest` github runner [does not currently include mono or nuget](https://github.com/actions/runner-images/issues/10636#issuecomment-2375010324). 

Please use the `ubuntu-22.04` runner image or install nuget yourself when `use-nuget-restore` is not `'true'`.

This does not affect projects that use `packagereference`
```

````