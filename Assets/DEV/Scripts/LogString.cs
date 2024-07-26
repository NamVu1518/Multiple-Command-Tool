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
            public const string ERR_OU002 = "OU feild is blank";
            public const string ERR_OU003 = "Cannot find OU in CSV";

            public const string ERR_DC001 = "DC is not correct format";
            public const string ERR_DC002 = "DC feild is blank";
            public const string ERR_DC003 = "Cannot find DC in CSV";

            public const string ERR_PATH001 = "Path to CSV file is not define";
            public const string ERR_PATH002 = "Path to save diretory is not define";

            public const string ERR_PWD001 = "Password feild is blank";
            public const string ERR_PWD002 = "Cannot fimd Password in CSV";
        }

        public class Warning
        {
            public const string WARN_FN001 = "No FN feild in CSV, FN and LN would not ba create";
            public const string WARN_LN001 = "No LN feild in CSV, FN and LN would not ba create";
        }

        public class System
        {
            public const string SYS_RUNNING = "Running...";
            public const string SYS_DONE = "DONE !!!";
            public const string SYS_WELCOME = "Wellcome";
        }
    }

    private static StringBuilder log = new StringBuilder();

    public static void LogError(string message)
    {
        log.Append(BorderText(ColorTextRed("ERROR")) + " " + message + "\n\n");
        UIManager.Instance.GetInstanceUI<UILogPanel>().ChangeLog(log.ToString());
        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_ERROR);
    }

    public static void LogWarning(string message) 
    {
        log.Append(BorderText(ColorTextYellow("WARNING")) + " " + message + "\n\n");
        UIManager.Instance.GetInstanceUI<UILogPanel>().ChangeLog(log.ToString());
        ProgramLifeCycle.Instance.ChangStatus(LifeStatus.ON_WARNING);
    }

    public static void LogSystem(string message)
    {
        log.Append(BorderText(ColorTextGreen("SYS")) + " " + message + "\n\n");
        UIManager.Instance.GetInstanceUI<UILogPanel>().ChangeLog(log.ToString());
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

    private static string ColorTextYellow(string str)
    {
        return "<color=yellow>" + str + "</yellow>";
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
