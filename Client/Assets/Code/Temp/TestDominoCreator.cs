using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
public class TestDominoCreator : MonoBehaviour
{
    ScenePlayer scenePlayer;
    InputField loadInput;
    InputField saveInput;
    private void Start()
    {
        loadInput = transform.Find("LoadInput").GetComponent<InputField>();
        saveInput = transform.Find("SaveInput").GetComponent<InputField>();
        transform.Find("LoadButton").GetComponent<Button>().onClick.AddListener(OnLoadButton);
        transform.Find("SaveButton").GetComponent<Button>().onClick.AddListener(OnSaveButton);
        scenePlayer = FindObjectOfType<ScenePlayer>();
    }
    private void OnLoadButton()
    {
        Debug.Log("Load Scene Data");
        var fileName = CheckFileName(loadInput.text);
        if (!string.IsNullOrEmpty(fileName))
            fileName = Application.dataPath.Combine(PathConst.assetBundleRoot).Combine(PathConst.sceneDataBundle).Combine(fileName);
        if (File.Exists(fileName))
        {
            SceneData data = null;
            using (var fs = File.OpenRead(fileName))
            {
                var serializer = new XmlSerializer(typeof(SceneData));
                data = (SceneData)serializer.Deserialize(fs);
            }
            scenePlayer.ReadData(data);
        }
        else
            Debug.LogError(string.Format("文件不存在，路径{0}", fileName));
    }
    private void OnSaveButton()
    {
        Debug.Log("Save Scene Data");
        var fileName = CheckFileName(saveInput.text);
        if (!string.IsNullOrEmpty(fileName))
            fileName = Application.dataPath.Combine(PathConst.sceneDataBundle).Combine(fileName);
        // Hack：由于复制构造不经过ScenePlayer，因此数据也不从ScenePlayer获得
        var data = new SceneData();
        var items = FindObjectsOfType<MapItem>();
        var cubes = FindObjectsOfType<TerrainCube>();
        var itemDataList = new List<SceneItemData>();
        var cubeDatalist = new List<TerrainCubeData>();
        for (int i = 0; i < items.Length; i++)
            itemDataList.Add(scenePlayer.itemHub.GetItemData(items[i]));
        for (int i = 0; i < cubes.Length; i++)
            cubeDatalist.Add(scenePlayer.terrainHub.GetItemData(cubes[i]));
        data.itemData = itemDataList;
        data.terrainData = cubeDatalist;

        using (var fs = File.Open(fileName, FileMode.Create, FileAccess.Write))
        {
            var serializer = new XmlSerializer(typeof(SceneData));
            serializer.Serialize(fs, data);
        }
    }
    string CheckFileName(string fileName)
    {
        var result = string.Empty;
        if (string.IsNullOrEmpty(fileName))
            Debug.LogError("文件名未设定");
        else
        {
            var validFileName = Path.GetFileName(fileName);
            if (validFileName != fileName)
                Debug.LogError("不允许额外添加路径");
            else
            {
                var extension = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(extension))
                    result = fileName + ".xml";
                else if (extension == ".xml")
                    result = fileName;
                else
                    Debug.LogError(string.Format("后缀名{0}不是xml文件标准", extension));
            }
        }
        return result;
    }
}