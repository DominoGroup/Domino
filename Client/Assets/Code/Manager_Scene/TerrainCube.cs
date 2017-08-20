using UnityEngine;
/// <summary>
/// 地形方块
/// </summary>
public class TerrainCube : MonoBehaviour
{
    public TerrainCubeData cubeData { get; private set; }
    
    public void SetCubeData(TerrainCubeData cubeData)
    {
#if UNITY_EDITOR
        if (this.cubeData != null)
            throw new System.Exception(string.Format("地形数据被重复构造：{0}", gameObject.name));
        else
#endif
        {
            this.cubeData = cubeData;
            var meshFilter = gameObject.AddComponent<MeshFilter>();
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            var boxCollider = gameObject.AddComponent<BoxCollider>();
            var size = (cubeData.maxValue.ToVector2() - cubeData.minValue.ToVector2()).AddHeight(TerrainCubeData.thickness);

            boxCollider.center = new Vector3();

            // 拼合地形效果







        }
    }
}