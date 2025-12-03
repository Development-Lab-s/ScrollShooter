using System;
using System.Collections;
using UnityEngine;

public class GameOverUI : MonoBehaviour, IUI
{
    public GameObject UIObject { get; private set; }

    public UIType UIType => UIType.GameOverUI;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    private NestingOpener nestingOpener;

    public void BackMove()
    {
        Close();
    }

    public void Close()
    {
        Time.timeScale = 1;
        OnClose?.Invoke(UIType);
    }

    public void FrontMove()
    {
        Close();
    }

    public void Initialize()
    {
        nestingOpener = GetComponent<NestingOpener>();
        nestingOpener.Initialize();
    }

    public void LeftMove() { }

    public void MiddleMove() { }

    public void Open()
    {
        Time.timeScale = 0;
        nestingOpener.StartNesting(UIObject);
        OnOpen?.Invoke(UIType);
    }

    public void RightMove() { }

    public void ScrollMove(int value) { }
}
