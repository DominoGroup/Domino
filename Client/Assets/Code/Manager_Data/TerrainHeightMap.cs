using System.Collections.Generic;
public class TerrainHeightMap
{
    /// <summary>
    /// 按地形分解的高度图
    /// </summary>
    public List<TerrainHeightMapData> heightMapList;
    public TerrainHeightMap()
    {
        heightMapList = new List<TerrainHeightMapData>();
    }
}