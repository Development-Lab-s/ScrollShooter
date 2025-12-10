using CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.InputSystem;
using YGPacks;

public class ShopManager : Singleton<ShopManager>
{
    public Member.PTY.Scripts.SO.SkinListSO skinList;
    
    public GameObject player;
    public int stageCleared;

    private SpriteRenderer _playerSR;

    protected override void Awake()
    {
        _playerSR = player.GetComponentInChildren<SpriteRenderer>();    
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            for (int i = 0; i < skinList.skinList.Count; i++)
            {
                PlayerPrefs.SetInt(skinList.skinList[i].prefsName, stageCleared >= skinList.skinList[i].unlockStage ? 1 : 0);
            }
        }
    }
    
    public void ChangeSkin(SkinSO skin)
    {
        _playerSR.sprite = skin.skin;
    }
    
}
