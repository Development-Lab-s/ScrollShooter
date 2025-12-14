using Member.JYG._Code;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WavBlock : BlockBase, IUseable
{
    [SerializeField] string soundName;
    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        Used?.Invoke();
        GameManager.Instance.ChangeBGM(soundName);
        Destroy(gameObject);
    }
}
