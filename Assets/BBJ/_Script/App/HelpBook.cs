using UnityEngine;
using UnityEngine.Events;

public class HelpBook : BlockBase, IUseable
{
    public UnityEvent Used;
    public void Use(UseableInfo info)
    {
        HelpPopupWindowManager.Instance.OnActivePopupWindow();
        Used?.Invoke();
        Destroy(gameObject);
    }
}
