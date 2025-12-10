using Member.JYG._Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YGPacks;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private TutorialInfoSO[] tutoInfos; 
    public event Action<TutorialInfoSO> OnPlayerNearObstacle;
    public event Action OnSkipPhaze;

    private int currentPhase = 0;
    private InteractiveType currentNeedInput;
    private List<TutoTypeHolder> usedBlocks = new();

    private bool getInput = false;

    protected override void Awake()
    {
        base.Awake();
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.enabled = false;
        if (GameManager.Instance.StageSO.StageNumber == 0)
        {
            col.enabled = true;
            StartTuto();
        }
    }

    private void Update()
    {
        transform.position = new Vector3(0, GameManager.Instance.Player.transform.position.y,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TutoTypeHolder block))
        {
            usedBlocks.ForEach((holder) =>
            {
                if (holder.TutoNumber == block.TutoNumber) return;
            });

            usedBlocks.Add(block);
            PlayTuto();
        }
    }

    private void PlayTuto()
    {
        TutorialInfoSO currentTutoInfo = GetCurrentTuto();

        getInput = true;
        OnPlayerNearObstacle?.Invoke(currentTutoInfo);
        currentNeedInput = currentTutoInfo.NeedInput; 
        InputControlManager.Instance.ChangePlayerInputActiveType(currentNeedInput, true);
        currentPhase++;

        if (currentNeedInput == InteractiveType.None) StartCoroutine(EndCoroutine());
    }

    private IEnumerator EndCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        getInput = false;
        OnSkipPhaze?.Invoke();
        EndTuto();
    }

    public void GetInput(InteractiveType type)
    {
        if (getInput == false || type != currentNeedInput) return;

        getInput = false;
        OnSkipPhaze?.Invoke();

        if (currentPhase == tutoInfos.Length - 1) StartCoroutine(WaitLastTuto());
    }

    private TutorialInfoSO GetCurrentTuto()
    {
        TutorialInfoSO currentTutoInfo = null;

        foreach (TutorialInfoSO tutoInfo in tutoInfos)
        {
            if (tutoInfo.Phase == currentPhase)
            {
                currentTutoInfo = tutoInfo;

                break;
            }
        }

        return currentTutoInfo;
    }

    private void StartTuto()
    {
        InputControlManager.Instance.ChangeAllPlayerActiveType(false);
        InputControlManager.Instance.ChangePlayerInputActiveType(InteractiveType.Middle, true);
    }

    private void EndTuto()
    {
        InputControlManager.Instance.ChangeAllPlayerActiveType(true);
    }

    private IEnumerator WaitLastTuto()
    {
        yield return new WaitForSecondsRealtime(1f);
        PlayTuto();
    }
}
