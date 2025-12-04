using DG.Tweening;
using Member.JYG.Input;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GoButtonUI : MonoBehaviour
{
    private RectTransform rectTrn;
    private Button goButton_Left;
    private Button goButton_Right;

    [SerializeField] private float duration;
    [SerializeField] private float upYPos;
    [SerializeField] private float downYPos;

    public void Initialize(Action rigthClick, Action leftClick)
    {
        rectTrn = GetComponent<RectTransform>();
        rectTrn.anchoredPosition = new Vector2(rectTrn.anchoredPosition.x, downYPos);

        goButton_Left = transform.GetChild(0).GetComponent<Button>();
        goButton_Right = transform.GetChild(1).GetComponent<Button>();
        goButton_Left.onClick.AddListener(() => leftClick?.Invoke());
        goButton_Right.onClick.AddListener(() => rigthClick?.Invoke());
        goButton_Left.gameObject.SetActive(true);
        goButton_Right.gameObject.SetActive(true);
    }

    public void ButtonUp()
    {
        float target = upYPos;
        float firstPos = downYPos;

        rectTrn.anchoredPosition = new Vector2(rectTrn.anchoredPosition.x, firstPos);

        if (rectTrn != null)
        {
            rectTrn.DOAnchorPosY(target, duration).SetUpdate(true);
        }
    }

    public void ButtonDown()
    {
        float target = downYPos;
        float firstPos = upYPos;

        rectTrn.anchoredPosition = new Vector2(rectTrn.anchoredPosition.x, firstPos);

        if (rectTrn != null)
        {
            rectTrn.DOAnchorPosY(target, duration).SetUpdate(true);
        }
    }

    private void OnDestroy()
    {
        goButton_Left.onClick?.RemoveAllListeners();
        goButton_Right.onClick?.RemoveAllListeners();
    }
}
