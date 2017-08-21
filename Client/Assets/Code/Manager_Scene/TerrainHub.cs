using UnityEngine;
/// <summary>
/// 地形管理器
/// </summary>
public class TerrainHub : SceneObjectHub<TerrainTypeData, TerrainCube, TerrainCubeData>
{
    public override TerrainCube CreateItem(TerrainCubeData itemData)
    {
        var terrainTypeData = GameDataHub.instance.excelDataHub.GetTerrainTypeData(itemData.terrainId);
        return TerrainCube.Create(terrainTypeData, itemData.minValue.ToVector3(), itemData.maxValue.ToVector3());
    }
    public override TerrainCubeData GetItemData(TerrainCube item)
    {
        var result = new TerrainCubeData();
        result.terrainId = item.typeData.id;
        var bounds = item.boxCollider.bounds;
        result.minValue = bounds.min.ToSerilizable();
        result.maxValue = bounds.max.ToSerilizable();
        return result;
    }
    public static Material[] GetTerrainMaterials(params string[] materialNames)
    {
        var result = new Material[materialNames.Length];
        for (int i = 0; i < materialNames.Length; i++)
        {
            var materialName = materialNames[i];
            result[i] = AssetHub.instance.GetAsset<Material>(PathConst.terrainMaterialBundle, materialName);
        }
        return result;
    }
}