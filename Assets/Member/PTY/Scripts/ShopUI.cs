using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IUI
{
    public int buttonAmount;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonsParent;
    
    [SerializeField] private TextMeshProUGUI previewSkinName;
    
    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Transform skinContent;
    private List<SkinButton> _skinButtons = new();

    private int _currentIndex = 0;

    public GameObject UIObject { get; }
    public UIType UIType => UIType.ShopUI;
    public InteractiveType OpenInput => InteractiveType.None;

    public void Initialize()
    {
        for (int i = 0; i < buttonAmount; i++)
        {
            var button = Instantiate(buttonPrefab, transform);
            button.transform.SetParent(buttonsParent.transform);
            button.name = "skin" + i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "스킨" + i;
            _skinButtons.Add(button.GetComponentInChildren<SkinButton>());
        }

        Highlight(0);
        ScrollTo();

        Open();
    }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
    }

    public void BackMove()
    {
        //상점 닫기
        Close();
    }

    public void ForwardMove()
    {
        //스킨 선택
        _skinButtons[_currentIndex].OnClick();
    }

    public void LeftMove()
    {

    }

    public void RightMove()
    {

    }

    public void MiddleMove()
    {

    }

    public void ScrollMove(int value)
    {
        //스킨 둘러보기
        if (value == 0) return;

        int next = Mathf.Clamp(_currentIndex + value, 0, _skinButtons.Count - 1);

        if (next == _currentIndex) return;

        _currentIndex = next;

        Highlight(_currentIndex);
        ScrollTo();
        
        previewSkinName.text = _skinButtons[_currentIndex].GetComponentInChildren<TextMeshProUGUI>().text;
    }
    
    private void Highlight(int index)
    {
        for (int i = 0; i < _skinButtons.Count; i++)
            _skinButtons[i].SetHighlight(i == index);
    }

    private void ScrollTo()
    {
        if (Mouse.current.scroll.ReadValue().y == -1)
        {
            if (_currentIndex % 3 == 0)
                scrollbar.value = 1 - _currentIndex / 3 * 0.5f;
        }
        else if (Mouse.current.scroll.ReadValue().y == 1)
        {
            if ((_currentIndex + 1) % 3 == 0)
                scrollbar.value = 1 - _currentIndex / 3 * 0.5f;
        }
    }
}
