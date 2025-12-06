using System;
using UnityEngine;

public interface IUI
{
    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;
    public GameObject UIObject { get; }
    public UIType UIType { get; }
    public InteractiveType OpenInput { get; } 

    public void Initialize(UIController uIController);

    public void Open();

    public void Close();

    public void BackMove();

    public void FrontMove();

    public void LeftMove();

    public void RightMove();

    public void MiddleMove();

    public void ScrollMove(int value);
}
