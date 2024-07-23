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

    public string PathOfDirectoryFileRun
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

    public bool ValidatePath()
    {
        if (string.IsNullOrEmpty(PathOfData))
        {
            LogString.LogOnError(LogString.Log.Error.ERR_PATH001);
            return false;
        }
        if (string.IsNullOrEmpty(PathOfDirectoryFileRun))
        {
            LogString.LogOnError(LogString.Log.Error.ERR_PATH002);
            return false;
        }

        return true;
    }

    public bool InputValidateIsTrue()
    {
        //UNDONE

        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_WAIT);
        return true;
    }

    public void PrcessingAll()
    {
        if(ValidatePath())
        {
            string path = Path.Combine(PathOfDirectoryFileRun, "CREATE_USER_" + DateTime.Now.ToString().Replace("/", "_").Replace(@" ", "_").Replace(":", "_").Replace(@"\", @"\\") + ".bat");
            Validate.Instance.CompleteDataTable(PathOfData);

            if (InputValidateIsTrue())
            {
                ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_RUN);
                LogString.LogOnError(LogString.Log.System.SYS_RUNNING);
                CommandCMD.WriteFileBat(LoopToAddUser(), path);
                CommandCMD.RunBat(path);
            }
        }
    }
}
