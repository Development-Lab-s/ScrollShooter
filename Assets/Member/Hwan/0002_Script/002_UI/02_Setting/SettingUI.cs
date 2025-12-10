using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour, IUI
{
    [SerializeField] private Image iconImage;
    [SerializeField] private SettingValuesSO settingValuesSO;
    [SerializeField] private Slider slider;
    [field: SerializeField] public GameObject UIObject { get; private set; }
    private TextChangeMove changeText;

    public event Action<UIType> OnOpen;
    public event Action<UIType> OnClose;

    public InteractiveType OpenInput => InteractiveType.Middle;

    public UIType UIType => UIType.SettingUI;

    public ValueSetter valueSetter;

    public void Initialize()
    {
        changeText = GetComponentInChildren<TextChangeMove>(true);
        changeText.Initialize();
        valueSetter = new(settingValuesSO.SettingValues, slider);
        InitializeSetting();
        UIObject.SetActive(false);
    }

    public void Open()
    {
        TimeManager.Instance.StopTime();
        UIObject.SetActive(true);
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        TimeManager.Instance.UnStopTime();
        UIObject.SetActive(false);
        OnClose?.Invoke(UIType);
    }

    public void BackMove()
    {
        if (changeText.IsShaking == true) return;
        valueSetter.ChangeType(-1);
        InitializeSetting();
    }

    public void ForwardMove()
    {
        if (changeText.IsShaking == true) return;
        valueSetter.ChangeType(1);
        InitializeSetting();
    }

    private void InitializeSetting()
    {
        changeText.ChangeText(valueSetter.CurrentValue.Text);
        iconImage.sprite = valueSetter.CurrentValue.Icon;
    }

    public void LeftMove() { }

    public void RightMove() { }

    public void MiddleMove()
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

    public void ScrollMove(int value) 
    {
        valueSetter.ChangeSliderValue(-value);
    }
}
