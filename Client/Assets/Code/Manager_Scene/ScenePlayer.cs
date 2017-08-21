using UnityEngine;
using UnityEditor;
public class ScenePlayer : MonoSingleton<ScenePlayer>
{
    public MapItemHub itemHub { get; private set; }
    public TerrainHub terrainHub { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        itemHub = new MapItemHub();
        terrainHub = new TerrainHub();
    }
    public void ReadData(SceneData sceneData)
    {
        itemHub.ReadData(sceneData.itemData);
        terrainHub.ReadData(sceneData.terrainData);
    }
    public SceneData WriteData()
    {
        var result = new SceneData();
        result.itemData = itemHub.WriteData();
        result.terrainData = terrainHub.WriteData();
        return result;
    }
}