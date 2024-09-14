ARG UNITY_VERSION=2019.4.18f1

# Source from UnityCI's prebuild unity editor containers
FROM unityci/editor:${UNITY_VERSION}-windows-mono-3

#Copy entire KSPBuildTools repo into container
COPY ./ /KSPBuildTools

# Code file to execute when the docker container starts up (`build-assetbundle.sh`)
ENTRYPOINT ["/KSPBuildTools/build-assetbundle.sh", "--editor", "unity-editor"]