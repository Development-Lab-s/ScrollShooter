using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUp : MonoBehaviour
{
    [SerializeField] private GameObject buttonUp;
    [SerializeField] private Slider slider;
    [SerializeField] private float targetY = 0;
    [SerializeField] private TitlePAni pAni;
    [SerializeField] private DescChange desc;
    private bool isBool;
    private bool IsBool
    {
        get => isBool; set
        {
            if (isBool != value)
            {
                isBool = value;
                moveTween?.Kill();
                moveTween = isBool ?
                    rectTransform.DOAnchorPos(new Vector2(0, targetY), 0.3f).SetEase(Ease.OutBack):
                    moveTween = rectTransform.DOAnchorPos(Vector2.zero, 0.3f).SetEase(Ease.InQuad);
            }
        }
    }

    private RectTransform rectTransform;
    private Tween moveTween;

    private void Awake()
    {
        pAni.GetComponent<TitlePAni>();
        desc.GetComponent<DescChange>();
        rectTransform = buttonUp.GetComponent<RectTransform>();
    }

    private void Start()
    {
        desc.OnChange += desc.ChangeText;
        pAni.isTrigger += pAni.ConverSion;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void Up(float value)
    {
        if (rectTransform == null) return;


        IsBool = (value >= 1f);
        if (isBool)
        {
            Debug.Log("aa");
            desc.ChangeText(true);
            pAni?.isTrigger.Invoke(true);
        }
        if (!isBool)
        {
            Debug.Log("DD");
            desc.ChangeText(false);
            pAni?.isTrigger.Invoke(false);
        }
        Debug.Log(isBool);
    }
}

