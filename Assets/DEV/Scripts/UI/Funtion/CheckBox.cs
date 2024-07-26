using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CheckBox : MonoBehaviour
{
    [SerializeField] private ToggleCheckBox[] toggles;
    [SerializeField] private int indexOfDefault = 0, indexToggleNow = 0;

    public ToggleCheckBox[] Toggles => toggles;

    public int IndexToggleNow => indexToggleNow;

    public int IndexOfDefault => indexOfDefault;

    private void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        for (int i = 0; i < Toggles.Length; i++)
        {
            Toggles[i].SetStatus(false);
        }

        toggles[indexOfDefault].SetStatus(true);

        indexToggleNow = indexOfDefault;
    }

    public void SetOn(int index)
    {
        if (index == indexToggleNow) return;
        toggles[index].SetStatus(true);
        toggles[indexToggleNow].SetStatus(false);
        indexToggleNow = index;
    }
}
