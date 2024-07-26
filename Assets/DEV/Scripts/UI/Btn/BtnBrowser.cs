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
        MainProgramProcess.Instance.CreateUserProcess.OpenFExAndAssignDataPath();
        StartCoroutine(SetTextPath(MainProgramProcess.Instance.CreateUserProcess.PathOfData));
    }

    public void OpenFolderWindowForFileBat()
    {
        MainProgramProcess.Instance.CreateUserProcess.OpenFExAndAssignLocationRunFilePath();
        StartCoroutine(SetTextPath(MainProgramProcess.Instance.CreateUserProcess.PathOfDirectoryFileRun));
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
