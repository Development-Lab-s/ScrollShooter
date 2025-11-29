using System;
using System.Collections.Generic;
using UnityEngine;


public class SettingValueContainer : YGPacks.Singleton<SettingValueContainer>
{
    private Dictionary<SettingType, NotifyValue<float>> settingValueDictionary = new();

    protected override void Awake()
    {
        base.Awake();

        settingValueDictionary.Add(SettingType.BGMSlider, new());
        settingValueDictionary.Add(SettingType.SFXSlider, new());
        settingValueDictionary.Add(SettingType.SensitivitySlider, new());
    }

    public float GetSettingValue(SettingType type)
    {
        return settingValueDictionary[type].Value;
    }

    public void SetSettingValue(SettingType type, float value)
    {
        settingValueDictionary[type].Value = value;
    }

    public void SubSettingValueEvent(SettingType type, ref Action<float, float> action)
    {
        settingValueDictionary[type].OnValueCanged += action;
    }

    public void UnSubSettingValueEvent(SettingType type, ref Action<float, float> action)
    {
        settingValueDictionary[type].OnValueCanged -= action;
    }
}