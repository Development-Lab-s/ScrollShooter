using System;
using System.Collections.Generic;
using UnityEngine;


public class SettingValueContainer : YGPacks.Singleton<SettingValueContainer>
{
    private Dictionary<SettingType, NotifyValue<float>> settingValueDictionary;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        settingValueDictionary = new()
        {
            { SettingType.BGMVolumeSlider, new() },
            { SettingType.SFXVolumeSlider, new() },
            { SettingType.SensitivitySlider, new() },
            { SettingType.MasterVolumeSlider, new() }
        };

        foreach (SettingType type in Enum.GetValues(typeof(SettingType)))
        {
            settingValueDictionary[type].Value = PlayerPrefs.GetFloat(type.ToString(), 0.5f);
            settingValueDictionary[type].OnValueCanged += (_, value) => PlayerPrefs.SetFloat(type.ToString(), value);
        }
    }

    public float GetSettingValue(SettingValueSO settingValueSO)
    {
        if (settingValueDictionary == null) Init();
        return Mathf.Lerp(settingValueSO.MinValue, settingValueSO.MaxValue, settingValueDictionary[settingValueSO.MyType].Value);
    }

    public void SetSettingValue(SettingValueSO settingValueSO, float value)
    {
        if (settingValueDictionary == null) Init();
        settingValueDictionary[settingValueSO.MyType].Value = (value - settingValueSO.MinValue) / (settingValueSO.MaxValue - settingValueSO.MinValue);
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