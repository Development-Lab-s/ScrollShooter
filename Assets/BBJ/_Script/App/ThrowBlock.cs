using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class ThrowBlock : FolderBlock
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float arc = 1;
    public UnityEvent<int, float> AnimationValueChanged;
    public UnityEvent<int, Action> AnimationTriggered;
    [SerializeField] private AnimationHashDataSO moveHash;
    [SerializeField] private AnimationHashDataSO delHash;

    [SerializeField] private OverlapDataSO overlap;
    private bool _isArrival;
    private Coroutine coroutine;

    public void OnDel()
    {
        Debug.Log("delAnima");
        AnimationTriggered?.Invoke(delHash, OnBreak);
    }
    public void StartMove(Vector2 startPos, Vector2 targetPos)
    {
        transform.position = startPos;
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(MoveCoroutine(startPos, targetPos));
    }

    private IEnumerator MoveCoroutine(Vector2 startPos, Vector2 endPos)
    {
        float x0 = startPos.x;
        float x1 = endPos.x;
        float distance = x1 - x0;

        float dir = Mathf.Sign(distance);   // 이동 방향 (+1 또는 -1)
        float currentX = x0;

        while (true)
        {
            currentX += speed * dir * Time.deltaTime;
            float t = (currentX - x0) / distance;

            float baseY = Mathf.Lerp(startPos.y, endPos.y, t);
            float arc = this.arc * (currentX - x0) * (currentX - x1) / (-0.25f * distance * distance);

            Vector2 nextPosition = new Vector2(currentX, baseY + arc);

            transform.position = nextPosition;
            AnimationValueChanged?.Invoke(moveHash, Mathf.Clamp01(t));

            if (t >= 1f && _isArrival == false)
            {
                _isArrival = true;
                if (TryOverlapCircle(overlap, out var collider))
                {
                    foreach (var item in collider)
                    {
                        if (item.transform.TryGetComponent<Recipient>(out var recipient))
                        {
                            recipient.GotIt();
                            Destroy();
                            Debug.Log("destroy");
                            yield break;
                        }
                    }
                }
                Debug.Log("del");
                OnDel();
            }
            yield return null;
        }
    }
    public override void ResetItem()
    {
        base.ResetItem();
        _isArrival = false;
    }

    private bool TryOverlapCircle(OverlapDataSO overlap, out Collider2D[] collider)
    {
        collider = Physics2D.OverlapCircleAll(transform.position, overlap.size, overlap.whatIsTarget);
        return collider.Length != 0;
    }
    protected override void Destroy()
    {
        tween?.Kill();
        StopAllCoroutines();
        PoolManager.Instance.Push(this);
    }
}
