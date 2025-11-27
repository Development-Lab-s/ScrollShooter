using System;
using UnityEngine;

public class SettingUI : MonoBehaviour, IUI
{
    [field: SerializeField] public GameObject UIObject { get; private set; }
    private TextChangeMove changeText;

    public UIType UIType => UIType.SettingUI;
    public void Initialize()
    {
        UIObject.GetComponentInChildren<SliderUI>().InitializeSlider();
        changeText = GetComponentInChildren<TextChangeMove>();
        Close();
    }
    public void Open()
    {
        UIObject.SetActive(true);
    }
    public void Close()
    {
        UIObject.SetActive(false);
    }

    public void BackMove()
    {
        changeText.TryChangeText("감도");
    }

    public void FrontMove()
    {
        changeText.TryChangeText("사운드");
    }

    public void LeftButton() { }

    public void RightButton() { }

    public void MiddleButton()
    {
        if (UIObject.activeSelf == true)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
