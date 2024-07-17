using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Diagnostics;
using System.IO;

public class UserProfile
{
    public string CN;
    public string OU;
    public string DC;
    public string PWD;
    public string FN = "";
    public string LN = "";
}

public class GroupProfile
{
    public string CN;
    public string OU;
    public string DC;
}

public static class CommandCMD
{
    public static string CommandCreateUser(UserProfile userProfile)
    {
        return CommandCreateUser(userProfile.CN, userProfile.OU, userProfile.DC, userProfile.PWD, userProfile.FN, userProfile.LN);
    }

    public static string CommandCreateUser(string CN, string OU, string DC, string PWD, string fn, string ln)
    {
        string command = $"dsadd user ";

        string sub = @"CN=" + CN;
        string[] OUArray = OU_Process(OU);
        for (int i = OUArray.Length - 1; i >= 0; i--)
        {
            sub += $", OU={OUArray[i]}";
        }

        string[] DCArray = DC_Process(DC);
        for (int i = 0; i < DCArray.Length; i++)
        {
            sub += $", DC={DCArray[i]}";
        }

        command += Mark(sub);
        command += "-upn " + Mark(CN + "@" + DC);
        command += "-fn " + Mark(fn);
        command += "-ln " + Mark(ln);
        command += "-pwd " + Mark(PWD);
        command += "-disabled no";
        
        return command ;
    }

    public static string CommandAddUserInGroup(string CN_Group, string OU_Group, string DC_Group, string CN_User, string OU_User, string DC_User)
    {
        string command = $"dsmod group ";

        string sub = "CN=" + CN_Group;
        string[] OUArray = OU_Process(OU_Group);
        for (int i = OUArray.Length - 1; i >= 0; i--)
        {
            sub += $", OU={OUArray[i]}";
        }

        string[] DCArray = DC_Process(DC_Group);
        for (int i = 0; i < DCArray.Length; i++)
        {
            sub += $", DC={DCArray[i]}";
        }

        command += Mark(sub);
        command += "-addmbr ";

        sub = "CN=" + CN_User;
        OUArray = OU_Process(OU_User);
        for (int i = OUArray.Length - 1; i >= 0; i--)
        {
            sub += $", OU={OUArray[i]}";
        }

        DCArray = DC_Process(DC_User);
        for (int i = 0; i < DCArray.Length; i++)
        {
            sub += $", DC={DCArray[i]}";
        }

        command += Mark(sub);

        return command;
    }

    private static string Mark(string str)
    {
        return "\"" + str + "\" ";    
    }

    public static void RunBat(string path)
    {
        //Process batProc = new Process();
        //batProc.StartInfo.FileName = "powershell.exe";
        //batProc.StartInfo.Arguments = $"-ExecutionPolicy Bypass -Command \"Start-Process '{path}' -NoNewWindow -Wait\"";
        //batProc.StartInfo.UseShellExecute = false;
        //batProc.StartInfo.CreateNoWindow = false;
        //batProc.Start();

        Process batProc = new Process();
        batProc.StartInfo.FileName = path;
        batProc.StartInfo.UseShellExecute = true;
        batProc.StartInfo.CreateNoWindow = false;
        batProc.Start();

        batProc.WaitForExit();

        Validate.Instance.LogOnSystem("Done !!!");
        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_WAIT);
    }

    public static string[] OU_Process(string OU)
    {
        OU = VietnameseProcess.Instance.RemoveSign4VietnameseString(OU);
        string[] result = OU.Split("/");
        if (result.Length <= 0) Validate.Instance.LogOnError("OU is not correct format");
        return result;
    }

    public static string[] DC_Process(string DC)
    {
        DC = VietnameseProcess.Instance.RemoveSign4VietnameseString(DC);
        string[] result = DC.Split(".");
        if (result.Length <= 1) Validate.Instance.LogOnError("DC is not correct format");
        return result;
    }

    public static void WriteFileBat(string str, string part)
    {
        using (FileStream fs = new FileStream(part, FileMode.Create, FileAccess.Write))
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(str + "\n" + "pause");
            sw.Close();
        }
    }
}
