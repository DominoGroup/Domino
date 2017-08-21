﻿using UnityEngine;
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