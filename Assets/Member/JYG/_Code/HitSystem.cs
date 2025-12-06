using UnityEngine;
using UnityEngine.Events;

public class HitSystem : MonoBehaviour
{
    public UnityEvent onDead;
    public UnityEvent onHit;

    [SerializeField] private int maxLife;

    private int _life;
    public int Life
    {
        get => _life;
        set
        {
            if (value > maxLife)
            {
                _life = maxLife;
            }
            else if(value <= 0)
            {
                _life = 0;
                onDead?.Invoke();
            }
            else
            {
                _life = value;
            }

            onHit?.Invoke();
        }
    }

    private void Awake()
    {
        _life = maxLife;
    }
}
