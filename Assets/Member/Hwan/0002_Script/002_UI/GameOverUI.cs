using System;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour, IUI
{
    [SerializeField] private RectTransform subObject;
    [SerializeField] private int subOffset;
    [SerializeField] private int subCount;
    private Stack<RectTransform> subObjects = new();

    public GameObject UIObject { get; private set; }

    public UIType UIType => UIType.GameOverUI;

    public event Action<UIType> OnClose;
    public event Action<UIType> OnOpen;

    public void BackMove()
    {
        OnClose?.Invoke(UIType);
    }

    public void Close()
    {
        OnClose?.Invoke(UIType);
    }

    public void FrontMove()
    {
        OnClose?.Invoke(UIType);
    }

    public void Initialize()
    {
        Transform subParent = transform.GetChild(1);

        float middleX = Screen.width / 2;
        float middleY = -Screen.height / 2;
        float startX = -middleX + subObject.sizeDelta.x / 2 + subOffset;
        float startY = middleY - subObject.sizeDelta.y / 2 - subOffset;

        for (int i = 0; i < subCount; i++)
        {
            float t = (float)i / (subCount - 1); // 0 ~ 1
            RectTransform rect = Instantiate(subObject, subParent).GetComponent<RectTransform>();
            float anchorX = Mathf.Lerp(startX, 0, t);
            float anchorY = Mathf.Lerp(startY, 0, t);
            rect.anchoredPosition = new Vector2(anchorX, anchorY);
            subObjects.Push(rect);
        }
    }

    public void LeftMove() { }

    public void MiddleMove() { }

    public void Open()
    {
        OnOpen?.Invoke(UIType);
    }

    public void RightMove() { }

    public void ScrollMove(int value) { }
}
