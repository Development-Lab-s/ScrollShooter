using Member.JYG._Code;
using Member.JYG.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YGPacks;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private TutorialInfoSO[] tutoInfos; 
    public event Action<TutorialInfoSO> OnPlayerNearObstacle;
    public event Action OnSkipPhaze;

    private int currentPhase = 0;
    private InteractiveType currentNeedInput;
    private List<TutoTypeHolder> usedBlocks = new();

    public bool IsTutorialing { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        if (GameManager.Instance.StageSO.StageNumber == 0)
        {
            IsTutorialing = false;
            playerInputSO.ChangeAllInputState(false);
            IsTutorialing = true;
        }
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
        if (currentPhase == 0) StartTuto();
        TimeManager.Instance.FadeStopTime(0.75f, 0.2f);
        TutorialInfoSO currentTutoInfo = null;
        foreach (TutorialInfoSO tutoInfo in tutoInfos)
        {
            if (tutoInfo.Phase != currentPhase) continue;
            currentTutoInfo = tutoInfos[currentPhase];
        }

        OnPlayerNearObstacle?.Invoke(currentTutoInfo);
        currentNeedInput = currentTutoInfo.NeedInput;
        playerInputSO.ChangeInputState(currentNeedInput, true);
    }

    private void StartTuto()
    {
        playerInputSO.ChangeAllInputState(false);
        playerInputSO.ChangeInputState(InteractiveType.Middle, true);
    }

    private void EndTuto()
    {
        IsTutorialing = false;
        playerInputSO.ChangeAllInputState(true);
    }

    public void GetInput(InteractiveType type)
    {
        if (currentNeedInput != InteractiveType.None)
        {
            if (type != currentNeedInput) return;
        }

        currentNeedInput = InteractiveType.None;
        currentPhase++;
        OnSkipPhaze?.Invoke();

        if (currentPhase == tutoInfos.Length - 1) StartCoroutine(WaitLastTuto());

        if (currentPhase == tutoInfos.Length) EndTuto();
    }

    private IEnumerator WaitLastTuto()
    {
        yield return new WaitForSecondsRealtime(1f);
        PlayTuto();
    }
}
