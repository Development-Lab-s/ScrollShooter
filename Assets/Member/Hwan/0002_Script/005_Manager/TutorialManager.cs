using Member.JYG.Input;
using System;
using TMPro;
using UnityEngine;
using YGPacks;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private PlayerInputSO uiInputSO;
    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private TutorialInfoSO[] tutoInfos; 
    public event Action<TutorialInfoSO> OnPlayerNearObstacle;
    public event Action OnSkipPhaze;

    private int currentPhase = 0;
    private InteractiveType currentNeedInput;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IBlock>(out IBlock block))
        {
            StartTutoPattern();
        }
    }

    private void StartTutoPattern()
    {
        TimeManager.Instance.FadeStopTime(1, 0.2f);
        TutorialInfoSO currentTutoInfo = tutoInfos[0];
        foreach (TutorialInfoSO tutoInfo in tutoInfos)
        {
            if (tutoInfo.Phase != currentPhase) return;
            currentTutoInfo = tutoInfos[currentPhase];
        }
        OnPlayerNearObstacle?.Invoke(currentTutoInfo);
        currentNeedInput = currentTutoInfo.NeedInput;
        currentPhase++;
    }

    public void GetInput(InteractiveType type)
    {
        if (type == currentNeedInput)
        {
            TimeManager.Instance.UnStopTime();
            OnSkipPhaze?.Invoke();
            currentNeedInput = InteractiveType.None;
        }
    }
}
