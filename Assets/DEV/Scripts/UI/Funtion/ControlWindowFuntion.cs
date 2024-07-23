using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ControlWindowFuntion : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern int ShowWindow(System.IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool CloseWindow(System.IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool DestroyWindow(System.IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();

    private const int SW_MINIMIZE = 6;
    private const int SW_RESTORE = 9;

    public void Minimize()
    {
        System.IntPtr hwnd = GetActiveWindow();
        ShowWindow(hwnd, SW_MINIMIZE);
    }

    public void Restore()
    {
        System.IntPtr hwnd = GetActiveWindow();
        ShowWindow(hwnd, SW_RESTORE);
    }

    public void Close()
    {
        System.IntPtr hwnd = GetActiveWindow();
        DestroyWindow(hwnd);
    }
}
