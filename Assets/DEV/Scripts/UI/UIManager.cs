using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private ToggDefine toggleOU;
    [SerializeField] private ToggDefine toggleGroup;
    [SerializeField] private ToggDefine togglePassword;

    [SerializeField] private TMP_InputField inputDC;

    [SerializeField] private LogPanel logPanel;

    public bool IsOnToggOU
    {
        get => toggleOU.Toggle.isOn; 
        set => toggleOU.Toggle.isOn = value;
    }

    public bool IsOnToggGRP
    {
        get => toggleGroup.Toggle.isOn;
        set => toggleGroup.Toggle.isOn = value;
    }

    public bool IsOnToggPWD
    {
        get => togglePassword.Toggle.isOn;
        set => togglePassword.Toggle.isOn = value;
    }

    public string InputOU
    {
        get => toggleOU.InputField.text;
        set => toggleOU.InputField.text = value;
    }

    public string InputGRP
    {
        get => toggleGroup.InputField.text;
        set => toggleGroup.InputField.text = value;
    }

    public string InputPWD
    {
        get => togglePassword.InputField.text;
        set => togglePassword.InputField.text = value;
    }

    public string InputDC
    {
        get => inputDC.text;
        set => inputDC.text = value;
    }

    public void ChangeLog(StringBuilder stringBuilder)
    {
        logPanel.ChangeLog(stringBuilder.ToString());
        logPanel.ResetScroolBar();
    }
}
