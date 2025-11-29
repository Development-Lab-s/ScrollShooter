using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextChangeMove : MonoBehaviour
{
    private RectTransform rectTrn;
    private TextMeshProUGUI tmpProUGUI;
    [SerializeField] float duration = 0.18f; // 전체 시간(짧게)
    [SerializeField] float strengthY = 18f;  // 위아래 진폭(픽셀 기준)

    private Sequence seq;
    private bool isShaking;

    private void Awake()
    {
        rectTrn = GetComponent<RectTransform>();
        tmpProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public bool TryChangeText(string text)
    {
        if (isShaking == true) return false;

        tmpProUGUI.text = text;

        ChangeTextMove();
        return true;
    }

    private void ChangeTextMove()
    {
        seq = DOTween.Sequence();
        float tempYValue = rectTrn.anchoredPosition.y;
        isShaking = true;

        seq = DOTween.Sequence();
        seq.Append(rectTrn.DOPunchAnchorPos(new Vector2(0f, strengthY), duration).SetUpdate(true));
        seq.OnComplete(() => isShaking = false);
    }
}