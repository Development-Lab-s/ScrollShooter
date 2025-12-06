using DG.Tweening;
using UnityEditor.VersionControl;
using UnityEngine;

public class Msn : BlockBase, IUseable
{
    [SerializeField]private float invincibleTime = 10f;
    public void Use(UseableInfo info)
    {
        info.Player.OnInvincible(invincibleTime);
        Destroy(gameObject);
    }
}
