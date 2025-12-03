using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IUI
{
    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform skinContent;
    private List<SkinButton> skinButtons = new();

    private int currentIndex = 0;

    public GameObject UIObject { get; }
    public UIType UIType { get; }

    public void Initialize()
    {
        // 스킨 버튼 수집
        foreach (var btn in skinContent.GetComponentsInChildren<SkinButton>())
        {
            skinButtons.Add(btn);
        }

        Highlight(0);
        ScrollTo(0);

        Open();
    }


    public void Open()
    {
        OnOpen?.Invoke(UIType);
    }

    public void Close()
    {
        
    }

    public void BackMove()
    {
        //스킨 선택
        skinButtons[currentIndex].OnClick();
    }

    public void FrontMove()
    {
        //상점 닫기
        gameObject.SetActive(false);
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
        Debug.Log(value);
        if (value == 0) return;

        int next = Mathf.Clamp(currentIndex + value, 0, skinButtons.Count - 1);

        if (next == currentIndex) return;

        currentIndex = next;

        Highlight(currentIndex);
        ScrollTo(currentIndex);
    }
    
    private void Highlight(int index)
    {
        for (int i = 0; i < skinButtons.Count; i++)
            skinButtons[i].SetHighlight(i == index);
    }

    private void ScrollTo(int index)
    {
        if (skinButtons.Count <= 1) return;

        float normalized = 1f - (float)index / (skinButtons.Count - 1);
        scrollRect.verticalNormalizedPosition = normalized;
    }

}
