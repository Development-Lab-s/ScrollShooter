using UnityEngine;
using UnityEngine.Events;

public class HitSystem : MonoBehaviour
{
    public UnityEvent onDead;
    public UnityEvent onSecondDead;
    public UnityEvent onHit;

    [SerializeField] private int maxLife;
    public bool isSecondDead = false;
    private bool isDead;

    private int _life;
    public int Life
    {
        get => _life;
        set
        {
            if (isDead == true) return;

            if (value > maxLife)
            {
                _life = maxLife;
            }
            else if(value <= 0)
            {
                _life = 0;
                isDead = true;
                InputControlManager.Instance.ChangeUIInputActive(false);

                if (isSecondDead && PlayerPrefs.GetInt("IsFirst", 1) == 0)
                {
                    onSecondDead?.Invoke();
                }
                else
                {
                    onDead?.Invoke();
                }
                return;
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
