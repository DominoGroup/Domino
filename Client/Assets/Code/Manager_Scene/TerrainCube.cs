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
            this.cubeData = cubeData;
    }
}