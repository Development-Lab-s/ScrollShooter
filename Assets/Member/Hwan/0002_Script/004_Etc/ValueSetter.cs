using Member.JYG._Code;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

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
        settingUI = GetComponentInChildren<SettingUI>();
        settingUI.SliderValueSetter.OnValueChange += SetValue;

        foreach (SettingType type in Enum.GetValues(typeof(SettingType)))
        {
            if (type == SettingType.SensitivitySlider) SetValue(type, PlayerPrefs.GetInt(type.ToString(), 250));
            else if (type == SettingType.SkipDeadMotionToggle) SetValue(type, PlayerPrefs.GetInt(type.ToString(), 1));
            else
            {
                SetValue(type, PlayerPrefs.GetInt(type.ToString(), 0));
            }
        }
    }

    private void SetValue(SettingType type, float value)
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
                if (SceneManager.GetActiveScene().buildIndex is 0 or 1 or 2) return;
                player.SetPower(value);
                break;
            case SettingType.SkipDeadMotionToggle:
                if (SceneManager.GetActiveScene().buildIndex is 0 or 1 or 2) return;
                hitSystem.isSecondDead = value == 1;
                break;
        }
    }
}
