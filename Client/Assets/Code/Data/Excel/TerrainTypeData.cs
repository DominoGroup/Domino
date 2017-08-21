/// <summary>
/// 地形类型数据，记录地形材质和摩擦力
/// </summary>
[System.Serializable]
public class TerrainTypeData : ExcelData
{
    public int id;
    public string name;
    public string top;
    public string side;
    public string transition;
    public float drag;
    public string physics;
}