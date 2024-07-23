using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CheckBox : MonoBehaviour
{
    [SerializeField] private ToggleCheckBox[] toggles;
    [SerializeField] private int indexOfDefault = 0, indexToggleNow = 0;

    public ToggleCheckBox[] Toggles => toggles;

    public int IndexToggleNow => indexToggleNow;



    private void SetOffAllToggle()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] != null)
            {
                toggles[i].SetStatus(false);
            }
        }
    }

    private void SetUpDefault()
    {
        if (toggles == null || toggles.Length <= 0)
        {
            toggles = GetComponentsInChildren<ToggleCheckBox>();
        }

        SetOffAllToggle();

        indexToggleNow = indexOfDefault;
        toggles[indexToggleNow].SetStatus(true);

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] != null)
            {
                int index = i;
                toggles[i].Interaction = false;
                toggles[i].Toggle.onClick.AddListener(() => SetOnToggle(index));
            }
        }
    }

    private void SetOnToggle(int index)
    {
        if (index == indexToggleNow
            || index >= toggles.Length 
            || index < 0 
            || toggles[index] == null 
            || toggles[indexToggleNow] == null)
        {
            return;
        }
        toggles[indexToggleNow].SetStatus(false);
        toggles[index].SetStatus(true);
        indexToggleNow = index;
    }

    private void Awake()
    {
        SetUpDefault();
    }
}
