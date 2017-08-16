using UnityEngine;
[ExecuteInEditMode]
public abstract class AssetHub
{
    public static AssetHub instance
    {
        get
        {
            // 注：编辑器和运行时方法暂时未分离
            if (_instance == null)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    _instance = new AssetHubEditor();
                else
#endif
                    _instance = new AssetHubEditor();
            }
            return _instance;
        }
    }
    private static AssetHub _instance;
    protected AssetHub()
    {
    }
    // 注：暂时只留Editor加载路线，Release版本之后再补
    public abstract T GetAsset<T>(string bundle, string asset) where T : Object;
}