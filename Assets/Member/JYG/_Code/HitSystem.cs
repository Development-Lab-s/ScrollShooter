using System;
using UnityEngine;
using UnityEngine.Events;

public class HitSystem : MonoBehaviour
{
    public UnityEvent onDead;
    public UnityEvent onHit;

    [SerializeField] private int maxLife;

    public int Life
    {
        get => Life;
        set
        {
            if (value + Life >= maxLife)
            {
                Life = maxLife;
            }
            else if(value + Life <= 0)
            {
                Life = 0;
            }
            else
            {
                Life += value;
            }

            if (value < 0)
            {
                JudgeIsDead();
            }
        }
    }

    private void JudgeIsDead()
    {
        if (Life == 0)
        {
            onDead?.Invoke();
            return;
        }
        onHit?.Invoke();
    }
    
}
