using System.Collections.Generic;
using UnityEngine;

public class ExcelDataHub : Singleton<ExcelDataHub>
{
    public ExcelDataHub()
    {
        
    }
    /// <summary>
    /// 二进制文件读取一个数据列表
    /// </summary>
    public static List<T> ReadFromBinary<T>(string excelName) where T : ExcelData
    {
        throw new System.NotImplementedException();
    }
}
/// <summary>
/// 标识一个类可以从Excel表格构造的基类
/// </summary>
public abstract class ExcelData
{
}