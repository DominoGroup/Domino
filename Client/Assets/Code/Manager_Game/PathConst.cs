using UnityEngine;
using System.IO;
using System.Collections;
public static class PathConst
{
    // AssetBundle文件相对dataPath的位置
    public const string assetBundleRoot = "AssetBundle";
    // 条件节点路径段
    public const string commonBundleRoot = "common";
    // 高级节点路径段
    public const string prefabBundleRoot = "prefab";
    // AssetBundle路径
    public static string mapItemBundle
    {
        get
        {
            return commonBundleRoot.Combine(prefabBundleRoot).Combine("mapitem");
        }
    }
    public const string dataBundle = "data";
}