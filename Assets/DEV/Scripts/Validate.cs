using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Windows.Input;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using System.Text;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;

public enum TITLE
{
    OU = 0,
    GRP = 1,
    CN = 2,
    DC = 3,
    PWD = 4,
    FN = 5,
    LN = 6
}

public class TitleInfo
{
    public bool hasTitle = false;
    public int col = -1;
    public string title = "none";
}

public class Validate : Singleton<Validate>
{

    private string[] OUTitle = new string[] { "ou", "organiztional units", "organiztional unit" };
    private string[] GroupTitle = new string[] { "grp", "gr", "group", "groups", "gp" };
    private string[] CNTitle = new string[] { "cn", "command name", "command names"};
    private string[] DCTitle = new string[] { "dc", "domain controller", "domain controllers" };
    private string[] PWDTitle = new string[] { "pw", "pwd", "-pwd", "password" };
    private string[] FNTitle = new string[] { "fn", "first name", "fns", "first names" };
    private string[] LNTitle = new string[] { "ln", "last name", "lns", "last names" };


    private List<string[]> AllTitle = new List<string[]>();

    private Dictionary<TITLE, TitleInfo> hasWhatTitle = Enum.GetValues(typeof(TITLE)).Cast<TITLE>().ToDictionary(key => key, value => new TitleInfo());

    public Dictionary<TITLE, TitleInfo> HasWhatTitle
    {
        get { return hasWhatTitle; }
    }

    private StringBuilder log = new StringBuilder();

    private string[,] data;

    public string[,] Data
    {
        get { return data; }
    }



    private bool IsTitle(string strCheck, int col)
    {
        if (AllTitle.Count == 0) return false;

        for (int i = 0; i < AllTitle.Count; i++)
        {
            for (int j = 0; j < AllTitle[i].Length; j++)
            {
                bool isFount = strCheck.Equals(AllTitle[i][j], StringComparison.OrdinalIgnoreCase);
                if (!isFount) continue;

                DicChangeValue((TITLE)i, strCheck, true, col);
                
                return true;
            }
        }

        return false;
    }

    private void DicChangeValue(TITLE title, string titleName, bool hasTitle = true, int col = -1)
    {
        hasWhatTitle[title].title = titleName;
        hasWhatTitle[title].hasTitle = hasTitle;
        hasWhatTitle[title].col = col;
    }

    public void CompleteDataTable(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            StreamReader st = new StreamReader(fs);
            data = DataUtility.CSVToArray(st.ReadToEnd());
            SearchingTitle(data);
            st.Close();
        }
    }

    private Dictionary<TITLE, TitleInfo> SearchingTitle(string[,] data)
    {
        for (int i = 0; i < data.GetLength(1); i++)
        {
            IsTitle(data[0, i], i);
        }

        return hasWhatTitle;
    }

    public UserProfile UploadDataToUserProfileSetting(int row)
    {
        UserProfile userProfile = new UserProfile();
        
        if (!UIManager.Instance.IsOnToggOU && hasWhatTitle[TITLE.OU].hasTitle){ userProfile.OU = GetDataFromRTable(TITLE.OU, row);     } 
        else{ userProfile.OU = UIManager.Instance.InputOU;  }

        userProfile.CN = DataUtility.Standardized(GetDataFromRTable(TITLE.CN, row));

        if (!UIManager.Instance.IsOnToggPWD && hasWhatTitle[TITLE.PWD].hasTitle) { userProfile.PWD = GetDataFromRTable(TITLE.PWD, row);     }
        else { userProfile.PWD = UIManager.Instance.InputPWD;  }
        
        userProfile.DC = UIManager.Instance.InputDC; 

        if (hasWhatTitle[TITLE.FN].hasTitle) { userProfile.FN = GetDataFromRTable(TITLE.FN, row); }
        else { userProfile.FN = "user"; }

        if (hasWhatTitle[TITLE.LN].hasTitle) { userProfile.LN = GetDataFromRTable(TITLE.LN, row); }
        else { userProfile.LN = row.ToString(); }

        return userProfile;
    }

    public GroupProfile UploadDataToGroupProfileSetting(int row)
    {
        GroupProfile userProfile = new GroupProfile();

        return userProfile;
    }


    private string GetDataFromRTable(TITLE title, int row)
    {
        Debug.Log(title + " " + hasWhatTitle[title].col);
        return data[row, hasWhatTitle[title].col];
    }

    public void LogOnError(string message)
    {
        log.Append(BorderText(ColorTextRed("ERROR")) + " " + message + "\n\n");
        UIManager.Instance.ChangeLog(log);
        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_ERROR);
    }

    public void LogOnSystem(string message)
    {
        log.Append(BorderText(ColorTextGreen("SYS")) + " " + message + "\n\n");
        UIManager.Instance.ChangeLog(log);
        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_RUN);
    }

    public string ColorTextRed(string str)
    {
        return "<color=red>" + str + "</color>";
    }

    public string ColorTextGreen(string str)
    {
        return "<color=green>" + str + "</color>";
    }

    public string BorderText(string str)
    {
        return "[" + str + "]";
    }

    private void Awake()
    {
        AllTitle.Add(OUTitle);
        AllTitle.Add(GroupTitle);
        AllTitle.Add(CNTitle);
        AllTitle.Add(DCTitle);
        AllTitle.Add(PWDTitle);
        AllTitle.Add(FNTitle);
        AllTitle.Add(LNTitle);
    }
}
