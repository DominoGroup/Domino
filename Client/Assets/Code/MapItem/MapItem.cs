using UnityEngine;
public class MapItem : MonoBehaviour
{
    public int uid { get; private set; }
    public MapItemData itemData { get; private set; }
    public void SetUid(int uid)
    {
#if UNITY_EDITOR
        if (uid > 0)
            throw new System.Exception(string.Format("地图物品被重复构造：{0}", gameObject.name));
        else
#endif
        this.uid = uid;
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
    public void Kill()
    {
        Destroy(gameObject);
    }
}