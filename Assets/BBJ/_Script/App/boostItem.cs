using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.Events;

public class BoostItem : BlockBase, IUseable
{

    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        SoundManager.Instance.PlaySound("DefaultItem", transform.position.y);
        info.Player.OnDash();
        Used?.Invoke();
        Destroy();
    }
}
