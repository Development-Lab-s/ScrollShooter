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

    private bool isLengthSetting = false;

    public SettingUIValueSetter(SettingValueSO[] settingValueSOs, Slider slider, Toggle toggle)
    {
        this.settingValueSOs = settingValueSOs;
        this.slider = slider;
        slider.onValueChanged.AddListener((value) =>
        {
            if (isLengthSetting == true) return;
            SaveValue(Mathf.RoundToInt(value));
        });
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

            isLengthSetting = true;
            slider.maxValue = CurrentValue.MaxValue;
            slider.minValue = CurrentValue.MinValue;
            isLengthSetting = false;
            if (CurrentValue.MyType == SettingType.SensitivitySlider) slider.value = PlayerPrefs.GetInt(CurrentValue.MyType.ToString(), 250);
            else
            {
                slider.value = PlayerPrefs.GetInt(CurrentValue.MyType.ToString(), 0);
            }
        }
        else
        {
            slider.gameObject.SetActive(false);

            toggle.gameObject.SetActive(true);
            toggle.isOn = PlayerPrefs.GetInt(CurrentValue.MyType.ToString(), 0) == 0 ? false : true;
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

    private void SaveValue(int changeValue)
    {
        PlayerPrefs.SetInt(CurrentValue.MyType.ToString(), changeValue);
        PlayerPrefs.Save();
        OnValueChange?.Invoke(CurrentValue.MyType, changeValue);
    }

    private void SaveValue(bool changeValue)
    {
        PlayerPrefs.SetInt(CurrentValue.MyType.ToString(), changeValue == true ? 1 : 0);
        PlayerPrefs.Save();
        OnValueChange?.Invoke(CurrentValue.MyType, changeValue ? 1 : 0);
    }
}