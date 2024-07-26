using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DefineStatus
{
    UNDEFINE = 0,
    DEFINE_HERE = 1,
    DEFINE_IN_CSV = 2,
    RANDOM = 3
}

public class UICreateUser : UICanvas
{
    [Header("Chang Mode")]
    [SerializeField] private Dropdown dropdown;

    [Header("Check Box")]
    [SerializeField] private CheckBox checkBoxMultipleChoise;
    [SerializeField] private CheckBox checkBoxDC;
    [SerializeField] private CheckBox checkBoxOU;
    [SerializeField] private CheckBox checkBoxPassword;
    [SerializeField] private CheckBox checkBoxGroup;

    [Header("Input Feild")]
    [SerializeField] private TMP_InputField inputDC;
    [SerializeField] private TMP_InputField inputOU;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputGroup;

    public TMP_InputField InputDC
    {
        get { return inputDC; }
    }

    public TMP_InputField InputOU
    {
        get { return inputOU; }
    }

    public TMP_InputField InputPassword
    {
        get { return inputPassword; }
    }

    public TMP_InputField InputGroup
    {
        get { return inputGroup; }
    }

    public void ChangeMode()
    {
        int mode = dropdown.value;
        if (mode < 0 || mode >= Enum.GetValues(typeof(MODE)).Length) return; 
        MainProgramProcess.Instance.ChangeMode((MODE)mode);
    }

    public void ChangeMultipleChoise(int choise)
    {
        if (choise < 0 || choise >= Enum.GetValues(typeof(MultipleChoise)).Length) return;
        MainProgramProcess.Instance.ChangeMode((MODE)choise);
    }

    public DefineStatus WhichStatusFeildDC() 
    {
        if (checkBoxDC == null) return DefineStatus.UNDEFINE;

        if (checkBoxDC.IndexToggleNow == 0)
        {
            return DefineStatus.DEFINE_HERE;
        }
        else if (checkBoxDC.IndexToggleNow == 1)
        {
            return DefineStatus.DEFINE_IN_CSV;
        }

        return DefineStatus.UNDEFINE;
    }

    public DefineStatus WhichStatusFeildOU()
    {
        if (checkBoxOU == null) return DefineStatus.UNDEFINE;

        if (checkBoxPassword.IndexToggleNow == 0)
        {
            return DefineStatus.DEFINE_HERE;
        }
        else if (checkBoxPassword.IndexToggleNow == 1)
        {
            return DefineStatus.DEFINE_IN_CSV;
        }

        return DefineStatus.UNDEFINE;
    }

    public DefineStatus WhichStatusFeildPassword() 
    {
        if (checkBoxPassword == null) return DefineStatus.UNDEFINE;

        if (checkBoxPassword.IndexToggleNow == 0)
        {
            return DefineStatus.DEFINE_HERE;
        }
        else if (checkBoxPassword.IndexToggleNow == 1)
        {
            return DefineStatus.DEFINE_IN_CSV;
        }
        else if (checkBoxPassword.IndexToggleNow == 2)
        {
            return DefineStatus.RANDOM;
        }

        return DefineStatus.UNDEFINE;
    }

    private void Awake()
    {
        checkBoxMultipleChoise.Toggles[checkBoxMultipleChoise.IndexOfDefault].Toggle.onClick.Invoke();
    }
}
