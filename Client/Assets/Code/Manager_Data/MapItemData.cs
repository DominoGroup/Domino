using UnityEngine;
/// <summary>
/// 地图物品数据
/// </summary>
[System.Serializable]
public class MapItemData : ExcelData
{
    public int id;
    public string name;
    public Vector3 size;
    public float mass;
    public float drag;
    public float angularDrag;
}