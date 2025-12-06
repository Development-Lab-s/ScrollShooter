using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoButtonIconChanger : MonoBehaviour
{
    [SerializeField] private IconForUI[] iconForUIs;
    private Dictionary<UIType, GoButtonIcon> iconDictionary = new();

    [SerializeField] private Image RightImage;
    [SerializeField] private Image LeftImage;
    [SerializeField] private TextMeshProUGUI RightText;
    [SerializeField] private TextMeshProUGUI LeftText;

    public void Initialize()
    {
        foreach (IconForUI iconForUI in iconForUIs)
        {
            iconDictionary.Add(iconForUI.UIType, iconForUI.Icon);
        }
    }

    public void ChangeIcon(UIType uiType)
    {
        RightText.text = "";
        LeftText.text = "";
        RightImage.enabled = false;
        LeftImage.enabled = false;

        switch (iconDictionary[uiType].iconType)
        {
            case GoButtonIconType.Sprite:
                RightImage.enabled = true;
                LeftImage.enabled = true;
                RightImage.sprite = iconDictionary[uiType].RigthSprite;
                LeftImage.sprite = iconDictionary[uiType].LeftSprite;
                break;
            case GoButtonIconType.Text:
                RightText.text = iconDictionary[uiType].RigthText;
                LeftText.text = iconDictionary[uiType].LeftText;
                break;
        }
    }
}

[Serializable]
public struct IconForUI
{
    [field: SerializeField] public UIType UIType { get; private set; }
    [field: SerializeField] public GoButtonIcon Icon { get; private set; }
}

[Serializable]
public struct GoButtonIcon
{
    [field: SerializeField] public GoButtonIconType iconType { get; private set; }

    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite leftSprite;
    public Sprite RigthSprite => rightSprite;
    public Sprite LeftSprite => leftSprite;

    [SerializeField] private string rightText;
    [SerializeField] private string leftText;
    public string RigthText => rightText;
    public string LeftText => leftText;
}