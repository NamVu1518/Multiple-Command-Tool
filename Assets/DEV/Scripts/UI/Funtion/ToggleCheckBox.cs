using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheckBox : MonoBehaviour
{
    [SerializeField] private bool interaction = true;
    [SerializeField] private Image mark;
    [SerializeField] private Button toggle;
    
    public bool isOn => mark.IsActive(); 

    public bool Interaction
    {
        get { return interaction; }
        set { interaction = value; }
    }

    public Button Toggle
    {
        get { return toggle; }
        set { toggle = value; }
    }

    private void Awake()
    {
        if (mark == null) mark = GetComponentInChildren<Image>();
        if (toggle == null) toggle = GetComponentInChildren<Button>();

        toggle.onClick.AddListener(OnOrOffToggle);
    }

    public void OnOrOffToggle()
    {
        if (interaction)
        {
            mark.gameObject.SetActive(!mark.IsActive());
        }
    } 

    public void SetStatus(bool status)
    {
        mark.gameObject.SetActive(status);
    }
}
