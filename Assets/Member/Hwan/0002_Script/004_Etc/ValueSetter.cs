using Member.JYG._Code;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class ValueSetter : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private HitSystem hitSystem;
    [SerializeField] private float zeroVolume;
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
                if (value == zeroVolume) value = -80;
                audioMixer.SetFloat("BGM", value);
                break;
            case SettingType.SFXVolumeSlider:
                if (value == zeroVolume) value = -80;
                audioMixer.SetFloat("SFX", value);
                break;
            case SettingType.MasterVolumeSlider:
                if (value == zeroVolume) value = -80;
                audioMixer.SetFloat("Master", value);
                break;
            case SettingType.SensitivitySlider:
                if (player == null) return;
                player.SetPower(value);
                break;
            case SettingType.SkipDeadMotionToggle:
                if (hitSystem == null) return;
                hitSystem.isSecondDead = value == 0;
                break;
        }
    }

    private void OnDestroy()
    {
        settingUI.SliderValueSetter.OnValueChange -= SetVolume;
    }
}
