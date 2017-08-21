/// <summary>
/// 地形方块数据
/// </summary>
[System.Serializable]
public class TerrainCubeData
{
    public const float transitionHeight = 0.2f;
    public int terrainId;
    public SerilizableVector3 minValue;
    public SerilizableVector3 maxValue;
}