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

        if (Input.GetKeyDown(KeyCode.S))
        {
            CameraShaker.Instance.ImpulseCamera(ImpulseType.SHAKE, 1f);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.Player.SetYSpeed(1, 2f, GameManager.Instance.Player.OriginalSpeed);
            Debug.Log("속도 감속");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.Player.SetYSpeed(20, 5f, GameManager.Instance.Player.OriginalSpeed);
            Debug.Log("속도 증가");
        }
    }
}
