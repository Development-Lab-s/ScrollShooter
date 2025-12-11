using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour, IUI
{
    [SerializeField] private Image iconImage;
    [SerializeField] private SettingValuesSO settingValuesSO;
    [SerializeField] private Slider slider;
    [field: SerializeField] public GameObject UIObject { get; private set; }
    private TextChangeMove changeText;
    private FilledUp filled;

    public event Action<UIType> OnOpen;
    public event Action<UIType> OnClose;

    public InteractiveType OpenInput => InteractiveType.Middle;

    public UIType UIType => UIType.SettingUI;

    public ValueSetter valueSetter;
    private Coroutine fills;

    public void Initialize()
    {
        filled = GetComponentInChildren<FilledUp>();
        changeText = GetComponentInChildren<TextChangeMove>(true);
        changeText.Initialize();
        valueSetter = new(settingValuesSO.SettingValues, slider);
        InitializeSetting();
        UIObject.SetActive(false);
    }

    public void Open()
    {
        filled.fillTrigger += filled.FillUp;
        TimeManager.Instance.StopTime();
        UIObject.SetActive(true);
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        filled.fillTrigger -= filled.FillUp;
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

    public void LeftMove(bool isPerformed)
    {
       filled.fillTrigger?.Invoke(isPerformed);        
       Debug.Log(isPerformed);
    }
    
}
