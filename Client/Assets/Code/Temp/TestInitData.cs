using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
[ExecuteInEditMode]
public class TestInitData : MonoBehaviour
{
    public bool execute;
    public string fileName = "example_0";
    private void Update()
    {
        if (execute)
        {
            execute = false;
            var data = new SceneData();
            data.itemData = new List<SceneItemData>();
            data.terrainData = new List<TerrainCubeData>();
            data.itemData.Add(new SceneItemData() { mapItemId = 1, position = Vector3.zero.ToSerilizable(), rotation = Quaternion.identity.ToSerilizable() });
            data.terrainData.Add(new TerrainCubeData() { terrainId = 1, minValue = Vector3.zero.ToSerilizable(), maxValue = new Vector3(20f, 3f, 20f).ToSerilizable() });
            var filePath = Application.dataPath.Combine(PathConst.assetBundleRoot).Combine(PathConst.sceneDataBundle).Combine(fileName + ".xml");
            using (var fs = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(SceneData));
                serializer.Serialize(fs, data);
            }
        }
    }
}