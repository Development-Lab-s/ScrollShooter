using DG.Tweening;
using Member.JYG.Input;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GoButtonUI : MonoBehaviour
{
    [SerializeField] private float duration;

    private RectTransform goButton_Left;
    private RectTransform goButton_Right;

    [SerializeField] private float upYPos;
    [SerializeField] private float downYPos;

    private PlayerInputSO inputSO;

    public void Initialize(Action rigthClick, Action leftClick)
    {
        goButton_Left = transform.GetChild(0).GetComponent<RectTransform>();
        goButton_Right = transform.GetChild(1).GetComponent<RectTransform>();

        goButton_Left.anchoredPosition = new Vector2(goButton_Left.anchoredPosition.x, downYPos);
        goButton_Right.anchoredPosition = new Vector2(goButton_Right.anchoredPosition.x, downYPos);

        goButton_Right.GetComponent<Button>().onClick.AddListener(() => rigthClick?.Invoke());
        goButton_Left.GetComponent<Button>().onClick.AddListener(() => leftClick?.Invoke());
    }

    public void ButtonUp()
    {
        goButton_Left.gameObject.SetActive(true);
        goButton_Right.gameObject.SetActive(true);

        float target = upYPos;
        float firstPos = downYPos;

        goButton_Left.anchoredPosition = new Vector2(goButton_Left.anchoredPosition.x, firstPos);
        goButton_Right.anchoredPosition = new Vector2(goButton_Right.anchoredPosition.x, firstPos);

        goButton_Left.DOAnchorPosY(target, duration).SetUpdate(true);
        goButton_Right.DOAnchorPosY(target, duration).SetUpdate(true);
    }

    public void ButtonDown()
    {
        Sequence sequence = DOTween.Sequence();
        float target = downYPos;
        float firstPos = upYPos;

        goButton_Left.anchoredPosition = new Vector2(goButton_Left.anchoredPosition.x, firstPos);
        goButton_Right.anchoredPosition = new Vector2(goButton_Right.anchoredPosition.x, firstPos);

        sequence.Append(goButton_Left.DOAnchorPosY(target, duration).SetUpdate(true));
        sequence.Join(goButton_Right.DOAnchorPosY(target, duration).SetUpdate(true));
        sequence.OnComplete(() => {
            goButton_Left.gameObject.SetActive(true);
            goButton_Right.gameObject.SetActive(true);
        });
    }

    private void OnDestroy()
    {
        goButton_Left.GetComponent<Button>().onClick?.RemoveAllListeners();
        goButton_Right.GetComponent<Button>().onClick?.RemoveAllListeners();
    }
}
