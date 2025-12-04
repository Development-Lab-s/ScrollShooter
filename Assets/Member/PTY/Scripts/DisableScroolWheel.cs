using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisableScrollWheel : ScrollRect
{
    public override void OnScroll(PointerEventData data)
    {
        return;
    }
}