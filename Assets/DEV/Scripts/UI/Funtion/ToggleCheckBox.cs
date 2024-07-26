using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheckBox : MonoBehaviour
{
    [SerializeField] private Image mark;
    [SerializeField] private Button toggle;
    
    public bool interactable = true;

    public bool isOn => mark.IsActive(); 

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
        if (interactable)
        {
            mark.gameObject.SetActive(!mark.IsActive());
        }
    } 

    public void SetStatus(bool status)
    {
        mark.gameObject.SetActive(status);
    }

    public void OnClickButton()
    {
        toggle.onClick.Invoke();
    }
}
