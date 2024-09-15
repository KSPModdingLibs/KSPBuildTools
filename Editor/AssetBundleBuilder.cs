using System;
using UnityEditor;

public class AssetBundleBuilder
{
    public static void BuildBundles()
    {
        string[] args = Environment.GetCommandLineArgs();
        string path = "";
        string target = "";
        for (int i = 0; i < args.Length; i++)
        {
            if(args[i] == "-assetbundlePath")
            {
                path = args[i + 1];
            }
        }

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}