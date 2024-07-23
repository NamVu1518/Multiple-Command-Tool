using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheckBox : MonoBehaviour
{
    [SerializeField] private Image mark;
    [SerializeField] private Button toggle;
    
    public bool isOn => mark.IsActive(); 

    private void Awake()
    {
        if (mark == null) mark = GetComponentInChildren<Image>();
        if (toggle == null) toggle = GetComponentInChildren<Button>();

        toggle.onClick.AddListener(OnOrOffToggle);
    }

    private void OnOrOffToggle()
    {
        mark.gameObject.SetActive(!mark.IsActive());
    } 
}
