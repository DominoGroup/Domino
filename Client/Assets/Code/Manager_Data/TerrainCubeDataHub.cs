using System.Collections.Generic;
/// <summary>
/// 地形方块数据组
/// </summary>
[System.Serializable]
public class TerrainCubeDataHub
{
    public List<TerrainCubeData> terrainCubeList;
    public TerrainCubeDataHub()
    {
        terrainCubeList = new List<TerrainCubeData>();
    }
}