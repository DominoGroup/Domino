using Excel;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 将数据打包到AssetBundle的工具
/// </summary>
public class ExcelDataEditor
{
    // 编辑器使用Excel作为数据工具，打包时将数据转为二进制文件并编译到AssetBundle中
    const string excelFileRoot = "Excel";
    const string excelFileExtension = ".xlsx";
    const string xmlFileExtension = ".xml";
    [MenuItem("Utility/Read Excel Data")]
    public static void ReadAllExcelData()
    {
        ConvertFromExcel<MapItemData>(ExcelDataHub.mapItemFileName);
        ConvertFromExcel<TerrainTypeData>(ExcelDataHub.terrainFileName);
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 读取一个Excel文件，然后存储成Xml
    /// </summary>
    /// <param name="excelName"></param>
    private static void ConvertFromExcel<T>(string excelName) where T : ExcelData, new()
    {
        var result = new List<T>();
        using (var fs = File.Open(GetExcelFilePath(excelName), FileMode.Open, FileAccess.Read))
        {
            using (var excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs))
            {
                var table = excelReader.AsDataSet().Tables[0];
                var fields = typeof(T).GetFields();
                int rowCount = table.Rows.Count;
                int columnCount = table.Columns.Count;
                // 第一行为变量名称
                List<string> variableNameList = new List<string>();
                for (int i = 0; i < columnCount; i++)
                    variableNameList.Add(table.Rows[0][i].ToString());
                for (int i = 1; i < rowCount; i++)
                {
                    var item = new T();
                    var row = table.Rows[i];
                    for (int j = 0; j < fields.Length; j++)
                    {
                        var field = fields[j];
                        var index = variableNameList.IndexOf(field.Name);
                        if (index < 0)
                            Debug.LogError(string.Format("Excel表格{0}中，无法找到{1}字段", excelName, field.Name));
                        else
                            field.SetValue(item, ConvertObject(row[j].ToString(), field.FieldType));
                    }
                    result.Add(item);
                }
            }
        }
        SaveToXml(excelName, result);
    }
    
    private static void SaveToXml<T>(string excelName, List<T> data) where T : ExcelData
    {
        var xmlPath = Application.dataPath.Combine(PathConst.assetBundleRoot).Combine(PathConst.dataBundle).Combine(excelName + xmlFileExtension);
        using (var fs = File.Create(xmlPath))
        {
            XmlSerializer serilizer = new XmlSerializer(typeof(List<T>));
            serilizer.Serialize(fs, data);
        }
    }

    private static string GetExcelFilePath(string excelName)
    {
        return Application.dataPath.RemoveLast("Assets".Length).Combine(excelFileRoot).Combine(excelName + excelFileExtension);
    }
    /// <summary>
    /// 按照Field类型设定FieldInfo的数值
    /// </summary>
    // 按照物体类型将字符串转到所需类型
    private static object ConvertObject(string rawValue, System.Type type)
    {
        if (type == typeof(int))
            return int.Parse(rawValue);
        else if (type == typeof(float))
            return float.Parse(rawValue);
        else if (type == typeof(string))
            return rawValue;
        else if (type == typeof(SerilizableVector2))
            return SerilizableVector2.Parse(rawValue);
        else if (type == typeof(SerilizableVector3))
            return SerilizableVector3.Parse(rawValue);
        else if (type == typeof(SerilizableQuaternion))
            return SerilizableQuaternion.Parse(rawValue);
        else
            throw new System.NotImplementedException(string.Format("未处理类型{0}的读取方式", type));
    }
}