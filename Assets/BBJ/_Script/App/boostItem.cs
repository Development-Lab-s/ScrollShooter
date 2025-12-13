using UnityEngine;
using UnityEngine.Events;

public class BoostItem : BlockBase, IUseable
{

    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        info.Player.OnDash();
        Used?.Invoke();
        Destroy();
    }
}
