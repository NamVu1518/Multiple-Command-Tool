using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Text;
using SFB;
using System;
using Ookii.Dialogs;

public enum MODE
{
    CREATE_USER = 0,
    CREATE_GROUP = 1,
    CREATE_OU = 2
}

public enum MultipleChoise
{
    SINGLE = 0,
    MULTIPLE = 1,
}

public class MainProgramProcess : Singleton<MainProgramProcess>
{
    private MODE mode = MODE.CREATE_USER;

    public MODE Mode => mode;

    private MultipleChoise multipleChoise;
  
    public MultipleChoise MultipleChoise => multipleChoise;

    private CreateUserProcess createUserProcess = new CreateUserProcess();

    public CreateUserProcess CreateUserProcess
    {
        get { return createUserProcess; }
    }

    public void ChangeMode(MODE mode)
    {
        if (this.mode == mode) return;
        this.mode = mode;
    }

    public void ChangeMultipleChoise(MultipleChoise multipleChoise)
    {
        if (this.multipleChoise == multipleChoise) return;
        this.multipleChoise = multipleChoise;
    }

    public void PrcessingAll()
    {
        if (mode == MODE.CREATE_USER)
        {
            createUserProcess.PrcessingRun();
        }
    }

    public void GetFileRunOnly()
    {
        if (mode == MODE.CREATE_USER && multipleChoise == MultipleChoise.MULTIPLE)
        {
            createUserProcess.PrcessingRun(false);
        }
    }

    private void Start()
    {
        LogString.LogSystem(LogString.Log.System.SYS_WELCOME);
    }
}
