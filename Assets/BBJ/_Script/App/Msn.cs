using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.Events;

public class Msn : BlockBase,IUseable
{
    [SerializeField]private float invincibleTime = 10f;
    public UnityEvent Used;

    public void Use(UseableInfo info)
    {
        SoundManager.Instance.PlaySound("Fly", transform.position.y);
        info.Player.OnInvincible(invincibleTime);
        Used?.Invoke();
        Destroy();
    }
}
