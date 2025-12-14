using csiimnida.CSILib.SoundManager.RunTime;
using Member.JYG._Code;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WavBlock : BlockBase, IUseable
{
    [SerializeField] string soundName;
    public UnityEvent Used;
    private string[] eggSounds = { "WOWEgg", "BabyEgg", "ClassicEgg" };

    public void Use(UseableInfo info)
    {
        Used?.Invoke();
        SoundManager.Instance.PlaySound("DefaultItem", transform.position.y);
        GameManager.Instance.ChangeBGM(eggSounds[Random.Range(0, eggSounds.Length)]);

        Destroy(gameObject);
    }
}
