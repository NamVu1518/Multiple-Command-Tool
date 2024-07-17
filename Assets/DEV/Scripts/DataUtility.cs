using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using Unity.VisualScripting.FullSerializer;

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
            if (str[j + 1] == ',' && isMiddelMarks == true)
            {
                str = str.Substring(0, j + 1) + pwdComma + str.Substring(j + 2);
            }
        }

        if (hasMarks)
        {
            str = str.Replace("\"", "");
        }

        return str.Split(',').Select(x => x.Replace(pwdComma, ",")).ToList();
    }

    public static string Standardized(string str)
    {
        return Regex.Replace(VietnameseProcess.Instance.RemoveSign4VietnameseString(str),"[^a-zA-Z0-9]","");
    }

    public static string[] RemoveNullCellCSV(string str)
    {
        List<string> strList = ValidateMarks(str);

        for (int i = strList.Count - 1; i >= 0; i--)
        {
            if (string.IsNullOrEmpty(strList[i]))
            {
                strList.Remove(strList[i]);
                continue;
            }

            break;
        }

        return strList.ToArray();
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

            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (min == 0) break;

                data[i, j] = RemoveNullCellCSV(rows[i])[j];

                count++;
                if (count >= min) break;
            }
        }

        return data;
    }
}
