using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnBrowser : MonoBehaviour
{
    [SerializeField] private Button btnBrowser;
    [SerializeField] private TextMeshProUGUI path;

    public void OpenFileWindowForData()
    {
        MainProgramProcess.Instance.OpenFExAndAssignDataPath();
        StartCoroutine(SetTextPath(MainProgramProcess.Instance.PathOfData));
    }

    public void OpenFolderWindowForFileBat()
    {
        MainProgramProcess.Instance.OpenFExAndAssignLocationBatPath();
        StartCoroutine(SetTextPath(MainProgramProcess.Instance.PathOfLocationBat));
    }

    private IEnumerator SetTextPath(string path)
    {
        
        while (string.IsNullOrEmpty(path))
        {
            yield return null;
        }

        this.path.SetText(path);
    }
}
