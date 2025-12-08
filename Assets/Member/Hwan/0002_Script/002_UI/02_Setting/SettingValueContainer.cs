using System;
using System.Collections.Generic;
using UnityEngine;


public class SettingValueContainer : YGPacks.Singleton<SettingValueContainer>
{
    private Dictionary<SettingType, NotifyValue<float>> settingValueDictionary;

    private void Init()
    {
        settingValueDictionary = new()
        {
            { SettingType.BGMVolumeSlider, new() },
            { SettingType.SFXVolumeSlider, new() },
            { SettingType.SensitivitySlider, new() },
            { SettingType.MasterVolumeSlider, new() }
        };
    }

    public float GetSettingValue(SettingType type)
    {
        if (settingValueDictionary == null) Init();
        return settingValueDictionary[type].Value;
    }

    public void SetSettingValue(SettingType type, float value)
    {
        if (settingValueDictionary == null) Init();
        settingValueDictionary[type].Value = value;
    }

    public void SubSettingValueEvent(SettingType type, Action<float, float> action)
    {
        if (settingValueDictionary == null) Init();
        settingValueDictionary[type].OnValueCanged += action;
    }

    public void UnSubSettingValueEvent(SettingType type, Action<float, float> action)
    {
        if (settingValueDictionary == null) Init();
        settingValueDictionary[type].OnValueCanged -= action;
    }
}