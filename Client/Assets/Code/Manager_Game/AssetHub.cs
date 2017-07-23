using UnityEngine;
public class AssetHub : MonoBehaviour
{
    public static AssetHub instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("AssetHub被重复构造！");
    }
    /// <summary>
    /// 获得地图使用的GameObject实例
    /// </summary>
    // 注：会完成添加必要组件等一系列功能
    public MapItem GetMapItem(int id)
    {
        throw new System.NotImplementedException();
    }
}