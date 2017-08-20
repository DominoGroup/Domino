/// <summary>
/// 地形方块数据
/// </summary>
[System.Serializable]
public class TerrainCubeData
{
    public const float thickness = 1f;
    public int terrainId;
    public SerilizableVector2 minValue;
    public SerilizableVector2 maxValue;
    public float height;
}