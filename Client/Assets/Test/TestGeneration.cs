using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGeneration : MonoBehaviour
{
    public Material top;
    public Material side;
    public Material transistion;
    public int width;
    public int height;
    
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    private void Start ()
    {
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer.sharedMaterials = new Material[] { top, side };//, transistion };
        var mesh = new Mesh();
        var vertices = new List<Vector3>();
        var uv = new List<Vector2>();
        var normals = new List<Vector3>();
        mesh.subMeshCount = 2;

        var min = new Vector3(0f, -1f, 0f);
        var max = new Vector3(width, 0f, height);
        // 计算方法顶面
        int startIndex = vertices.Count;
        vertices.Add(new Vector3(min.x, max.y, min.z));
        vertices.Add(new Vector3(max.x, max.y, min.z));
        vertices.Add(new Vector3(max.x, max.y, max.z));
        vertices.Add(new Vector3(min.x, max.y, max.z));
        normals.Add(Vector3.up);
        normals.Add(Vector3.up);
        normals.Add(Vector3.up);
        normals.Add(Vector3.up);
        uv.Add(new Vector2(0, 0));
        uv.Add(new Vector2(1, 0));
        uv.Add(new Vector2(1, 1));
        uv.Add(new Vector2(0, 1));
        var triangles_0 = new List<int>();
        triangles_0.Add(startIndex);
        triangles_0.Add(startIndex + 3);
        triangles_0.Add(startIndex + 1);
        triangles_0.Add(startIndex + 1);
        triangles_0.Add(startIndex + 3);
        triangles_0.Add(startIndex + 2);
        // 计算方法侧面
        startIndex = vertices.Count;
        vertices.Add(new Vector3(min.x, max.y, min.z));
        vertices.Add(new Vector3(max.x, max.y, min.z));
        vertices.Add(new Vector3(max.x, max.y, max.z));
        vertices.Add(new Vector3(min.x, max.y, max.z));
        vertices.Add(new Vector3(min.x, min.y, min.z));
        vertices.Add(new Vector3(max.x, min.y, min.z));
        vertices.Add(new Vector3(max.x, min.y, max.z));
        vertices.Add(new Vector3(min.x, min.y, max.z));
        
        uv.Add(new Vector2(0, 1));
        uv.Add(new Vector2(1, 1));
        uv.Add(new Vector2(0, 1));
        uv.Add(new Vector2(1, 1));
        uv.Add(new Vector2(0, 0));
        uv.Add(new Vector2(1, 0));
        uv.Add(new Vector2(0, 0));
        uv.Add(new Vector2(1, 0));
        var triangles_1 = new List<int>();
        triangles_1.Add(startIndex + 4);
        triangles_1.Add(startIndex + 3);
        triangles_1.Add(startIndex);

        triangles_1.Add(startIndex + 4);
        triangles_1.Add(startIndex + 7);
        triangles_1.Add(startIndex + 3);
        
        triangles_1.Add(startIndex + 6);
        triangles_1.Add(startIndex + 2);
        triangles_1.Add(startIndex + 3);

        triangles_1.Add(startIndex + 7);
        triangles_1.Add(startIndex + 6);
        triangles_1.Add(startIndex + 3);

        triangles_1.Add(startIndex + 2);
        triangles_1.Add(startIndex + 6);
        triangles_1.Add(startIndex + 5);

        triangles_1.Add(startIndex + 5);
        triangles_1.Add(startIndex + 1);
        triangles_1.Add(startIndex + 2);

        triangles_1.Add(startIndex + 1);
        triangles_1.Add(startIndex + 4);
        triangles_1.Add(startIndex);

        triangles_1.Add(startIndex + 5);
        triangles_1.Add(startIndex + 4);
        triangles_1.Add(startIndex + 1);

        mesh.SetVertices(vertices);
        mesh.SetUVs(0, uv);
        mesh.SetTriangles(triangles_0, 0);
        mesh.SetTriangles(triangles_1, 1);
        mesh.RecalculateTangents();
        meshFilter.mesh = mesh;
    }
}