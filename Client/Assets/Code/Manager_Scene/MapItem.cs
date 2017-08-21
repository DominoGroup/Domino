using UnityEngine;
public abstract class MapItem : SceneObject<ItemTypeData>
{
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
    
    public override void SetUid(int uid)
    {
        base.SetUid(uid);
        Init();
    }
    /// <summary>
    /// 代替原来Unity的Awake方法
    /// </summary>
    // 该方法会确保在uid赋值后执行
    protected virtual void Init()
    {
    }
    public static MapItem Create(ItemTypeData mapItemData, Vector3 position, Quaternion rotation)
    {
        var mapItemObj = Instantiate(AssetHub.instance.GetAsset<GameObject>(PathConst.mapItemBundle, mapItemData.prefab));
        mapItemObj.transform.localScale = mapItemData.size.ToVector3();

        // 不使用反射来构造逻辑组件
        MapItem result = null;
        switch (mapItemData.code)
        {
            case "domino":
                result = mapItemObj.AddComponent<Domino>();
                break;
            default:
                Debug.LogError(string.Format("未处理代码类型{0}", mapItemData.code));
                break;
        }
        result.transform.localPosition = position;
        result.transform.localRotation = rotation;
        result.SetTypeData(mapItemData);
        return result;
    }
}
public delegate void MapItemStateDelegate(MapItem mapItem, MapItemState previousState, MapItemState currentState);
public enum MapItemState
{
    wait, // 等待动作执行，一旦执行就不会回到wait。
    acting, // 动作执行中。
    acted, // 动作执行结束。如果再次受力，可以转到acting。
}