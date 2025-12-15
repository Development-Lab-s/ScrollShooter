using System;
using System.Collections.Generic;
using UnityEngine;


public class SettingValueContainer : YGPacks.Singleton<SettingValueContainer>
{
    //private Dictionary<SettingType, float> settingValueDictionary;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    DontDestroyOnLoad(gameObject);
    //}

    //private void Init()
    //{
    //    settingValueDictionary = new();

    //    foreach (SettingType type in Enum.GetValues(typeof(SettingType)))
    //    {
    //        settingValueDictionary.Add(type, new());
    //        settingValueDictionary[type] = PlayerPrefs.GetFloat(type.ToString(), 0.5f);
    //    }
    //}

    //public float GetSettingValue(SettingType type)
    //{
    //    if (settingValueDictionary == null) Init();
    //    return settingValueDictionary[type].Value;
    //}

    //public void SetSettingValue(SettingValueSO settingValueSO, float value)
    //{
    //    if (settingValueDictionary == null) Init();
    //    settingValueDictionary[settingValueSO.MyType].Value = value;
    //    PlayerPrefs.SetFloat(settingValueSO.MyType.ToString(), value);
    //}
}