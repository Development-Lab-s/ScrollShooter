using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "SO/Skin")]
public class SkinSO : ScriptableObject
{
    public string skinName;
    public string prefsName;
    public int unlockStage;
    public Sprite skin;
    public float targetTime;
}
