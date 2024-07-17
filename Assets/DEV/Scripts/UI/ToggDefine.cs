using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggDefine : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_InputField inputField;

    public Toggle Toggle { get { return toggle; } }
    public TMP_InputField InputField { get { return inputField; } }

    public void InteracableInputField()
    {
        inputField.interactable = toggle.isOn;
    }
}
