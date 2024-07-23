using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Dictionary<System.Type, UICanvas> dicUI = new Dictionary<System.Type, UICanvas>();
    public Dictionary<System.Type, UICanvas> dicUIPefab = new Dictionary<System.Type, UICanvas>();
    private UICanvas[] arrCanvas;

    public Transform canvasParent;

    public T OpenUI<T>() where T : UICanvas
    {
        UICanvas canvas = GetUI<T>();

        canvas.Setup();
        canvas.Open();

        return canvas as T;
    }

    public void CloseUI<T>() where T : UICanvas
    {
        if (IsOpening<T>())
        {
            GetUI<T>().Close();
        }
    }

    public void CloseAll()
    {
        foreach (var item in dicUI)
        {
            if (item.Value != null && item.Value.gameObject.activeInHierarchy)
            {
                item.Value.Close();
            }
        }
    }

    public T GetUI<T>() where T : UICanvas
    {
        if (!IsInitialized<T>())
        {
            T uiCanvas = GetUIPrefab<T>();
            Transform parent = canvasParent;
            if (uiCanvas.Parent != null)
            {
                parent = uiCanvas.Parent;
            }

            UICanvas canvas = Instantiate(uiCanvas, parent);
            dicUI[typeof(T)] = canvas;
        }

        return dicUI[typeof(T)] as T;
    }

    private T GetUIPrefab<T>() where T : UICanvas
    {
        if (!dicUIPefab.ContainsKey(typeof(T)))
        {
            if (arrCanvas == null)
            {
                arrCanvas = Resources.LoadAll<UICanvas>("UI/");
            }

            for (int i = 0; i < arrCanvas.Length; i++)
            {
                if (arrCanvas[i] is T)
                {
                    dicUIPefab[typeof(T)] = arrCanvas[i];
                    break;
                }
            }
        }

        return dicUIPefab[typeof(T)] as T;
    }

    public bool IsInitialized<T>() where T : UICanvas
    {
        System.Type type = typeof(T);
        return dicUI.ContainsKey(type) && dicUI[type] != null;
    }

    public bool IsOpening<T>() where T : UICanvas
    {
        return IsInitialized<T>() && dicUI[typeof(T)].gameObject.activeInHierarchy;
    }
}