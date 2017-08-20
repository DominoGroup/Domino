public class TerrainHeightMapData
{
    public TerrainTypeData terrainType;
    public float[,] heightMapData;
    public TerrainHeightMapData(int terrainId,int width, int height)
    {
        terrainType = GameDataHub.instance.excelDataHub.GetTerrainTypeData(terrainId);
        heightMapData = new float[width, height];
    }
}