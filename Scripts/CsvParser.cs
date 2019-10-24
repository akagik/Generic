using System.Collections.Generic;
using System.Text.RegularExpressions;

//#if UNITY_EDITOR
//using UnityEngine;
//using UnityEditor;
//#endif

/// <summary>
/// original: <https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/>
/// </summary>
public static class CsvParser
{
    //[MenuItem("Test/CsvParser", false, 0)]
    //public static void Test()
    //{
    //    string csv = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Test/table2.csv").text;

    //    var grid = ReadAsList(csv);

    //    string str = "";
    //    for (int i = 0; i < grid.Count; i++)
    //    {
    //        for (int j = 0; j < grid[i].Count; j++)
    //        {
    //            str += "[" +grid[i][j] + "], ";
    //        }
    //        str += "\n";
    //    }
    //    Debug.Log(str);
    //}

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    //static char[] TRIM_CHARS = { '\"' };

    public static List<List<string>> ReadAsList(string text)
    {
        var list = new List<List<string>>();

        var lines = new List<string>(Regex.Split(text, LINE_SPLIT_RE));

        // 最後の行が空行なら削除する.
        if (lines[lines.Count - 1].Length == 0)
        {
            lines.RemoveAt(lines.Count - 1);
        }

        if (lines.Count <= 0) return list;

        for (var i = 0; i < lines.Count; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0) continue;

            list.Add(new List<string>());

            for (var j = 0; j < values.Length; j++)
            {
                string value = values[j].Trim();
                value = UnquoteString(value);
                value = value.Replace("\"\"", "\"");
                value = value.Replace("\\n", "\n");
                value = value.Replace("\\", "");

                //value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                list[i].Add(value);
            }
        }

        return list;
    }

    public static string UnquoteString(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        int length = str.Length;

        if (length > 1 && str[0] == '\"' && str[length - 1] == '\"')
        {
            str = str.Substring(1, length - 2);
        }

        return str;
    }
}