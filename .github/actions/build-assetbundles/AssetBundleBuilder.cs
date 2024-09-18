using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace KSPBuildTools {
    public class AssetBundleBuilder {
        public static void BuildBundles() {
            string[] args = Environment.GetCommandLineArgs();
            bool isProject = true;
            string assetbundleName = null;
            string path = "";
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == "-assetbundlePath") {
                    path = args[i + 1];
                    i++;
                }

                if (args[i] == "-assetbundleName") {
                    isProject = false;
                    assetbundleName = args[i + 1];
                    i++;
                }
            }

            Debug.Log($"Building assetbundle for {path}");

            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.StandaloneWindows64, false);
            PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows64, new GraphicsDeviceType[] {
                GraphicsDeviceType.OpenGLCore, GraphicsDeviceType.Direct3D11
            });

            if (isProject) {
                BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression,
                    BuildTarget.StandaloneWindows64);
            }
            else {
                var bundleDefinitions = new AssetBundleBuild[] {
                    new AssetBundleBuild {
                        assetBundleName = assetbundleName,
                        assetNames = new string[] { "Assets/Bundle" }
                    }
                };
                BuildPipeline.BuildAssetBundles(path, bundleDefinitions, BuildAssetBundleOptions.ChunkBasedCompression,
                    BuildTarget.StandaloneWindows64);
            }

            Debug.Log("Done!");
        }
    }
}