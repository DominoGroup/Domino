using System.Data;
using System.Collections.Generic;
using UnityEngine;
using System;
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
                            //throw new System.NotImplementedException();
                            field.SetValue(item, ConvertObject(row[j], field.FieldType));
                        }
                    }
                    result.Add(item);
                }

            }
        }
        //throw new System.NotImplementedException();
        return result;
    }
    static string GetExcelFilePath(string excelName)
    {
        return Application.dataPath + Path.DirectorySeparatorChar + excelFileRoot + Path.DirectorySeparatorChar + excelName + excelFileExtension;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    static public object ConvertObject(object obj, System.Type type)
    {
        if (type == null) return obj;

        if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

        Type underlyingType = Nullable.GetUnderlyingType(type);
        if (type.IsAssignableFrom(obj.GetType())) // 如果待转换对象的类型与目标类型兼容，则无需转换
        {
            return obj;
        }
        else if ((underlyingType ?? type).IsEnum) // 如果待转换的对象的基类型为枚举
        {
            if (underlyingType != null && string.IsNullOrEmpty(obj.ToString())) // 如果目标类型为可空枚举，并且待转换对象为null 则直接返回null值
            {
                return null;
            }
            else
            {
                return Enum.Parse(underlyingType ?? type, obj.ToString());
            }
        }
        else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type)) // 如果目标类型的基类型实现了IConvertible，则直接转换
        {
            try
            {
                return Convert.ChangeType(obj, underlyingType ?? type, null);
            }
            catch
            {
                return underlyingType == null ? Activator.CreateInstance(type) : null;
            }
        }
        else
        {
            var splitValue = (obj as string).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var args = new object[splitValue.Length];
            
            for (int i = 0; i < splitValue.Length; i++)
            {
                args[i] = ConvertObject(splitValue[i], typeof(float));
            }
            return Activator.CreateInstance(type, args);
        }
    }
}