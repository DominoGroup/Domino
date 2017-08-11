using UnityEngine;
/// <summary>
/// 地图物品数据
/// </summary>
[System.Serializable]
public class MapItemData : ExcelData
{
    public int id;
    public string name;
    public string prefab;
    public string code;
    public SerilizableVector3 size;
    public float mass;
    public float drag;
    public float angularDrag;
}
/// <summary>
/// 可用于序列化的Vector3
/// </summary>
[System.Serializable]
public struct SerilizableVector3
{
    public float x;
    public float y;
    public float z;

    public SerilizableVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static SerilizableVector3 Parse(string text)
    {
        var raw = text.Split(',');
        var result = new SerilizableVector3();
        result.x = float.Parse(raw[0]);
        result.y = float.Parse(raw[1]);
        result.z = float.Parse(raw[2]);
        return result;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
public struct SerilizableQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public SerilizableQuaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public static SerilizableQuaternion Parse(string text)
    {
        var raw = text.Split(',');
        var result = new SerilizableQuaternion();
        result.x = float.Parse(raw[0]);
        result.y = float.Parse(raw[1]);
        result.z = float.Parse(raw[2]);
        result.w = float.Parse(raw[3]);
        return result;
    }
    public Quaternion ToVector3()
    {
        return new Quaternion(x, y, z, w);
    }
}
public static class DataCastFunctionHolder
{
    public static SerilizableVector3 ToSerilizable(this Vector3 original)
    {
        return new SerilizableVector3(original.x, original.y, original.z);
    }
    public static SerilizableQuaternion ToSerilizable(this Quaternion original)
    {
        return new SerilizableQuaternion(original.x, original.y, original.z, original.w);
    }
}