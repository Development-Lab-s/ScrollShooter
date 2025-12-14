using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public void SoundPlay(string soundName)
    {
        SoundManager.Instance.PlaySound(soundName);
    }
}
