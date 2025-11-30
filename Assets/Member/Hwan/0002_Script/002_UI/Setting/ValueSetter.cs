using System;
using UnityEngine;
using UnityEngine.UI;
// 버튼을 누를 때마다 int값이 바뀌어서 그 값에 따라 배열의 값으로 현재 SO가 달라짐. 현재 SO가 바뀔 때마다 슬라이더를 초기화 해주고, 현재 SO에 따라 값이 올라갈 때 값을 슬라이더와 SettingValueContainer에 값을 적용시키기
public class ValueSetter
{
    private SettingValueSO[] settingValueSOs;
    private int currentValueType = 0;
    public SettingValueSO CurrentValue { get; private set; }
    private Slider slider;
    public ValueSetter(SettingValueSO[] settingValueSOs, Slider slider)
    {
        this.settingValueSOs = settingValueSOs;
        this.slider = slider;
        slider.onValueChanged.AddListener(SaveValue);
        CurrentValue = settingValueSOs[0];
        InitializeSlider();
    }

    public void ChangeType(int value)
    {
        currentValueType += value;
        CurrentValue = settingValueSOs[Mathf.Abs(currentValueType) % settingValueSOs.Length];
        InitializeSlider();
    }

    private void InitializeSlider()
    {
        slider.maxValue = CurrentValue.MaxValue;
        slider.minValue = CurrentValue.MinValue;
        slider.wholeNumbers = true;
        slider.value = SettingValueContainer.Instance.GetSettingValue(CurrentValue.MyType);
    }

    public void ChangeSliderValue(float value)
    {
        slider.value += value;
    }

    public void SaveValue(float changeValue)
    {
        SettingValueContainer.Instance.SetSettingValue(CurrentValue.MyType, changeValue);
    }
}