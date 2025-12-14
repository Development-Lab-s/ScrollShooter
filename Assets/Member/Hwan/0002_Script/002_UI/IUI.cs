using System;
using UnityEngine;

public interface IUI
{
    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;
    public GameObject UIObject { get; }
    public UIType UIType { get; }
    public InteractiveType OpenInput { get; } 

    public void Initialize();

    public void Open();

    public void Close();

    public void BackMove();

    public void ForwardMove();

    public void RightClick(bool isPerformed);

    public void LeftClick();

    public void MiddleMove(bool isPreformed);

    public void ScrollMove(int value);
}
