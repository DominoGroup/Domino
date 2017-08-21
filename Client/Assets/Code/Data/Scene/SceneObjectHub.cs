using System.Collections.Generic;
public abstract class SceneObjectHub<T, V, W> where T : ExcelData where V : SceneObject<T> where W : SceneObjectData
{
    protected int nextUid;
    public List<V> itemList;
    public SceneObjectHub()
    {
        itemList = new List<V>();
    }
    public void ReadData(List<W> itemDataList)
    {
        for (int i = 0; i < itemDataList.Count; i++)
            AddItem(itemDataList[i]);
    }
    public List<W> WriteData()
    {
        var result = new List<W>();
        for (int i = 0; i < itemList.Count; i++)
            result.Add(GetItemData(itemList[i]));
        return result;
    }
    public abstract W GetItemData(V item);
    public void Clear()
    {
        nextUid = 0;
        for (int i = 0; i < itemList.Count; i++)
            itemList[i].Kill();
        itemList.Clear();
    }
    public void AddItem(W itemData)
    {
        nextUid++;
        var item = CreateItem(itemData);
        item.SetUid(nextUid);
        itemList.Add(item);
    }
    public void RemoveItem(int uid)
    {
        var itemId = itemList.FindIndex(a => a.uid == uid);
        if (itemId >= 0)
        {
            itemList[itemId].Kill();
            itemList.RemoveAt(itemId);
        }
    }
    public abstract V CreateItem(W itemData);
}
/// <summary>
/// 标识一个数据可以从Xml表格构造的基类
/// </summary>
public abstract class SceneObjectData
{
}