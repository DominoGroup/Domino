using UnityEngine;
using System.IO;
/// <summary>
/// 扩展方法集合
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// 路径格式切换到超链接和Unity路径
    /// </summary>
    public static string ToUrlPath(this string path)
    {
        return path.Replace('\\', '/');
    }
    /// <summary>
    /// 组合超链接路径的快捷方法
    /// </summary>
    public static string Combine(this string current, string next)
    {
        return current + "/" + next;
    }
    /// <summary>
    /// 移除字符串最后几个字符
    /// </summary>
    public static string RemoveLast(this string current, int length)
    {
        if (current.Length > length)
            return current.Remove(current.Length - length);
        else
            return string.Empty;
    }
    /// <summary>
    /// 为二维数增加一个高度坐标
    /// </summary>
    public static Vector3 AddHeight(this Vector2 source, float height = 0)
    {
        return new Vector3(source.x, height, source.y);
    }
    /// <summary>
    /// 移除三维数的高度坐标
    /// </summary>
    public static Vector2 RemoveHeight(this Vector3 source)
    {
        return new Vector2(source.x, source.z);
    }
    /// <summary>
    /// 路径向上移动一级，或者移除文件名
    /// </summary>
    public static string MoveUp(this string path)
    {
        int index = Mathf.Max(path.LastIndexOf(Path.DirectorySeparatorChar), path.LastIndexOf(Path.AltDirectorySeparatorChar));
        if (index >= 0)
            path = path.Remove(index);
        return path;
    }
    public static T Find<T>(this T[] array, System.Predicate<T> condition)
    {
        T result = default(T);
        for (int i = 0; i < array.Length; i++)
        {
            if (condition.Invoke(array[i]))
            {
                result = array[i];
                break;
            }
        }
        return result;
    }
}