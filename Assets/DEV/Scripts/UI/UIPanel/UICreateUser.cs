using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public enum DefineStatus
{
    UNDEFINE = 0,
    DEFINE_HERE = 1,
    DEFINE_IN_CSV = 2,
    RANDOM = 3
}

public class UICreateUser : UICanvas
{
    [Header("Check Box")]
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

    public void SetinteractableInputFeild(TMP_InputField inputField, bool interactable)
    {
        inputField.interactable = interactable;
    }

    public void SetUp()
    {
        if (checkBoxDC != null && checkBoxDC.Toggles != null)
        {
            if (checkBoxDC.Toggles[0] != null)
            {
                checkBoxDC.Toggles[0].Toggle.onClick.AddListener(() => { SetinteractableInputFeild(inputDC, true); });
            }
            if (checkBoxDC.Toggles[1] != null)
            {
                checkBoxDC.Toggles[1].Toggle.onClick.AddListener(() => { SetinteractableInputFeild(inputDC, false); });
            }
        }

        if (checkBoxOU != null && checkBoxOU.Toggles != null)
        {
            if (checkBoxOU.Toggles[0] != null)
            {
                checkBoxOU.Toggles[0].Toggle.onClick.AddListener(() => { SetinteractableInputFeild(inputOU, true); });
            }
            if (checkBoxOU.Toggles[1] != null)
            {
                checkBoxOU.Toggles[1].Toggle.onClick.AddListener(() => { SetinteractableInputFeild(inputOU, false); });
            }
        }

        if (checkBoxPassword != null && checkBoxPassword.Toggles != null)
        {
            if (checkBoxPassword.Toggles[0] != null)
            {
                checkBoxPassword.Toggles[0].Toggle.onClick.AddListener(() => { SetinteractableInputFeild(inputPassword, true); });
            }
            if (checkBoxPassword.Toggles[1] != null)
            {
                checkBoxPassword.Toggles[1].Toggle.onClick.AddListener(() => { SetinteractableInputFeild(inputPassword, false); });
            }
            if (checkBoxPassword.Toggles[2] != null)
            {
                checkBoxPassword.Toggles[2].Toggle.onClick.AddListener(() => { SetinteractableInputFeild(inputPassword, false); });
            }
        }
    }

    private void Awake()
    {
        SetUp();
    }
}
