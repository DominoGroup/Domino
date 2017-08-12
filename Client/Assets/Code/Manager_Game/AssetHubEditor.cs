#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;
/// <summary>
/// 编辑器用资源加载器
/// </summary>
public class AssetHubEditor : AssetHub
{
    string[] allAssetPaths;
    protected override void Awake()
    {
        base.Awake();
        allAssetPaths = (from path in AssetDatabase.GetAllAssetPaths()
                         let extension = System.IO.Path.GetExtension(path)
                         where !string.IsNullOrEmpty(extension) && extension != ".meta"
                         select path).ToArray();
    }
    public override T GetAsset<T>(string bundle, string asset)
    {
        T result = null;
        var assetPath = GetBundlePathInEditor(bundle, asset);
        // 增加点号，保证文件名后面是后缀
        assetPath += ".";
        for (int i = 0; i < allAssetPaths.Length; i++)
        {
            if (allAssetPaths[i].StartsWith(assetPath))
            {
                result = AssetDatabase.LoadAssetAtPath<T>(allAssetPaths[i]);
                if (result != null)
                    break;
            }
        }
        if (result == null)
            Debug.LogError(string.Format("无法找到资源包为{0}，文件为{1}，类型为{2}的资源", bundle, asset, typeof(T)));
        return result;
    }
    public static string GetBundlePathInEditor(string bundle, string asset)
    {
        return "Assets".Combine(PathConst.assetBundleRoot).Combine(bundle).Combine(asset);
    }
}
#endif