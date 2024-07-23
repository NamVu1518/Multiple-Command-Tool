using System;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOnlyOneToggle : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private int indexOfDefault = 0, indexToggleNow = 0;

    private void SetOffAllToggle()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] != null)
            {
                toggles[i].isOn = false;
            }
        }
    }

    private void SetUpDefault()
    {
        SetOffAllToggle();

        if (toggles[indexOfDefault].isOn == false && toggles[indexOfDefault] != null)
        {
            toggles[indexOfDefault].isOn = true;
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] != null)
            {
                int index = i;
                toggles[i].onValueChanged.AddListener((isOn) => SetOnToggle(isOn, index));
            }
        }
    }

    private void SetOnToggle(bool isOn, int index)
    {
        if (isOn && index != indexToggleNow && toggles[indexToggleNow] != null)
        {
            toggles[indexToggleNow].onValueChanged.RemoveAllListeners();
            toggles[indexToggleNow].isOn = false;
            int temp = indexToggleNow;
            toggles[indexToggleNow].onValueChanged.AddListener((bol) => { SetOnToggle(isOn, temp); });
            indexToggleNow = index;
        }
        else if (index == indexToggleNow && toggles[indexToggleNow] != null)
        {
            toggles[indexToggleNow].onValueChanged.RemoveAllListeners();
            toggles[indexToggleNow].isOn = true;
            int temp = indexToggleNow;
            toggles[indexToggleNow].onValueChanged.AddListener((bol) => { SetOnToggle(isOn, temp); });
        }
    }

    private void Awake()
    {
        SetUpDefault();
    }
}
