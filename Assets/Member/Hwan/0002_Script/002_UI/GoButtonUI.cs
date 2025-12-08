using DG.Tweening;
using Member.JYG.Input;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GoButtonUI : MonoBehaviour
{
    private GoButtonIconChanger iconChanger;

    private RectTransform rectTrn;
    private Button goButton_Left;
    private Button goButton_Right;

    [SerializeField] private float duration;
    [SerializeField] private float upYPos;
    [SerializeField] private float downYPos;

    private bool isUp;

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

        iconChanger = GetComponent<GoButtonIconChanger>();
        iconChanger.Initialize();
    }

    public void ButtonMove(UIType uiType, bool isUp)
    {
        if (isUp == true) iconChanger.ChangeIcon(uiType);
        if (this.isUp == isUp) return;

        this.isUp = isUp;
        float target = isUp ? upYPos : downYPos;
        float firstPos = !isUp ? upYPos : downYPos;

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
