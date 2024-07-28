using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class CreateUserProcess
{
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

    public void OpenFExAndAssignLocationRunFilePath()
    {
        pathOfLocationBat = OpenFolderExplore();
    }

    public string OpenFileExplore()
    {
        string[] path = StandaloneFileBrowser.OpenFilePanel("Choose", "", "csv", false);

        if (path.Length <= 0) return "";

        return path[0];
    }

    public string OpenFolderExplore()
    {
        string[] path = StandaloneFileBrowser.OpenFolderPanel("Choose Location", "", false);

        if (path.Length <= 0) return "";

        return path[0];
    }

    public string LoopToAddUser()
    {
        int count = Validate.Instance.Data.GetLength(0) - 1;
        for (int i = 1; i <= count; i++)
        {
            if (ProgramLifeCycle.Instance.IsError) return stringData.ToString();
            stringData.Append(CommandCMD.CommandCreateUser(UploadDataToUserProfileSetting(i)));
            stringData.Append("\n");
        }

        return stringData.ToString();
    }

    public bool ValidatePath()
    {
        if (string.IsNullOrEmpty(PathOfData))
        {
            LogString.LogError(LogString.Log.Error.ERR_PATH001);
            return false;
        }
        if (string.IsNullOrEmpty(PathOfDirectoryFileRun))
        {
            LogString.LogError(LogString.Log.Error.ERR_PATH002);
            return false;
        }

        return true;
    }

    public bool InputValidateMultipleIsTrue()
    {
        if (!Validate.Instance.HasWhatTitle[TITLE.CN].hasTitle)
        {
            LogString.LogError(LogString.Log.Error.ERR_CN001);
            return false;
        }

        if (!Validate.Instance.HasWhatTitle[TITLE.FN].hasTitle)
        {
            LogString.LogWarning(LogString.Log.Warning.WARN_FN001);
        }

        if (!Validate.Instance.HasWhatTitle[TITLE.LN].hasTitle)
        {
            LogString.LogWarning(LogString.Log.Warning.WARN_LN001);
        }

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildDC() == DefineStatus.DEFINE_IN_CSV && !Validate.Instance.HasWhatTitle[TITLE.DC].hasTitle) 
        {
            LogString.LogError(LogString.Log.Error.ERR_DC003);
            return false;
        }
         
        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildDC() == DefineStatus.DEFINE_HERE && string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputDC.text))
        {
            LogString.LogError(LogString.Log.Error.ERR_DC002);
            return false;
        }

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildOU() == DefineStatus.DEFINE_IN_CSV && !Validate.Instance.HasWhatTitle[TITLE.OU].hasTitle)
        {
            LogString.LogError(LogString.Log.Error.ERR_OU003);
            return false;
        }

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildOU() == DefineStatus.DEFINE_HERE && string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputOU.text))
        {
            LogString.LogError(LogString.Log.Error.ERR_OU002);
            return false;
        }

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildPassword() == DefineStatus.DEFINE_IN_CSV && !Validate.Instance.HasWhatTitle[TITLE.PWD].hasTitle)
        {
            LogString.LogError(LogString.Log.Error.ERR_PWD002);
            return false;
        }

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildPassword() == DefineStatus.DEFINE_HERE && string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputPassword.text))
        {
            LogString.LogError(LogString.Log.Error.ERR_PWD001);
            return false;
        }

        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_WAIT);
        return true;
    }

    public bool InputValidateSingleIsTrue()
    {
        if (string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputCN.text))
        {
            LogString.LogError(LogString.Log.Error.ERR_CN002);
            return false;
        }

        if (string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputFN.text))
        {
            LogString.LogWarning(LogString.Log.Warning.WARN_FN001);
        }

        if (string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputLN.text))
        {
            LogString.LogWarning(LogString.Log.Warning.WARN_LN001);
        }

        if (string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputDC.text))
        {
            LogString.LogError(LogString.Log.Error.ERR_DC002);
            return false;
        }

        if (string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputOU.text))
        {
            LogString.LogError(LogString.Log.Error.ERR_OU002);
            return false;
        }

        if (string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputPassword.text))
        {
            LogString.LogError(LogString.Log.Error.ERR_PWD001);
            return false;
        }

        return true;
    }

    public UserProfile UploadDataToUserProfileSetting(int row)
    {
        UserProfile userProfile = new UserProfile();

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildDC() == DefineStatus.DEFINE_IN_CSV) 
        { 
            userProfile.DC = Validate.Instance.GetDataFromTable(TITLE.DC, row); 
        }
        else if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildDC() == DefineStatus.DEFINE_HERE) 
        { 
            userProfile.DC = UIManager.Instance.GetInstanceUI<UICreateUser>().InputDC.text; 
        }

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildOU() == DefineStatus.DEFINE_IN_CSV) 
        { 
            userProfile.OU = Validate.Instance.GetDataFromTable(TITLE.OU, row); 
        }
        else if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildOU() == DefineStatus.DEFINE_HERE) 
        { 
            userProfile.OU = UIManager.Instance.GetInstanceUI<UICreateUser>().InputOU.text; 
        }

        userProfile.CN = DataUtility.Standardized(Validate.Instance.GetDataFromTable(TITLE.CN, row));

        if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildPassword() == DefineStatus.DEFINE_IN_CSV) 
        { 
            userProfile.PWD = Validate.Instance.GetDataFromTable(TITLE.PWD, row); 
        }
        else if (UIManager.Instance.GetInstanceUI<UICreateUser>().WhichStatusFeildPassword() == DefineStatus.DEFINE_HERE) 
        { 
            userProfile.PWD = UIManager.Instance.GetInstanceUI<UICreateUser>().InputPassword.text; 
        }

        if (Validate.Instance.HasWhatTitle[TITLE.FN].hasTitle 
            && Validate.Instance.HasWhatTitle[TITLE.LN].hasTitle) 
        { 
            userProfile.FN = Validate.Instance.GetDataFromTable(TITLE.FN, row);
            userProfile.LN = Validate.Instance.GetDataFromTable(TITLE.LN, row);
        }

        return userProfile;
    }

    private UserProfile UploadDataToUserProfileSetting()
    {
        UserProfile userProfile = new UserProfile();

        userProfile.CN = UIManager.Instance.GetInstanceUI<UICreateUser>().InputCN.text;
        userProfile.DC = UIManager.Instance.GetInstanceUI<UICreateUser>().InputDC.text;
        userProfile.OU = UIManager.Instance.GetInstanceUI<UICreateUser>().InputOU.text;
        userProfile.PWD = UIManager.Instance.GetInstanceUI<UICreateUser>().InputPassword.text;
        if (!string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputFN.text)
            && !string.IsNullOrEmpty(UIManager.Instance.GetInstanceUI<UICreateUser>().InputLN.text)) 
        {
            userProfile.FN = UIManager.Instance.GetInstanceUI<UICreateUser>().InputFN.text;
            userProfile.LN = UIManager.Instance.GetInstanceUI<UICreateUser>().InputLN.text;
        }

        return userProfile;
    }

    public void PrcessingRun(bool runNow = true)
    {
        if (MainProgramProcess.Instance.MultipleChoise == MultipleChoise.MULTIPLE)
        {
            if (ValidatePath())
            {
                string path = Path.Combine(PathOfDirectoryFileRun, "CREATE_USER_" + DateTime.Now.ToString().Replace("/", "_").Replace(@" ", "_").Replace(":", "_").Replace(@"\", @"\\") + ".ps1");
                Validate.Instance.CompleteDataTable(PathOfData);

                if (InputValidateMultipleIsTrue())
                {
                    ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_RUN);
                    LogString.LogSystem(LogString.Log.System.SYS_RUNNING);
                    
                    string data = LoopToAddUser();

                    if (ProgramLifeCycle.Instance.IsError) return;
                        
                    CommandCMD.WriteFilePS1(data, path);

                    if (!runNow)
                    {
                        LogString.LogSystem(LogString.Log.System.SYS_DONE);
                        return;
                    }

                    CommandCMD.RunCommandPS1(path, false);                   
                }
            }
        }
        else if (MainProgramProcess.Instance.MultipleChoise == MultipleChoise.SINGLE)
        {
            if (InputValidateSingleIsTrue())
            {
                ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_RUN);
                LogString.LogSystem(LogString.Log.System.SYS_RUNNING);
                string data = CommandCMD.CommandCreateUser(UploadDataToUserProfileSetting());

                if (!ProgramLifeCycle.Instance.IsError)
                {
                    CommandCMD.RunCommandPS1(data, true);
                    CommandCMD.RunCommandPS1(data, true);
                }
            }
        }
    }
}
