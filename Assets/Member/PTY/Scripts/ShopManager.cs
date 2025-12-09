using CSILib.SoundManager.RunTime;
using UnityEngine;

public class ShopManager : MonoSingleton<ShopManager>
{
    public GameObject player;

    private SpriteRenderer _playerSR;

    private void Awake()
    {
        _playerSR = player.GetComponentInChildren<SpriteRenderer>();
    }
    
    public void ChangeSkin(SkinSO skin)
    {
        _playerSR.sprite = skin.skin;
    }
    
}
