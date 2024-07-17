using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LifeStatus
{
    ON_WAIT = 0,
    ON_RUN = 1,
    ON_ERROR = 2
}

public class ProgramLifeCycle : Singleton<ProgramLifeCycle>
{
    private LifeStatus _lifeStatus = LifeStatus.ON_WAIT;

    public bool IsWait => _lifeStatus == LifeStatus.ON_WAIT;

    public bool IsError => _lifeStatus == LifeStatus.ON_ERROR;

    public bool IsRun => _lifeStatus == LifeStatus.ON_RUN;

    public void ChangStatus(LifeStatus lifeStatus)
    {
        if (lifeStatus != _lifeStatus) _lifeStatus = lifeStatus;
    } 
}
