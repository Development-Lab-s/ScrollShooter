using DG.Tweening;
using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class GoButtonUI : MonoBehaviour
{
    [SerializeField] private float duration;
    public UnityEngine.Events.UnityAction OnLeftClick;
    public UnityEngine.Events.UnityAction OnRightClick;

    private RectTransform goButton_Left;
    private RectTransform goButton_Right;

    [SerializeField] private float targetYPos;
    private float defaultYPos;

    private void Awake()
    {
        goButton_Left = transform.GetChild(0).GetComponent<RectTransform>();
        goButton_Right = transform.GetChild(1).GetComponent<RectTransform>();

        goButton_Left.GetComponent<Button>().onClick.AddListener(OnLeftClick);
        goButton_Right.GetComponent<Button>().onClick.AddListener(OnRightClick);

        defaultYPos = goButton_Left.anchoredPosition.y;
    }

    public void GoButtonActive(bool value)
    {
        goButton_Left.gameObject.SetActive(value);
        goButton_Right.gameObject.SetActive(value);

        float target = value ? targetYPos : defaultYPos;
        goButton_Left.DOAnchorPosY(target, duration).SetUpdate(true);
        goButton_Right.DOAnchorPosY(target, duration).SetUpdate(true);
    }

    private void OnDestroy()
    {
        goButton_Left.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
