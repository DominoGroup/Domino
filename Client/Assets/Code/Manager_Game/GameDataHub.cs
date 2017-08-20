using UnityEngine;
public class GameDataHub : MonoSingleton<GameDataHub>
{
    // 物体Layer区域
    public const string groundLayerName = "Ground";
    public const string mapItemLayerName = "MapItem";

    public int groundLayer;
    public int mapItemLayer;

    public ExcelDataHub excelDataHub { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        groundLayer = LayerMask.NameToLayer(groundLayerName);
        mapItemLayer = LayerMask.NameToLayer(mapItemLayerName);
        excelDataHub = new ExcelDataHub();
    }
}