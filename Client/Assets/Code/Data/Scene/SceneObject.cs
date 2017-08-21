using UnityEngine;
using System.Collections;

public abstract class SceneObject<T> : MonoBehaviour where T : ExcelData
{
    public int uid { get; private set; }
    public T typeData { get; private set; }

    public virtual void SetUid(int uid)
    {
#if UNITY_EDITOR
        if (this.uid > 0)
            throw new System.Exception(string.Format("{1}的Uid被重复构造：{0}", gameObject.name, GetType()));
        else
#endif
            this.uid = uid;
    }
    protected virtual void SetTypeData(T typeData)
    {
#if UNITY_EDITOR
        if (this.typeData != null)
            throw new System.Exception(string.Format("{1}的类型被重复构造：{0}", gameObject.name, GetType()));
        else
#endif
            this.typeData = typeData;
    }
    /// <summary>
    /// 代替原来Unity的Awake方法
    /// </summary>
    // 该方法会确保在uid赋值后执行
    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}