using System;
using Member.JYG._Code;
using UnityEngine;

public class CodeDebugger : MonoBehaviour
{
    HitSystem hitSystem;
    private void Start()
    {
        hitSystem = GameManager.Instance.Player.GetComponent<HitSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            hitSystem.Life -= 1;
        }
    }
}
