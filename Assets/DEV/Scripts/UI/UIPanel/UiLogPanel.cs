using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UILogPanel : UICanvas
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private Scrollbar scrollbar;

    public void ChangeLog(string str)
    {
        logText.text = str;
    }

    public void ResetScroolBar()
    {
        scrollbar.value = 0;
    }
}
