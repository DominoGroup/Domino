using UnityEngine;
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T instance { get; private set; }
    protected virtual void Awake()
    {
#if UNITY_EDITOR
        if (instance != null)
            Debug.LogError(string.Format("单例{0}被重复构造，检查是否有上个单例因为事件绑定或引用未析构", typeof(T).ToString()));
        else
#endif
            instance = (T)this;
    }
}