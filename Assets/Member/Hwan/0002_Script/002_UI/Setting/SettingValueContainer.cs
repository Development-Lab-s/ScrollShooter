using System;
using System.Collections.Generic;

public class SettingValueContainer : MonoSingleton<SettingValueContainer>
{
    private Dictionary<SettingType, NotifyValue<float>> settingValueDictionary = new();

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