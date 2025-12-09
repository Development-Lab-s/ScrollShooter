using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using YGPacks.PoolManager;

public class HelpPopupUI : PopupWindowBase
{
    private void Awake()
    {
        RectCompo = GetComponent<RectTransform>();
    }
    public override void OnPopup()
    {
        transform.DOScale(2f, 0.2f)
            .From(1f)
            .SetEase(Ease.InBounce);
        StartCoroutine(PopupCoroutine(5f));
    }
    private IEnumerator PopupCoroutine(float a)
    {
        yield return new WaitForSeconds(a);
        Destroyed?.Invoke(this);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
