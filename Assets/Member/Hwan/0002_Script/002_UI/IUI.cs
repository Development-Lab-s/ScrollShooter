using UnityEngine;

public interface IUI
{
    public GameObject UIObject { get; }
    public UIType UIType { get; }
    public void Initialize();
    public void Open();
    public void Close();
    public void BackMove();
    public void FrontMove();
    public void LeftButton();
    public void RightButton();
    public void MiddleButton();
}
