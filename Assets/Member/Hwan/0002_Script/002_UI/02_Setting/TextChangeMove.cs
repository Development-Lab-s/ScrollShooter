using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class TextChangeMove : MonoBehaviour
{
    private RectTransform rectTrn;
    private TextMeshProUGUI tmpProUGUI;
    [SerializeField] float duration = 0.18f; // 전체 시간(짧게)
    [SerializeField] float strengthY = 18f;  // 위아래 진폭(픽셀 기준)
    public bool IsShaking { get; private set; }

    public void Initialize()
    {
        rectTrn = GetComponent<RectTransform>();
        tmpProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string text)
    {
        tmpProUGUI.text = text;

        ChangeTextMove();
    }

    private void ChangeTextMove()
    {
        IsShaking = true;
        float tempYValue = rectTrn.anchoredPosition.y;

        rectTrn.DOPunchAnchorPos(new Vector2(0f, strengthY), duration).SetUpdate(true).OnComplete(() => IsShaking = false);
    }
}