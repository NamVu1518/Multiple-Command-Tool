using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnGetFileRun : MonoBehaviour
{
    [SerializeField] private Button btnRun;

    void Start()
    {
        btnRun.onClick.AddListener(MainProgramProcess.Instance.GetFileRunOnly);
    }
}
