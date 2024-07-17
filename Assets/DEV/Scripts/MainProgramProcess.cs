using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Text;
using SFB;
using System;

public enum MODE
{
    CREATE_USER = 0,
    CREATE_GROUP = 1,
    CREATE_OU = 2
}



public class MainProgramProcess : Singleton<MainProgramProcess>
{
    //private MODE mode = MODE.CREATE_USER;
    private string pathOfData = "";
    private string pathOfLocationBat = "";

    private string dataBat = "";
    private StringBuilder stringData = new StringBuilder();

    public string PathOfData
    {
        get => pathOfData;
        set => pathOfData = value;
    }

    public string PathOfLocationBat
    {
        get => pathOfLocationBat; 
        set => pathOfLocationBat = value;
    }


    public void OpenFExAndAssignDataPath()
    {
        pathOfData = OpenFileExplore();
    }

    public void OpenFExAndAssignLocationBatPath()
    {
        pathOfLocationBat = OpenFolderExplore();
    }

    public string OpenFileExplore()
    {
        string[] path = StandaloneFileBrowser.OpenFilePanel("Choose", "", "csv", false);

        return path[0];
    }

    public string OpenFolderExplore()
    {
        string[] path = StandaloneFileBrowser.OpenFolderPanel("Choose Location", "", false);

        return path[0];
    }

    public string LoopToAddUser()
    {
        int count = Validate.Instance.Data.GetLength(0) - 1;
        for (int i = 1; i <= count; i++)
        {
            stringData.Append(CommandCMD.CommandCreateUser(Validate.Instance.UploadDataToUserProfileSetting(i)));
            stringData.Append("\n");
        }

        return stringData.ToString();
    }

    public bool Validatepath()
    {
        if (string.IsNullOrEmpty(PathOfData))
        {
            Validate.Instance.LogOnError("Path data file is not define");
            return false;
        }
        if (string.IsNullOrEmpty(PathOfLocationBat))
        {
            Validate.Instance.LogOnError("Path of folder for batch file is not define");
            return false;
        }

        return true;
    }

    public bool InputValidateIsTrue()
    {
        if (Validatepath() == false) return false;

        if (string.IsNullOrEmpty(UIManager.Instance.InputDC))
        {
            Validate.Instance.LogOnError("DC is not define");
            return false;
        }
        if ((UIManager.Instance.IsOnToggOU && string.IsNullOrEmpty(UIManager.Instance.InputOU))
            ||
            (!UIManager.Instance.IsOnToggOU && Validate.Instance.HasWhatTitle[TITLE.OU].hasTitle))
        {
            Validate.Instance.LogOnError("OU is not define");
            return false;
        }
        if ((UIManager.Instance.IsOnToggPWD && string.IsNullOrEmpty(UIManager.Instance.InputPWD))
            ||
            (!UIManager.Instance.IsOnToggPWD && Validate.Instance.HasWhatTitle[TITLE.PWD].hasTitle))
        {
            Validate.Instance.LogOnError("PWD is not define");
            return false;
        }
        if (!Validate.Instance.HasWhatTitle[TITLE.CN].hasTitle)
        {
            Validate.Instance.LogOnError("CN is not define");
            return false;
        }

        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_WAIT);
        return true;
    }

    public void PrcessingAll()
    {
        if(Validatepath())
        {
            string path = Path.Combine(PathOfLocationBat, "CREATE_USER_" + DateTime.Now.ToString().Replace("/", "_").Replace(@" ", "_").Replace(":", "_").Replace(@"\", @"\\") + ".bat");
            Validate.Instance.CompleteDataTable(PathOfData);

            if (InputValidateIsTrue())
            {
                ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_RUN);
                Validate.Instance.LogOnSystem("Running . . .");
                CommandCMD.WriteFileBat(LoopToAddUser(), path);
                CommandCMD.RunBat(path);
            }
        }
    }
}
