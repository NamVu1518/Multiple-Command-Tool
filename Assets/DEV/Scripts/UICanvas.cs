using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public virtual void Setup()
    {

    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void CloseDelay(float timeDelay)
    {
        Invoke(nameof(Close), timeDelay);
    }
}
