using CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using YGPacks;

public class ShopManager : Singleton<ShopManager>
{
    public Member.PTY.Scripts.SO.SkinListSO skinList;
    
    public GameObject player;
    public int clearedStage;

    private SpriteRenderer _playerSR;

    protected override void Awake()
    {
        _playerSR = player.GetComponentInChildren<SpriteRenderer>();    
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            PlayerPrefs.SetInt("clearedstage", clearedStage);
            for (int i = 0; i < skinList.skinList.Count; i++)
            {
                PlayerPrefs.SetInt(skinList.skinList[i].prefsName, PlayerPrefs.GetInt("clearedstage") >= skinList.skinList[i].unlockStage ? 1 : 0);
            }
        }
    }
    
    public void ChangeSkin(SkinSO skin)
    {
        if(skin == null) Debug.LogWarning("Skin is Null");
        PlayerPrefs.SetString("userskin", skin.prefsName);
        Debug.Log(PlayerPrefs.GetString("userskin"));
        _playerSR.sprite = skin.skin;
    }
    
}
