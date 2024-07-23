using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnScope : MonoBehaviour
{
    [SerializeField] private Button scopeBtn;

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    public static extern bool SendMessage(System.IntPtr hWnd, int Msg, int wParam, int lParam);

    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HTCAPTION = 0x2;

    public void OnPointerDown(PointerEventData eventData)
    {
        System.IntPtr hwnd = GetActiveWindow();
        ReleaseCapture();
        SendMessage(hwnd, WM_NCLBUTTONDOWN, HTCAPTION, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

    private System.IntPtr GetActiveWindow()
    {
        return System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
    }
}
