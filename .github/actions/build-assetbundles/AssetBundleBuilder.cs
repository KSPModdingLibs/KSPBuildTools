using System;
using UnityEditor;
using UnityEngine;

namespace KSPBuildTools {
    public class AssetBundleBuilder
    {
        public static void BuildBundles()
        {
            string[] args = Environment.GetCommandLineArgs();
            string path = "";
            for (int i = 0; i < args.Length; i++)
            {
                if(args[i] == "-assetbundlePath")
                {
                    path = args[i + 1];
                }
            }

            Debug.Log($"Building assetbundle for {path}");

            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);

            Debug.Log("Done!");
        }
    }
}
