using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 地形方块
/// </summary>
public class TerrainCube : SceneObject<TerrainTypeData>
{
    public static Vector3[] pointBuffer = new Vector3[4];
    public static Vector2[] uvBuffer = new Vector2[2];
    public BoxCollider boxCollider { get; private set; }
    private void SetGeometry(Vector3 minPosition, Vector3 maxPosition)
    {
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        meshRenderer.sharedMaterials = TerrainHub.GetTerrainMaterials(typeData.top, typeData.side, typeData.transition);

        var size = (maxPosition - minPosition);
        boxCollider.center = new Vector3(size.x, -size.y, size.z) * 0.5f;
        boxCollider.size = size;
        // 拼合地形效果
        CubeGeneration(meshFilter, size);
    }
    private void CubeGeneration(MeshFilter meshFilter, Vector3 size)
    {
        // 因为法线方向问题，每一个面都需要独立四个点
        var mesh = new Mesh();
        var vertices = new List<Vector3>();
        var uv = new List<Vector2>();
        var normals = new List<Vector3>();
        mesh.subMeshCount = 3;

        var min = new Vector3(0f, -size.y, 0f);
        var max = new Vector3(size.x, 0f, size.z);
        // 计算方法顶面
        pointBuffer[0] = new Vector3(max.x, max.y, max.z);
        pointBuffer[1] = new Vector3(min.x, max.y, max.z);
        pointBuffer[2] = new Vector3(min.x, max.y, min.z);
        pointBuffer[3] = new Vector3(max.x, max.y, min.z);
        uvBuffer[0] = Vector2.zero;
        uvBuffer[1] = new Vector2(max.x, max.z);
        var triangles_0 = new List<int>();
        AddOneFace(vertices, uv, normals, triangles_0);
        
        // 计算方法侧面
        var centerPoint = max.y - TerrainCubeData.transitionHeight;
        // 侧部底面
        var triangles_1 = new List<int>();
        AddOneLayerOfSide(vertices, uv, normals, triangles_1, min, new Vector3(max.x, centerPoint, max.z));
        // 侧部顶面
        var triangles_2 = new List<int>();
        AddOneLayerOfSide(vertices, uv, normals, triangles_2, new Vector3(min.x, centerPoint, min.z), max);

        mesh.SetVertices(vertices);
        mesh.SetUVs(0, uv);
        mesh.SetTriangles(triangles_0, 0);
        mesh.SetTriangles(triangles_1, 1);
        mesh.SetTriangles(triangles_2, 2);
        mesh.RecalculateTangents();
        meshFilter.mesh = mesh;
    }
    void AddOneLayerOfSide(List<Vector3> vertices, List<Vector2> uv, List<Vector3> normals, List<int> triangles, Vector3 min, Vector3 max)
    {
        // 八个基本点
        var point_0 = new Vector3(min.x, max.y, min.z);
        var point_1 = new Vector3(min.x, max.y, max.z);
        var point_2 = new Vector3(max.x, max.y, max.z);
        var point_3 = new Vector3(max.x, max.y, min.z);
        var point_4 = new Vector3(min.x, min.y, min.z);
        var point_5 = new Vector3(min.x, min.y, max.z);
        var point_6 = new Vector3(max.x, min.y, max.z);
        var point_7 = new Vector3(max.x, min.y, min.z);
        // 左面
        pointBuffer[0] = point_0;
        pointBuffer[1] = point_1;
        pointBuffer[2] = point_5;
        pointBuffer[3] = point_4;
        uvBuffer[0] = new Vector2(point_4.z, point_4.y);
        uvBuffer[1] = new Vector2(point_1.z, point_1.y);
        AddOneFace(vertices, uv, normals, triangles);
        // 后面
        pointBuffer[0] = point_1;
        pointBuffer[1] = point_2;
        pointBuffer[2] = point_6;
        pointBuffer[3] = point_5;
        uvBuffer[0] = new Vector2(point_5.x, point_5.y);
        uvBuffer[1] = new Vector2(point_2.x, point_2.y);
        AddOneFace(vertices, uv, normals, triangles);
        // 右面
        pointBuffer[0] = point_2;
        pointBuffer[1] = point_3;
        pointBuffer[2] = point_7;
        pointBuffer[3] = point_6;
        uvBuffer[0] = new Vector2(point_7.z, point_7.y);
        uvBuffer[1] = new Vector2(point_2.z, point_2.y);
        AddOneFace(vertices, uv, normals, triangles);
        // 正面
        pointBuffer[0] = point_3;
        pointBuffer[1] = point_0;
        pointBuffer[2] = point_4;
        pointBuffer[3] = point_7;
        uvBuffer[0] = new Vector2(point_4.x, point_4.y);
        uvBuffer[1] = new Vector2(point_3.x, point_3.y);
        AddOneFace(vertices, uv, normals, triangles);
    }
    // 按从左上开始的顺时针顺序，添加四个坐标点组成一个面
    // 注：point信息从静态PointBuffer获得，uv信息从uvBuffer获得
    void AddOneFace(List<Vector3> vertices, List<Vector2> uv, List<Vector3> normals, List<int> triangles)
    {
        var startIndex = vertices.Count;
        vertices.Add(pointBuffer[0]);
        vertices.Add(pointBuffer[1]);
        vertices.Add(pointBuffer[2]);
        vertices.Add(pointBuffer[3]);

        uv.Add(new Vector2(uvBuffer[0].x, uvBuffer[1].y));
        uv.Add(new Vector2(uvBuffer[1].x, uvBuffer[1].y));
        uv.Add(new Vector2(uvBuffer[1].x, uvBuffer[0].y));
        uv.Add(new Vector2(uvBuffer[0].x, uvBuffer[0].y));

        var normal = Vector3.Cross(pointBuffer[3] - pointBuffer[1], pointBuffer[0] - pointBuffer[2]).normalized;
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);

        triangles.Add(startIndex);
        triangles.Add(startIndex + 3);
        triangles.Add(startIndex + 2);

        triangles.Add(startIndex + 2);
        triangles.Add(startIndex + 1);
        triangles.Add(startIndex);
    }
    /// <summary>
    /// 创建一个地形方块
    /// </summary>
    public static TerrainCube Create(TerrainTypeData terrainTypeData, Vector3 minPosition, Vector3 maxPosition)
    {
        var cubeObj = new GameObject(terrainTypeData.name);
        cubeObj.layer = GameDataHub.instance.groundLayer;
        var position = minPosition;
        position.y = maxPosition.y;
        cubeObj.transform.position = position;
        var terrainCube = cubeObj.AddComponent<TerrainCube>();
        terrainCube.SetTypeData(terrainTypeData);
        terrainCube.SetGeometry(minPosition, maxPosition);
        return terrainCube;
    }
}