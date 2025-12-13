using System;
using System.Collections;
using System.Collections.Generic;
using csiimnida.CSILib.SoundManager.RunTime;
using Member.JYG._Code;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerDeadEvent : MonoBehaviour
{
    [SerializeField] private AfterEffector afterEffector; 
    private Player _player;
    public List<GameObject> hideThings = new  List<GameObject>();
    public UnityEvent afterEffect;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
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
        foreach (GameObject hideThing in hideThings)
        {
            hideThing.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.275f);
        SoundManager.Instance.PlaySound("ValueOut");
        afterEffector.PlayPostProcessing(0.275f);
        yield return new WaitForSeconds(3.8f - 0.275f);
        SoundManager.Instance.PlaySound("DeadSound");
        _player.playerInCamera = false;
        CameraShaker.Instance.ImpulseCamera(ImpulseType.SHAKE, 0.5f);
        _player.SpriteRenderer.sprite = null;
        yield return new WaitForSeconds(1f);
        afterEffect?.Invoke();
        _player.playerInCamera = true;
    }

    public void PlayerSecondDeadEvt()
    {
        _player.StopAllCoroutines();
        _player.StopXYVelocity();
        _player.PlayerInputSO.SetInputActive(false);
        StartCoroutine(DeleteSecondPlayer());
    }

    private IEnumerator DeleteSecondPlayer()
    {
        foreach (GameObject hideThing in hideThings)
        {
            hideThing.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        _player.SpriteRenderer.sprite = null;
        _player.playerInCamera = false;
        CameraShaker.Instance.ImpulseCamera(ImpulseType.SHAKE, 0.5f);
        SoundManager.Instance.PlaySound("DeadSound");
        yield return new WaitForSeconds(1f);
        _player.playerInCamera = true;
        Hwan.SceneManager.Instance.OnLoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}