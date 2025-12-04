using System;
using UnityEngine;

public class StageSelectUI : MonoBehaviour, IUI
{
    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;
    public GameObject UIObject { get; }
    public UIType UIType { get; }
    public void Initialize()
    {
        Open();
    }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
    }

    public void BackMove()
    {
        Close();
    }

    public void FrontMove()
    {
        
    }

    public void LeftMove()
    {
    }

    public void RightMove()
    {
    }

    public void MiddleMove()
    {
    }

    public void ScrollMove(int value)
    {
        
    }
}
