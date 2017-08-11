using UnityEngine;
using System.Collections.Generic;
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