using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 地形管理器
/// </summary>
public class TerrainHub
{
    public List<TerrainCube> cubeList;
    public TerrainHub()
    {
        cubeList = new List<TerrainCube>();
    }
    public static TerrainCube Create(TerrainCubeData cubeData)
    {
        var terrainTypeData = GameDataHub.instance.excelDataHub.GetTerrainTypeData(cubeData.terrainId);
        var cubeObj = new GameObject(terrainTypeData.name);
        cubeObj.layer = GameDataHub.instance.groundLayer;
        var terrainCube = cubeObj.AddComponent<TerrainCube>();
        terrainCube.SetCubeData(cubeData);
        return terrainCube;
    }
}