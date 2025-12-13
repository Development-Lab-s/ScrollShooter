using csiimnida.CSILib.SoundManager.RunTime;
using Member.JYG._Code;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour, IUI
{
    [SerializeField] private Image iconImage;
    [SerializeField] private SettingValuesSO settingValuesSO;
    [SerializeField] private Slider slider;
    [SerializeField] private Toggle toggle;

    [field: SerializeField] public GameObject UIObject { get; private set; }
    private TextChangeMove changeText;
    private FilledUp filled;

    public event Action<UIType> OnOpen;
    public event Action<UIType> OnClose;

    public InteractiveType OpenInput => InteractiveType.Middle;

    public UIType UIType => UIType.SettingUI;

    public SettingUIValueSetter SliderValueSetter;
    private AudioSource settingAudio;
    private AudioSource inGameAudio;

    public void Initialize()
    {
        settingAudio = SoundManager.Instance.PlaySound("SettingBGM");
        settingAudio.Stop();

        filled = GetComponentInChildren<FilledUp>(true);
        filled.SetComplete(SceneManager.GetActiveScene().buildIndex is 1 or 2);

        changeText = GetComponentInChildren<TextChangeMove>(true);
        changeText.Initialize();
        SliderValueSetter = new(settingValuesSO.SettingValues, slider, toggle);
        InitializeSetting();
        UIObject.SetActive(false);
    }

    private void Start()
    {
        inGameAudio = GameManager.Instance.InGameAudio;
    }

    public void Open()
    {
        inGameAudio.Pause();
        settingAudio.Play();

        filled.fillTrigger += filled.FillUp;
        TimeManager.Instance.StopTime();
        UIObject.SetActive(true);
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        inGameAudio.UnPause();
        settingAudio.Stop();

        filled.fillTrigger -= filled.FillUp;
        TimeManager.Instance.UnStopTime();
        UIObject.SetActive(false);
        OnClose?.Invoke(UIType);
    }

    public void BackMove()
    {
        if (changeText.IsShaking == true) return;
        SliderValueSetter.ChangeType(-1);
        InitializeSetting();
    }

    public void ForwardMove()
    {
        if (changeText.IsShaking == true) return;
        SliderValueSetter.ChangeType(1);
        InitializeSetting();
    }

    private void InitializeSetting()
    {
        changeText.ChangeText(SliderValueSetter.CurrentValue.Text);
        iconImage.sprite = SliderValueSetter.CurrentValue.Icon;
    }

    public void LeftMove() { }

    public void LeftClick() { }

    public void MiddleMove()
    {
        if (UIObject.activeSelf == true)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void ScrollMove(int value) 
    {
        SliderValueSetter.ChangeSettingValue(-value);
    }

    public void RightClick(bool isPerformed)
    {
       filled.fillTrigger?.Invoke(isPerformed);
    }
}