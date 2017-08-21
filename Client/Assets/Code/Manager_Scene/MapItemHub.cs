using System;
using System.Collections.Generic;
using UnityEngine;
public class MapItemHub : SceneObjectHub<ItemTypeData, MapItem, SceneItemData>
{
    public override MapItem CreateItem(SceneItemData itemData)
    {
        var mapItemData = GameDataHub.instance.excelDataHub.GetMapItemData(itemData.mapItemId);
        return MapItem.Create(mapItemData, itemData.position.ToVector3(), itemData.rotation.ToQuaternion());
    }
    public override SceneItemData GetItemData(MapItem item)
    {
        var result = new SceneItemData();
        result.mapItemId = item.typeData.id;
        result.position = item.transform.position.ToSerilizable();
        result.rotation = item.transform.rotation.ToSerilizable();
        return result;
    }
}