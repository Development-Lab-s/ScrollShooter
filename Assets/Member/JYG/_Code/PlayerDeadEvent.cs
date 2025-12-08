using System;
using System.Collections;
using System.Collections.Generic;
using csiimnida.CSILib.SoundManager.RunTime;
using Member.JYG._Code;
using UnityEngine;

public class PlayerDeadEvent : MonoBehaviour
{
    private Player _player;
    public List<GameObject> hideThings = new  List<GameObject>();

    private void Awake()
    {
        _player = transform.parent.root.GetComponent<Player>();
    }

    public void PlayerDeadEvt()
    {
        _player.StopAllCoroutines();
        _player.StopXYVelocity();
        _player.PlayerInputSO.SetInputActive(false);
        StartCoroutine(DeletePlayer());
    }

    private IEnumerator DeletePlayer()
    {
        yield return new WaitForSeconds(0.275f);
        SoundManager.Instance.PlaySound("ValueOut");
        yield return new WaitForSeconds(3.8f - 0.275f);
        SoundManager.Instance.PlaySound("DeadSound");
        _player.SpriteRenderer.sprite = null;
        foreach (GameObject hideThing in hideThings)
        {
            hideThing.gameObject.SetActive(false);
        }
    }
}
