using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using Unity.VisualScripting.FullSerializer;
using System.Text;

public class DataUtility
{
    public static List<string> ValidateMarks(string str)
    {
        bool isMiddelMarks = false;
        bool hasMarks = false;
        string pwdComma = "Kdtvstrg";

        for (int j = 0; j < str.Length - 1; j++)
        {
            if (str[j] == '\"')
            {
                isMiddelMarks = !isMiddelMarks;
                if (!hasMarks) hasMarks = true;
            }
            if (str[j] == ',' && isMiddelMarks == true)
            {
                str = str.Substring(0, j) + pwdComma + str.Substring(j + 1);
            }
        }

        if (hasMarks)
        {
            str = str.Replace("\"", "");
        }
        
        List<string> listStr = str.Split(',').Select(x => x.Replace(pwdComma, ",")).ToList();

        return listStr;
    }

    public static string Standardized(string str)
    {
        return Regex.Replace(VietnameseProcess.Instance.RemoveSign4VietnameseString(str),"[^a-zA-Z0-9]","");
    }

    public static string StringCleaner(string str)
    {
        string reStr = str.Trim().Replace("\r", "").Replace(",", "").Replace("\t", "");
        return reStr;
    }

    public static string[] RemoveNullCellCSV(string str)
    {
        List<string> strList = ValidateMarks(str);

        for (int i = strList.Count - 1; i >= 0; i--)
        {
            if (string.IsNullOrWhiteSpace(strList[i]))
            {
                strList.RemoveAt(i);
                continue;
            }

            break;
        }

        return strList.ToArray();
    }

    public static string ConvertToVisibleString(string input)
    {
        if (input == null)
        {
            return "null";
        }

        StringBuilder visibleString = new StringBuilder();
        foreach (char c in input)
        {
            switch (c)
            {
                case ' ':
                    visibleString.Append("\\s");
                    break;
                case '\n':
                    visibleString.Append("\\n");
                    break;
                case '\t':
                    visibleString.Append("\\t");
                    break;
                case '\r':
                    visibleString.Append("\\r");
                    break;
                default:
                    if (char.IsControl(c))
                    {
                        visibleString.Append("\\u" + ((int)c).ToString("x4"));
                    }
                    else
                    {
                        visibleString.Append(c);
                    }
                    break;
            }
        }

        return visibleString.ToString();
    }

    public static bool IsEmptyRow(string row)
    {
        return !Regex.IsMatch(row, @"[^,\n\s]");
    }

    public static string[] RemoveEmptyRow(string csvData)
    {
        List<string> rows = csvData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        for (int i = rows.Count - 1; i >= 0; i--)
        {
            if (IsEmptyRow(rows[i]))
            {
                rows.Remove(rows[i]);
                continue;
            }

            break;
        }

        return rows.ToArray();
    }

    public static int CountTitle(string title)
    {
        return RemoveNullCellCSV(title).Length;
    }

    public static int CountRow(string csvData)
    {
        return RemoveEmptyRow(csvData).Length;
    }

    public static int CountCol(string row)
    {
        return CountTitle(row);
    }

    public static string[,] CSVToArray(string csvData)
    {
        string[] rows = csvData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        string[,] data = new string[CountRow(csvData), CountTitle(rows[0])];

        for (int i = 0; i < data.GetLength(0); i++)
        {
            int min = Math.Min(CountCol(rows[i]), data.GetLength(1));
            int count = 0;

            string[] strings = RemoveNullCellCSV(rows[i]);
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (min == 0) break;

                data[i, j] = StringCleaner(strings[j]);

                count++;
                if (count >= min) break;
            }
        }

        return data;
    }
}
