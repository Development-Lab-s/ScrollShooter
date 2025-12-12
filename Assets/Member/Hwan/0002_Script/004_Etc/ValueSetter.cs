using Member.JYG._Code;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class ValueSetter : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private Player player;
    private SettingUI settingUI;

    private void Start()
    {
        player = GameManager.Instance.Player;
        settingUI = GetComponent<SettingUI>();
        settingUI.SliderValueSetter.OnValueChange += SetVolume;

        foreach (SettingType type in Enum.GetValues(typeof(SettingType)))
        {
            SetVolume(type, SettingValueContainer.Instance.GetSettingValue(type));
        }
    }

    private void SetVolume(SettingType type, float value)
    {
        switch (type)
        {
            case SettingType.BGMVolumeSlider:
                audioMixer.SetFloat("BGM", value);
                break;
            case SettingType.SFXVolumeSlider:
                audioMixer.SetFloat("SFX", value);
                break;
            case SettingType.MasterVolumeSlider:
                audioMixer.SetFloat("Master", value);
                break;
            case SettingType.SensitivitySlider:
                player.SetPower(value);
                break;
            case SettingType.SkipDeadMotionToggle:
                //player.
                break;
        }
    }

    private void OnDestroy()
    {
        settingUI.SliderValueSetter.OnValueChange -= SetVolume;
    }
}
