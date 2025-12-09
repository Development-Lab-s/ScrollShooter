using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    public void OnDel()
    {
        AnimationTriggered?.Invoke(delHash, OnBreak);
    }
    public void StartMove(Vector2 startPos, Vector2 targetPos)
    {
        transform.position = startPos;
        StartCoroutine(MoveCoroutine(startPos, targetPos));
    }

    private IEnumerator MoveCoroutine(Vector2 startPos, Vector2 endPos)
    {
        float x0 = startPos.x;
        float x1 = endPos.x;
        float distance = x1 - x0;

        float currentX = startPos.x;

        while (true)
        {
            currentX += speed * Time.deltaTime;
            float nextX = currentX;

            float baseY = Mathf.Lerp(startPos.y, endPos.y, (nextX - x0) / distance);
            float arc = this.arc * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);

            Vector2 nextPosition = new Vector2(nextX, baseY + arc);

            transform.position = nextPosition;
            AnimationValueChanged?.Invoke(moveHash, Mathf.Clamp01((nextX - x0) / distance));

            if (nextX >= x1 && _isArrival == false)
            {
                _isArrival = true;
                if (CheckForPlayer(overlap))
                    yield break;
                else
                    OnDel();
            }
            yield return null;
        }
    }

    private bool CheckForPlayer(OverlapDataSO overlap)
    {
        var collider = Physics2D.OverlapCircle(transform.position, overlap.size, overlap.whatIsTarget);
        if (collider != null&& collider.transform.TryGetComponent<Recipient>(out var recipient))
        {
            recipient.GotIt();
            Destroy();
            return true;
        }
        Debug.Log("못찾음");
        return false;
        
        // 타겟이 있는지 확인 후 
    }
}
