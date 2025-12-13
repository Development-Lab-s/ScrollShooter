using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIValueSetter
{
    public event Action<SettingType, float> OnValueChange;
    private SettingValueSO[] settingValueSOs;
    private int currentValueType = 0;
    public SettingValueSO CurrentValue { get; private set; }
    private Slider slider;
    private Toggle toggle;

    public SettingUIValueSetter(SettingValueSO[] settingValueSOs, Slider slider, Toggle toggle)
    {
        this.settingValueSOs = settingValueSOs;
        this.slider = slider;
        slider.onValueChanged.AddListener(SaveValue);
        CurrentValue = settingValueSOs[0];

        this.toggle = toggle;
        this.toggle.onValueChanged.AddListener(SaveValue);

        InitializeSetting();
    }

    public void ChangeType(int value)
    {
        currentValueType += value;
        CurrentValue = settingValueSOs[Mathf.Abs(currentValueType) % settingValueSOs.Length];
        InitializeSetting();
    }

    private void InitializeSetting()
    {
        if (CurrentValue.IsToggle == false)
        {
            toggle.gameObject.SetActive(false);

            slider.gameObject.SetActive(true);
            slider.maxValue = CurrentValue.MaxValue;
            slider.minValue = CurrentValue.MinValue;
            slider.wholeNumbers = true;
            slider.value = SettingValueContainer.Instance.GetSettingValue(CurrentValue.MyType);
        }
        else
        {
            slider.gameObject.SetActive(false);

            toggle.gameObject.SetActive(true);
            toggle.isOn = SettingValueContainer.Instance.GetSettingValue(CurrentValue.MyType) == 0 ? false : true;
        }
    }

    public void ChangeSettingValue(float value)
    {
        if (CurrentValue.IsToggle == false)
        {
            slider.value += value * (Mathf.Abs(slider.minValue - slider.maxValue) / 20);
        }
        else
        {
            toggle.isOn = !toggle.isOn;
        }
    }

    private void SaveValue(float changeValue)
    {
        SettingValueContainer.Instance.SetSettingValue(CurrentValue, changeValue);
        OnValueChange?.Invoke(CurrentValue.MyType, SettingValueContainer.Instance.GetSettingValue(CurrentValue.MyType));
    }

    private void SaveValue(bool changeValue)
    {
        SettingValueContainer.Instance.SetSettingValue(CurrentValue, changeValue == true ? 1 : 0);
        OnValueChange?.Invoke(CurrentValue.MyType, SettingValueContainer.Instance.GetSettingValue(CurrentValue.MyType));
    }
}