using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;
using UnityEngine.Events;

public class HelpBook : BlockBase, IUseable
{
    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        SoundManager.Instance.PlaySound("Click", transform.position.y);

        HelpPopupWindowManager.Instance.OnActivePopupWindow();
        Used?.Invoke();
        Destroy(gameObject);
    }
}
