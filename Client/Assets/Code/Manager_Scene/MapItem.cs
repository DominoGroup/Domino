using UnityEngine;
public abstract class MapItem : MonoBehaviour
{
    public int uid { get; private set; }
    public MapItemData itemData { get; private set; }
    public MapItemState state
    {
        get
        {
            return _state;
        }
        set
        {
            if (_state != value)
            {
                var previous = _state;
                _state = value;
                if (onStateUpdate != null)
                    onStateUpdate.Invoke(this, previous, _state);
            }
        }
    }
    private MapItemState _state;
    public event MapItemStateDelegate onStateUpdate;
    
    public void SetUid(int uid)
    {
#if UNITY_EDITOR
        if (uid > 0)
            throw new System.Exception(string.Format("地图物品被重复构造：{0}", gameObject.name));
        else
#endif
        this.uid = uid;
        Init();
    }
    public void SetItemData(MapItemData itemData)
    {
#if UNITY_EDITOR
        if (itemData != null)
            throw new System.Exception(string.Format("地图数据被重复构造：{0}", gameObject.name));
        else
#endif
        this.itemData = itemData;
    }
    /// <summary>
    /// 代替原来Unity的Awake方法
    /// </summary>
    // 该方法会确保在uid赋值后执行
    protected abstract void Init();
    public void Kill()
    {
        Destroy(gameObject);
    }
}
public delegate void MapItemStateDelegate(MapItem mapItem, MapItemState previousState, MapItemState currentState);
public enum MapItemState
{
    wait, // 等待动作执行，一旦执行就不会回到wait。
    acting, // 动作执行中。
    acted, // 动作执行结束。如果再次受力，可以转到acting。
}