using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
public abstract class AssetHub : MonoBehaviour
{
    public static AssetHub instance { get; private set; }
    protected virtual void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("AssetHub被重复构造！");
    }
    // 注：暂时只留Editor加载路线，Release版本之后再补
    public abstract T GetAsset<T>(string bundle, string asset) where T : Object;
}