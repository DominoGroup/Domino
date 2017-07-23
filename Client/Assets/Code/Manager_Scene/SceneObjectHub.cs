using System.Collections.Generic;
using UnityEngine;
public class SceneObjectHub
{
    List<MapItem> mapItemList;
    int nextUid;
    public SceneObjectHub()
    {
        mapItemList = new List<MapItem>();
    }
    /// <summary>
    /// 增加地图物品
    /// </summary>
    /// <returns>地图物品唯一uid</returns>
    /// 注：添加前需要自行检查位置是否合理
    public MapItem AddMapItem(int id, Vector3 position, Quaternion rotation)
    {
        nextUid++;
        var item = AssetHub.instance.GetMapItem(id);
        item.SetUid(nextUid);
        return item;
    }
    public void RemoveMapItem(int uid)
    {
        var itemId = mapItemList.FindIndex(a => a.uid == uid);
        if (itemId >= 0)
        {
            mapItemList[itemId].Kill();
            mapItemList.RemoveAt(itemId);
        }
    }
}