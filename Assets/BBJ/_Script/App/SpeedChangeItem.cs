using UnityEngine;
using UnityEngine.Events;
using CSILib;
using csiimnida.CSILib.SoundManager.RunTime;

public class SpeedChangeItem : BlockBase, IUseable
{
    [SerializeField] private int speedValue = 10;
    [SerializeField] private float changeTime = 10f;
    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        SoundManager.Instance.PlaySound("DefaultItem", transform.position.y);
        info.Player.SetMaxSpeed(speedValue, changeTime);
        Used?.Invoke();
        Destroy();
    }
}
