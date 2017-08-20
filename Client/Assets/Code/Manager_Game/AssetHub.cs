using UnityEngine;
[ExecuteInEditMode]
public abstract class AssetHub : MonoSingleton<AssetHub>
{
    // 注：暂时只留Editor加载路线，Release版本之后再补
    public abstract T GetAsset<T>(string bundle, string asset) where T : Object;
}