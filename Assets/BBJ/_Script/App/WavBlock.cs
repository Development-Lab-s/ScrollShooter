using Member.JYG._Code;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WavBlock : BlockBase, IUseable
{
    [SerializeField] string soundName;

    private string[] eggSounds = { "WOWEgg", "BabyEgg", "ClassicEgg" };
    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        Used?.Invoke();
        GameManager.Instance.ChangeBGM(eggSounds[Random.Range(0, eggSounds.Length)]);
        Destroy(gameObject);
    }
}
