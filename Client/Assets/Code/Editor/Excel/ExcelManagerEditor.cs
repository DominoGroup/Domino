using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 测试用
/// </summary>
public class ExcelManagerEditor : EditorWindow
{
    [MenuItem("ExcelManager/ReadExcel")]
    static void ReadMapItemExcelData()
    {
        List<MapItemData> items = ExcelDataEditor.ReadFromExcel<MapItemData>("MapItem");
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i].id + items[i].name + items[i].size);
        }
    }
}
