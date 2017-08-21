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
    public const string materialBundleRoot = "material";
    // AssetBundle路径
    public static string mapItemBundle
    {
        get
        {
            return commonBundleRoot.Combine(prefabBundleRoot).Combine("mapitem");
        }
    }
    public static string terrainMaterialBundle
    {
        get
        {
            return commonBundleRoot.Combine(materialBundleRoot).Combine("terrain");
        }
    }
    public const string dataBundle = "data";
}