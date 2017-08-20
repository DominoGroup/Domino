using UnityEngine;
[ExecuteInEditMode]
public class TestMeshVerticles : MonoBehaviour
{
    public bool execute;
    private void Update()
    {
        if (execute)
        {
            execute = false;
            var meshFilter = GetComponent<MeshFilter>();
            var mesh = meshFilter.sharedMesh;
            var vertices = mesh.vertices;
            var uv = mesh.uv;
            var uv2 = mesh.uv2;
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                Debug.LogWarning(vertices[i] + ": " + uv[i] + " - " + uv2[i]);
                mesh.subMeshCount = 3;
                
            }
        }
    }
}
