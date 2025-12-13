using System;
using UnityEngine;

public class DashColorChanger : MonoBehaviour
{
    public Gradient dashColor;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void DashStateIsTrue(bool canDash)
    {
        if (canDash)
        {
            Debug.Log("ColorChange : White");
            _spriteRenderer.color = dashColor.Evaluate(0f);
        }
        else
        {
            Debug.Log("ColorChange : Gray");
            _spriteRenderer.color = dashColor.Evaluate(1f);
        }
    }
    
}
