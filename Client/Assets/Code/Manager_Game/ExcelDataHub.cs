using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
public class ExcelDataHub : Singleton<ExcelDataHub>
{
    #region 静态路径
    // 注：Excel文件名为静态路径+".xlsx"
    public const string mapItemFileName = "MapItem";
    public readonly List<MapItemData> mapItemData;
    #endregion

    public ExcelDataHub()
    {
        mapItemData = ReadFromBinary<MapItemData>(mapItemFileName);
    }
    public MapItemData GetMapItemData(int id)
    {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// 二进制文件读取一个数据列表
    /// </summary>
    private static List<T> ReadFromBinary<T>(string excelName) where T : ExcelData
    {
        var textAsset = AssetHub.instance.GetAsset<TextAsset>(PathConst.dataBundle, excelName);
        List<T> result = null;
        using (var textReader = new StringReader(textAsset.text))
        {
            XmlSerializer serilizer = new XmlSerializer(typeof(List<T>));
            result = (List<T>)serilizer.Deserialize(textReader);
        }
        return result;
    }
}
/// <summary>
/// 标识一个类可以从Excel表格构造的基类
/// </summary>
public abstract class ExcelData
{
}