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
        var mapItemData = ExcelDataHub.instance.GetMapItemData(id);
        var mapItemObj = Object.Instantiate(AssetHub.instance.GetAsset<GameObject>(PathConst.mapItemBundle, mapItemData.prefab));
        mapItemObj.transform.localPosition = position;
        mapItemObj.transform.localRotation = rotation;
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

        result.SetItemData(mapItemData);
        result.SetUid(nextUid);
        return result;
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