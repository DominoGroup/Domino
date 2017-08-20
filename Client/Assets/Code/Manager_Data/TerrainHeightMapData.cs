public class TerrainHeightMapData
{
    public int terrainType;
    public float[,] heightMapData;
    public TerrainHeightMapData(int terrainType,int width, int height)
    {
        this.terrainType = terrainType;
        heightMapData = new float[width, height];
    }
}