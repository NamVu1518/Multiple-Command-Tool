using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LogString
{

    public class Log
    {
        public class Error
        {
            public const string ERR_CN001 = "Cannot find CN in CSV";

            public const string ERR_OU001 = "OU is not correct format";
            public const string ERR_OU002 = "OU is not define";
            public const string ERR_OU003 = "Cannot find OU in CSV";

            public const string ERR_DC001 = "DC is not correct format";
            public const string ERR_DC002 = "DC is not define";
            public const string ERR_DC003 = "Cannot find DC in CSV";

            public const string ERR_PATH001 = "Path to CSV file is not define";
            public const string ERR_PATH002 = "Path to save diretory is not define";

            public const string ERR_PWD001 = "Password is not define";
        }
        
        public class System
        {
            public const string SYS_RUNNING = "Running...";
            public const string SYS_DONE = "DONE !!!";
            public const string SYS_WELCOME = "Wellcome";
        }
    }

    private static StringBuilder log = new StringBuilder();

    public static void LogOnError(string message)
    {
        log.Append(BorderText(ColorTextRed("ERROR")) + " " + message + "\n\n");
        UIManager.Instance.GetUI<UILogPanel>().ChangeLog(log.ToString());
        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_ERROR);
    }

    public static void LogOnSystem(string message)
    {
        log.Append(BorderText(ColorTextGreen("SYS")) + " " + message + "\n\n");
        UIManager.Instance.GetUI<UILogPanel>().ChangeLog(log.ToString());
        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_RUN);
    }

    private static string ColorTextRed(string str)
    {
        return "<color=red>" + str + "</color>";
    }

    private static string ColorTextGreen(string str)
    {
        return "<color=green>" + str + "</color>";
    }

    private static string BorderText(string str)
    {
        return "[" + str + "]";
    }

    public static string Mark(string str)
    {
        return "\"" + str + "\" ";
    }
}
