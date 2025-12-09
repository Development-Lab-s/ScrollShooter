using System;
using UnityEngine;
using UnityEngine.UI;

public class ValueSetter
{
    private SettingValueSO[] settingValueSOs;
    private int currentValueType = 0;
    public SettingValueSO CurrentValue { get; private set; }
    private Slider slider;
    public ValueSetter(SettingValueSO[] settingValueSOs, Slider slider)
    {
        this.settingValueSOs = settingValueSOs;
        this.slider = slider;
        slider.onValueChanged.AddListener(SaveValue);
        CurrentValue = settingValueSOs[0];
        InitializeSlider();
    }

    public void ChangeType(int value)
    {
        currentValueType += value;
        CurrentValue = settingValueSOs[Mathf.Abs(currentValueType) % settingValueSOs.Length];
        InitializeSlider();
    }

    private void InitializeSlider()
    {
        slider.maxValue = CurrentValue.MaxValue;
        slider.minValue = CurrentValue.MinValue;
        slider.wholeNumbers = true;
        slider.value = SettingValueContainer.Instance.GetSettingValue(CurrentValue);
    }

    public void ChangeSliderValue(float value)
    {
        slider.value += value;
    }

    public void SaveValue(float changeValue)
    {
        SettingValueContainer.Instance.SetSettingValue(CurrentValue, changeValue);
    }
}