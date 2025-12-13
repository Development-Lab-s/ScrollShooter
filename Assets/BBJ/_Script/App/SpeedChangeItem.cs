using UnityEngine;
using UnityEngine.Events;

public class SpeedChangeItem : BlockBase, IUseable
{
    [SerializeField] private int speedValue = 10;
    [SerializeField] private float changeTime = 10f;
    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        info.Player.SetMaxSpeed(speedValue, changeTime);
        Used?.Invoke();
        Destroy();
    }
}
