using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour, IUI
{
    [SerializeField] private SettingValuesSO settingValuesSO;
    [SerializeField] private Slider slider;
    [field: SerializeField] public GameObject UIObject { get; private set; }
    private TextChangeMove changeText;

    public event Action<UIType> OnOpen;
    public event Action<UIType> OnClose;

    public UIType UIType => UIType.SettingUI;

    public ValueSetter valueSetter;

    public void Initialize()
    {
        UIObject.GetComponentInChildren<SliderUI>().InitializeSlider();
        changeText = GetComponentInChildren<TextChangeMove>();
        valueSetter = new(settingValuesSO.SettingValues, slider);
        Close();
    }

    public void Open()
    {
        UIObject.SetActive(true);
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        UIObject.SetActive(false);
        OnClose?.Invoke(UIType);
    }

    public void BackMove()
    {
        valueSetter.ChangeSliderValue(-1);
    }

    public void FrontMove()
    {
        valueSetter.ChangeSliderValue(1);
    }

    public void LeftMove() { }

    public void RightMove() { }

    public void MiddleMove()
    {
        if (UIObject.activeSelf == true)
        {
            Debug.Log("sdfsfs");
            Close();
        }
        else
        {
            Debug.Log("sdfsfs");
            Open();
        }
    }

    public void ScrollMove(int value) { }
}
