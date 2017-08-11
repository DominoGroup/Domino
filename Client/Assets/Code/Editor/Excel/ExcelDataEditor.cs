using Excel;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class ExcelDataEditor
{
    // 编辑器使用Excel作为数据工具，打包时将数据转为二进制文件并编译到AssetBundle中
    const string excelFileRoot = "Excel";
    const string excelFileExtension = ".xlsx";
    /// <summary>
    /// Excel文件读取一个数据列表
    /// </summary>
    public static List<T> ReadFromExcel<T>(string excelName) where T : ExcelData, new()
    {
        var result = new List<T>();
        using (var fs = File.Open(GetExcelFilePath(excelName), FileMode.Open, FileAccess.Read))
        {
            using (var excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs))
            {
                var table = excelReader.AsDataSet().Tables[0];
                var fields = typeof(T).GetFields();
                var item = new T();
                int rowCount = table.Rows.Count;
                int columnCount = table.Columns.Count;
                // 第一行为变量名称
                List<string> variableNameList = new List<string>();
                for (int i = 0; i < columnCount; i++)
                    variableNameList.Add(table.Rows[0][i].ToString());
                for (int i = 1; i < rowCount; i++)
                {
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
        //throw new System.NotImplementedException();
        return result;
    }
    private static string GetExcelFilePath(string excelName)
    {
        return Application.dataPath.Remove("Assets".Length) + Path.DirectorySeparatorChar + excelFileRoot + Path.DirectorySeparatorChar + excelName + excelFileExtension;
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
        else if (type == typeof(SerilizableVector3))
            return SerilizableVector3.Parse(rawValue);
        else if (type == typeof(SerilizableQuaternion))
            return SerilizableQuaternion.Parse(rawValue);
        else
            throw new System.NotImplementedException(string.Format("未处理类型{0}的读取方式", type));
    }
}