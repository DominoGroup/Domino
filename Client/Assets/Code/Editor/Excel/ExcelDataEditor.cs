using System.Data;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using Excel;
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
                        {
                            // 赋值根据Field类型予以赋值的方法需要增加
                            throw new System.NotImplementedException();
                            //fields[i].SetValue(item, );
                        }
                    }
                }

            }
        }
        throw new System.NotImplementedException();
    }
    static string GetExcelFilePath(string excelName)
    {
        return Application.dataPath + Path.DirectorySeparatorChar + excelFileRoot + Path.DirectorySeparatorChar + excelName + excelFileExtension;
    }
}