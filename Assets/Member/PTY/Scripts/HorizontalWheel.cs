using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HorizontalWheel : ScrollRect
{
    public float wheelSensitivity = 30f;
    
    public override void OnScroll(PointerEventData data)
    {
        float delta = data.scrollDelta.y;  // 휠 위/아래 값 (위: +1, 아래: -1)

        Vector2 pos = content.anchoredPosition;
        pos.x += delta * wheelSensitivity;
        content.anchoredPosition = pos;
    }
}