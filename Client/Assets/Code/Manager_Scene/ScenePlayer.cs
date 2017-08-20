using UnityEngine;
using UnityEditor;
public class ScenePlayer : MonoSingleton<ScenePlayer>
{
    public MapItemHub mapItemHub { get; private set; }
    public TerrainHub terrainHub { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        mapItemHub = new MapItemHub();
        terrainHub = new TerrainHub();
    }
}