public static class PathConst
{
    // AssetBundle文件相对dataPath的位置
    public const string assetBundleRoot = "AssetBundle";
    // 永久节点路径段
    public const string dataBundle = "data";
    public const string physicsBundle = "physics";
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
    public static string physicsMaterialBundle
    {
        get
        {
            return physicsBundle;
        }
    }
    public static string excelDataBundle
    {
        get
        {
            return dataBundle.Combine("excel");
        }
    }
    public static string sceneDataBundle
    {
        get
        {
            return dataBundle.Combine("scene");
        }
    }
}